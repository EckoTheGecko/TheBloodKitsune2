using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class MarkOfBinding() : RiasSts2Card(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    
    protected override bool ShouldGlowGoldInternal { get {
            return this.CombatState != null && this.CombatState.HittableEnemies.Any<Creature>((Func<Creature, bool>) (e =>
            {
                MonsterModel monster = e.Monster;
                return monster != null && monster.IntendsToAttack;
            }));
        }
    }
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<VulnerablePower>("VulnerablePower",1), 
        new PowerVar<WeakPower>("WeakPower",1),
        new PowerVar<TalismanPower>("TalismanPower",1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<VulnerablePower>(choiceContext, play.Target, this.DynamicVars["VulnerablePower"].BaseValue, this.Owner.Creature, (CardModel) this);
        await PowerCmd.Apply<WeakPower>(choiceContext, play.Target, this.DynamicVars["WeakPower"].BaseValue, this.Owner.Creature, (CardModel) this);
        if (!play.Target.Monster.IntendsToAttack)
            return;
        await PowerCmd.Apply<TalismanPower>(choiceContext, this.Owner.Creature, this.DynamicVars["TalismanPower"].BaseValue, this.Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<VulnerablePower>().UpgradeValueBy(1);
        DynamicVars.Power<WeakPower>().UpgradeValueBy(1);
    }
}