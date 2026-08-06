using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards;


public class BloodSpikes() : RiasSts2Card(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move),
        ..MakeCalculatedVar("CalculatedHits", 0,(card,_)=> PileType.Exhaust.GetPile(card.Owner).Cards.Count)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
        
        int hitCount = (int)((CalculatedVar)DynamicVars["CalculatedHits"]).Calculate(play.Target); //performs calculation for above var
        
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitCount(hitCount) //hits equal to the calc'd number above
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }
    

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}