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
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("TotalDamage", 4 * Amount)
    ];
    public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side,
        IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner))
            return;
        {
            this.Flash();
            await Cmd.CustomScaledWait(0.2f, 0.4f);
            foreach (Creature hittableEnemy in CombatState.HittableEnemies)
            {
                NCombatRoom instance = NCombatRoom.Instance;
                if (instance != null)
                    instance.CombatVfxContainer.AddChildSafely((Node)NFireSmokePuffVfx.Create(hittableEnemy));
            }

            await Cmd.CustomScaledWait(0.2f, 0.4f);
            await CreatureCmd.Damage(choiceContext, CombatState.HittableEnemies,
                new DamageVar(this.Amount * 4, ValueProp.Unpowered), this.Owner);
            await PowerCmd.Remove(this);
        }



    }
}