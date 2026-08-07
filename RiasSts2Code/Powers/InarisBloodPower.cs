using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Powers;


public class InarisBloodPower() : RiasSts2Power
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    


    public override async Task AfterDamageReceived(
        PlayerChoiceContext choiceContext,
        Creature target,
        DamageResult result,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        if (target != this.Owner || result.UnblockedDamage <= 0 || this.CombatState.CurrentSide != this.Owner.Side)
            return;
        {
            TalismanPower talismanPower = await PowerCmd.Apply<TalismanPower>(choiceContext, this.Owner, (Decimal) this.Amount, this.Owner, (CardModel) null);
        }
    }
}
