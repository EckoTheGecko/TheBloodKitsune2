using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards.Rare;


public class ScarletPierce() : RiasSts2Card(2,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        ..MakeCalculatedDamage("calcDamage", 6, (card, target) => card.Owner.PlayerCombatState.AllCards.Count(c => c.Tags.Contains<CardTag>(RiasTags.Blood)),2)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
        int totalDamage = (int)((CalculatedDamageVar)DynamicVars["calcDamage"]).Calculate(null);
        await DamageCmd.Attack(totalDamage)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {

    }
}