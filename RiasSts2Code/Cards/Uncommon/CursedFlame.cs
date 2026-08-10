using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class CursedFlame() : RiasSts2Card(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{ 
    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromPowerWithPowerHoverTips<TalismanPower>();

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TalismanPower>(0)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int amount = Owner.Creature.GetPowerAmount<TalismanPower>(); //get current talismans amount
        await PowerCmd.Apply<TalismanNextTurnPower>(choiceContext, this.Owner.Creature, amount, this.Owner.Creature, (CardModel) this);
    } // add TalismanNextTurnPower equal to current talismans, which adds talis equal to how many stack exist

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}