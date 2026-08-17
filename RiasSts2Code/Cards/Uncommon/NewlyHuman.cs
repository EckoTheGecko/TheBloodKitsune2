using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards;


public class NewlyHuman() : RiasSts2Card(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<StrengthPower>("StrengthPower",-2), new PowerVar<WeakPower>("WeakPower",2)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await CreatureCmd.LoseBlock(play.Target, play.Target.Block);
        await PowerCmd.Apply<StrengthPower>(choiceContext, play.Target, this.DynamicVars["StrengthPower"].BaseValue, this.Owner.Creature, (CardModel) this);
        await PowerCmd.Apply<WeakPower>(choiceContext, play.Target, this.DynamicVars["WeakPower"].BaseValue, this.Owner.Creature, (CardModel) this);

    }

    protected override void OnUpgrade()
    {
        // DynamicVars.Power<WeakPower>().UpgradeValueBy(1);
        // DynamicVars.Power<StrengthPower>().UpgradeValueBy(-1);
        RemoveKeyword(CardKeyword.Exhaust);
    }
}