using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace RiasSts2.RiasSts2Code.Cards.Rare;

public class SpiritGuide() : RiasSts2Card(
    1,
    CardType.Skill, 
    CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

    // Override the selection prompt so it returns a valid LocString
    public LocString SelectionScreenPrompt => 
        new ("cards", "RIASSTS2-SPIRIT_GUIDE_SELECTION_PROMPT");

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (Owner == null)
            return;
        
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        int cardsToFetch = (int)DynamicVars.Cards.BaseValue;
        CardPile exhaustPile = PileType.Exhaust.GetPile(Owner);

        if (exhaustPile.Cards.Count == 0)
            return;

        var selectedCards = (await CardSelectCmd.FromCombatPile(
            choiceContext,
            exhaustPile,
            Owner,
            new CardSelectorPrefs(SelectionScreenPrompt, cardsToFetch)
        )).ToList();

        // Move the selected card(s) to Hand
        await CardPileCmd.Add(selectedCards, PileType.Hand);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.BaseValue = 2;
    }
}