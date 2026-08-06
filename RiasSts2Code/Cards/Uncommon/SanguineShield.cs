using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;

public class SanguineShield() : RiasSts2Card(1,
    CardType.Skill, CardRarity.Uncommon,
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
    }
    protected override void OnUpgrade()
    {
        DynamicVars.CalculatedBlock.UpgradeValueBy(1);
    }
}