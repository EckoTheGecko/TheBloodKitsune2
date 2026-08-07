using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class BloodInscription() : RiasSts2Card(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [RiasTags.Blood];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(15, ValueProp.Move).WithTooltip("TALISMAN")];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        TalismanPower power = Owner.Creature.GetPower<TalismanPower>();
        int amount = power != null ? power.Amount : 0;
        if (amount > 0)
            return;
        
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(5);
    }
}