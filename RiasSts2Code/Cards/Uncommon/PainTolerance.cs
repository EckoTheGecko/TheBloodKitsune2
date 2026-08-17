using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class PainTolerance() : RiasSts2Card(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    

    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(9, ValueProp.Move), new PowerVar<TalismanPower>(2)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromPowerWithPowerHoverTips<TalismanPower>();


    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await PowerCmd.Apply<TalismanPower>(choiceContext, this.Owner.Creature, this.DynamicVars["TalismanPower"].BaseValue, this.Owner.Creature, (CardModel) this);
    }
    
    protected override void OnUpgrade()
    {
        DynamicVars.Power<TalismanPower>().UpgradeValueBy(1);
        DynamicVars.Block.UpgradeValueBy(3);
    }
}