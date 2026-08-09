using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards.Rare;

public class Oathkeeper() : RiasSts2Card(2,
    CardType.Power, CardRarity.Ancient,
    TargetType.Self) 
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<VigorPower>(7)];
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
        LocString customPrompt = new LocString("characters", "TO_EXHAUST_ANY");
        CardSelectorPrefs prefs = new CardSelectorPrefs(customPrompt, 0,PileType.Hand.GetPile(Owner).Cards.Count);
        
        IEnumerable<CardModel> selectedCards = await CardSelectCmd.FromHandForDiscard(choiceContext, Owner, prefs, null, this);
        List<CardModel> cardsToExhaust = selectedCards.ToList();


        foreach (CardModel card in cardsToExhaust)
        {
            await CardCmd.Exhaust(choiceContext, card);
        }


        int cardsExhaustedCount = cardsToExhaust.Count;
        int totalVigor = (int)DynamicVars.Power<VigorPower>().BaseValue * cardsExhaustedCount;


        if (totalVigor > 0)
        {
            await PowerCmd.Apply<VigorPower>(choiceContext, Owner.Creature, totalVigor, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<VigorPower>().UpgradeValueBy(3);
    }
}