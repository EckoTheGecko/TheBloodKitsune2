using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Powers;


public class MomentumPower() : RiasSts2Power
{
    
    
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    
    
    public override Decimal ModifyHandDraw(Player player, Decimal count)
    {
        return player != this.Owner.Player ? count : count + (Decimal) this.Amount;
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != this.Owner.Player)
            return;
        foreach (CardModel card in await CardSelectCmd.FromHand(choiceContext, player, new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, this.Amount), (Func<CardModel, bool>) null, (AbstractModel) this))
            await CardCmd.Exhaust(choiceContext, card);
        
        for (int i = 0; i < Amount; i++)
        {
            CardModel dagger = this.CombatState.CreateCard<ConjuredDagger>(Owner.Player);
            await CardPileCmd.AddGeneratedCardToCombat(dagger, PileType.Hand, Owner.Player);
        }

    }
}