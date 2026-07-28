﻿using BaseLib.Cards.Variables;
 using BaseLib.Extensions;
 using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class BloodManipulation() : RiasSts2Card(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
       ..MakeCalculatedBlock(0, (card, target) => card.Owner.Creature.GetPowerAmount<TalismanPower>(),2), 
       new PowerVar<TalismanPower>("TalismanPower",0).WithTooltip("TALISMAN")]; //Here just to add a tooltip
      

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.CalculatedBlock.Calculate(Owner.Creature), this.DynamicVars.CalculatedBlock.Props, play);
        await PowerCmd.Remove<TalismanPower>(Owner.Creature);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationExtra.UpgradeValueBy(3);
    }
}