using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Powers;


public class RecursionPower() : RiasSts2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        int playsThisTurn = CombatManager.Instance.History.CardPlaysStarted
            .Count(e => e.Actor == Owner && e.CardPlay.IsFirstInSeries && e.HappenedThisTurn(CombatState));

        if (card.Owner.Creature == this.Owner && card.Tags.Contains(RiasTags.Blood) && playsThisTurn < 1) //if owned by this creature, contains blood tag and is first card played
            ++playCount;
        
        return playCount;
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player)
            return;
        this.Flash();
        await CreatureCmd.Damage(choiceContext, Owner, Amount, ValueProp.Unblockable, this.Owner, (CardModel) null);
    }
}