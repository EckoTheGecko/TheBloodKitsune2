using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards;


public class Interpose() : RiasSts2Card(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<StrengthPower>("EnemyStr",-4), new PowerVar<StrengthPower>("PlayerStr",-1)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<StrengthPower>(choiceContext, this.Owner.Creature, this.DynamicVars["PlayerStr"].BaseValue, this.Owner.Creature, (CardModel) this);
        foreach (Creature hittableEnemy in (IEnumerable<Creature>) this.CombatState.HittableEnemies)
        {
            StrengthPower strengthPowerPower = await PowerCmd.Apply<StrengthPower>(choiceContext, hittableEnemy, this.DynamicVars["EnemyStr"].BaseValue, Owner.Creature, (CardModel) this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["EnemyStr"].UpgradeValueBy(-2);
    }
}