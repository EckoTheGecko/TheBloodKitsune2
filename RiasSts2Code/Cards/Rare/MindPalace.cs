using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards.Rare;


public class MindPalace() : RiasSts2Card(3,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (Owner?.PlayerCombatState == null) return;

        // Iterate through all cards currently in the draw pile
        foreach (CardModel card in PileType.Draw.GetPile(Owner).Cards)
        {
            // Only reduce cost if it's strictly greater than 1 energy
            if (card.EnergyCost.GetAmountToSpend() > 1)
            {
                // Modifies base cost for rest of combat while respecting minimum floor
                card.EnergyCost.AddThisCombat(-1);
            }
        }

        await Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        // Reduce the play cost from 3 to 2 when upgraded
        EnergyCost.UpgradeBy(-1);
    }
}