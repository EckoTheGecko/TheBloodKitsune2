using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Powers;

public class DivineAssistancePower : RiasSts2Power
{
    private const string HpLossKey = "HpLoss";
    private static readonly Random TargetRng = new();

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars => new[]
    {
        new DamageVar(HpLossKey, 2m, ValueProp.Unblockable | ValueProp.Unpowered)
    };

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != this.Owner.Player || this.Amount <= 0)
            return;

        this.Flash();

        // 1. Lose 2 HP per stack (Unblockable damage)
        DamageVar damageVar = (DamageVar)this.DynamicVars[HpLossKey];
        decimal totalDamage = damageVar.BaseValue * (decimal)this.Amount;
        await CreatureCmd.Damage(choiceContext, this.Owner, totalDamage, damageVar.Props, this.Owner, (CardModel?)null);

        // 2. Create and play Conjured Daggers for each stack
        var combatState = this.Owner.CombatState;
        Player? ownerPlayer = this.Owner.Player;

        if (combatState == null || ownerPlayer == null)
            return;

        for (int i = 0; i < this.Amount; i++)
        {
            var livingEnemies = combatState.Enemies.Where(e => e.IsAlive).ToList();
            if (livingEnemies.Count == 0)
                break;

            Creature randomEnemy = livingEnemies[TargetRng.Next(livingEnemies.Count)];

            // Instantiate a distinct, mutable card instance for this player
            CardModel dagger = combatState.CreateCard<ConjuredDagger>(ownerPlayer);

            // Execute auto play on the target enemy
            await CardCmd.AutoPlay(
                choiceContext, 
                dagger, 
                randomEnemy, 
                AutoPlayType.Default, 
                false, 
                false
            );
        }
    }
}