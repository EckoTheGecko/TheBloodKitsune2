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

namespace RiasSts2.RiasSts2Code.Powers;

public class DemonBloodPower() : RiasSts2Power
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeSideTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side,
        IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner))
            return;

        Flash();
        await Cmd.CustomScaledWait(0.2f, 0.4f);

        int taliAmount = Owner.GetPowerAmount<TalismanPower>();
        int damagePerHit = taliAmount * 4;

        // Loop and do a separate hit for each stack of this power
        for (int i = 0; i < Amount; i++)
        {
            foreach (Creature hittableEnemy in CombatState.HittableEnemies)
            {
                NCombatRoom instance = NCombatRoom.Instance;
                if (instance != null)
                    instance.CombatVfxContainer.AddChildSafely((Node)NFireSmokePuffVfx.Create(hittableEnemy));
            }

            await CreatureCmd.Damage(choiceContext, CombatState.HittableEnemies,
                new DamageVar(damagePerHit, ValueProp.Unpowered), Owner);

            // Brief pacing delay between hits
            if (i < Amount - 1)
            {
                await Cmd.CustomScaledWait(0.1f, 0.2f);
            }
        }
    }
}