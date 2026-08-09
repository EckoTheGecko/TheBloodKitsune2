using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace RiasSts2.RiasSts2Code.Powers;


public class TalismanPower() : RiasSts2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    // public int TotalDamage => 4 * Amount;
    

    public override async Task BeforeSideTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side,
        IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner))
            return;

        Flash();
        await Cmd.CustomScaledWait(0.2f, 0.4f);

        foreach (Creature hittableEnemy in CombatState.HittableEnemies)
        {
            NCombatRoom instance = NCombatRoom.Instance;
            if (instance != null)
            {
                // Create VFX instance
                NFireSmokePuffVfx vfx = NFireSmokePuffVfx.Create(hittableEnemy);
            
                if (vfx != null)
                {
                    // Tint the effect
                    vfx.Modulate = new Color("#B026FF"); // Purple tint
                    

                    // Add to container
                    instance.CombatVfxContainer.AddChildSafely((Node)vfx);
                }
            }
        }

        await Cmd.CustomScaledWait(0.2f, 0.4f);
        await CreatureCmd.Damage(choiceContext, CombatState.HittableEnemies,
            new DamageVar(Amount * 4, ValueProp.Unpowered), Owner);
    }

    public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner))
            return;
        await PowerCmd.Remove(this);
    }
}