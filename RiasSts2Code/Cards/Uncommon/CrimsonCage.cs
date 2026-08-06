using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards;


public class CrimsonCage() : RiasSts2Card(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<IntangiblePower>("IntangiblePower",1), new PowerVar<WeakPower>("WeakPower",1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<WeakPower>(choiceContext, this.Owner.Creature, this.DynamicVars["WeakPower"].BaseValue, this.Owner.Creature, (CardModel) this);
        await PowerCmd.Apply<IntangiblePower>(choiceContext, this.Owner.Creature, this.DynamicVars["IntangiblePower"].BaseValue, this.Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<IntangiblePower>().UpgradeValueBy(1);
        DynamicVars.Power<WeakPower>().UpgradeValueBy(1);
    }
}