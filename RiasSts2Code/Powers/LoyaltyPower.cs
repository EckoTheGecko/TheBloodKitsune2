using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Powers;


public class LoyaltyPower() : RiasSts2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    public override async Task AfterCardExhausted(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool _)
    {
        if (card.Owner.Creature != this.Owner)
            return;
        TalismanPower talismanPower = await PowerCmd.Apply<TalismanPower>( new ThrowingPlayerChoiceContext(), Owner, Amount, this.Owner, (CardModel) null);
    }
    
}