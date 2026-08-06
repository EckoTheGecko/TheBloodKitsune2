using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace RiasSts2.RiasSts2Code.Cards;


public class HyperSenses() : RiasSts2Card(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(4, ValueProp.Move), 
        new PowerVar<WeakPower>(1)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        IReadOnlyList<Creature> enemies = this.CombatState.HittableEnemies.ToList();
        
        decimal totalBlock = this.DynamicVars.Block.IntValue * enemies.Count;
        if (totalBlock > 0)
        {
            await CreatureCmd.GainBlock(Owner.Creature, totalBlock, ValueProp.Move,play);
        }
        
        foreach (Creature hittableEnemy in (IEnumerable<Creature>) this.CombatState.HittableEnemies)
        {
            WeakPower weakPower = await PowerCmd.Apply<WeakPower>(choiceContext, hittableEnemy, this.DynamicVars["WeakPower"].BaseValue, Owner.Creature, (CardModel) this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(2);
        DynamicVars.Power<WeakPower>().UpgradeValueBy(1);
    }
}