using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace RiasSts2.RiasSts2Code.Powers;


public class AbundancePower() : RiasSts2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterBlockGained(
        Creature creature, 
        decimal amount, 
        ValueProp props, 
        CardModel? cardSource)
    {
        // Only trigger when the power owner gains block and block amount > 0
        if (creature != this.Owner || amount <= 0m || this.Amount <= 0)
            return;

        this.Flash();

        // Apply TalismanPower equal to AbundancePower's stack count
        await PowerCmd.Apply<TalismanPower>(
            new ThrowingPlayerChoiceContext(), 
            this.Owner, 
            this.Amount, 
            this.Owner, 
            cardSource
        );
    }
}