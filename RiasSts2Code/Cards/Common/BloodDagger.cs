using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;

public class BloodDagger() : RiasSts2Card(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [RiasTags.Blood];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromPowerWithPowerHoverTips<TalismanPower>();

    protected override IEnumerable<DynamicVar> CanonicalVars => 
    [
        new DamageVar(4, ValueProp.Move), 
        new PowerVar<VulnerablePower>(1),
        ..MakeCalculatedVar("total", 0, (card, target) => 
        {
            var player = card.Owner?.Creature;
            if (player == null) return 0;

            var power = player.GetPower<TalismanPower>();
            int hitCount = power != null ? power.Amount : 0;
            var strength = player.GetPower<StrengthPower>();
            int strTotal = strength != null ? power.Amount : 0;
            return (card.DynamicVars.Damage.IntValue+strTotal) * hitCount;
        })
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var player = this.Owner.Creature;
        var power = player.GetPower<TalismanPower>();
        int hitCount = power != null ? power.Amount : 0;

        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitCount(hitCount)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

        await PowerCmd.Apply<VulnerablePower>(
            choiceContext, 
            play.Target, 
            this.DynamicVars["VulnerablePower"].BaseValue, 
            this.Owner.Creature, 
            (CardModel)this
        );
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(3);
    }
}