﻿using BaseLib.Cards.Variables;
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
       //  new CalculationBaseVar(0),
       // new CalculationExtraVar(DynamicVars["blockies"].BaseValue),
       //  new CalculationExtraVar(2),
       //  new CalculatedBlockVar(ValueProp.Move)
       ..MakeCalculatedBlock(0, (card, target) => card.Owner.Creature.GetPowerAmount<TalismanPower>(),2)];
      

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        // int amount = Owner.Creature.GetPowerAmount<TalismanPower>();
        // if (amount <= 0)
        //     return;
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.CalculatedBlock.Calculate(Owner.Creature), this.DynamicVars.CalculatedBlock.Props, play);
        await PowerCmd.Remove<TalismanPower>(Owner.Creature);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationExtra.UpgradeValueBy(3);
    }
}