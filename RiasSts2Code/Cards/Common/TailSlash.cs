using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards;


public class TailSlash() : RiasSts2Card(2,
    CardType.Attack, CardRarity.Common,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(9, ValueProp.Move), new PowerVar<VulnerablePower>("VulnerablePower",1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        
        await PowerCmd.Apply<VulnerablePower>(choiceContext, CombatState.HittableEnemies, this.DynamicVars.Power<VulnerablePower>().BaseValue, this.Owner.Creature, (CardModel) this);
    
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<VulnerablePower>().UpgradeValueBy(1);
        DynamicVars.Damage.UpgradeValueBy(3);

    }
}