using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class BloodDagger() : RiasSts2Card(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    
    protected override HashSet<CardTag> CanonicalTags => [RiasTags.Blood];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move), new PowerVar<VulnerablePower>(1).WithTooltip("TALISMAN")];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var player = this.Owner.Creature;
        TalismanPower power = player.GetPower<TalismanPower>();
        int amount = power != null ? power.Amount : 0;
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitCount(amount)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        await PowerCmd.Apply<VulnerablePower>(choiceContext, play.Target, this.DynamicVars["VulnerablePower"].BaseValue, this.Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(3);
    }
}