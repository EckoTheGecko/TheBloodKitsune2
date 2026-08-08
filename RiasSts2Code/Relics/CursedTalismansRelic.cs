using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Relics;

namespace RiasSts2.RiasSts2Code.Powers;


public class CursedTalismansRelic() : RiasSts2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;


    

    public override async Task AfterCurrentHpChanged(
        Creature creature,
        decimal delta)
    {
        if (delta >= 0M)
            return;

        if (creature.Monster is not Osty)
            return;

        if (creature.PetOwner != Owner)
            return;

        // delta is negative, so convert it to positive damage.
        decimal damage = -delta;

        // We need a PlayerChoiceContext for PowerCmd.
        await PowerCmd.Apply<DoomPower>(
            new ThrowingPlayerChoiceContext(),
            creature.CombatState.HittableEnemies,
            damage,
            Owner.Creature,
            null);
    }
    
}