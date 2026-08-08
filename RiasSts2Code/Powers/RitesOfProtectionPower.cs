using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Powers;


public class RitesOfProtectionPower() : RiasSts2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (amount <= 0 ||  this.CombatState.CurrentSide != this.Owner.Side)
            return;
        if (power is TalismanPower && power.Owner == this.Owner)
        {
            BlockVar blockVar = new BlockVar(Amount * amount, ValueProp.Move);
            foreach (Creature creature in CombatState.GetTeammatesOf(Owner).Where(c => c != null && c.IsAlive && c.IsPlayer))
                await CreatureCmd.GainBlock(creature, blockVar, null);
        }
    }
}