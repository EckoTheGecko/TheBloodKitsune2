using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards;


public class BloodClaws() : RiasSts2Card(1,
    CardType.Attack, CardRarity.Basic,
    TargetType.AnyEnemy), ITranscendenceCard
{
    
    protected override HashSet<CardTag> CanonicalTags => [RiasTags.Blood];
    public CardModel GetTranscendenceTransformedCard() => ModelDb.Card<FeralClaws>();

    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new DamageVar(5, ValueProp.Move),
    new RepeatVar(2)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitCount(DynamicVars.Repeat.IntValue)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(2);
    }
}