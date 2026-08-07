using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class BloodMoonRitual() : RiasSts2Card(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => [RiasTags.Blood];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        ..MakeCalculatedVar("ExhaustPile", 0,(card,_)=> PileType.Exhaust.GetPile(card.Owner).Cards.Count), ];
    //Get number of cards in exhaust
    

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust]; //makes it exhaust after player and adds the word to the bottom of card automatically
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
        int taliGained = (int)((CalculatedVar)DynamicVars["ExhaustPile"]).Calculate(Owner.Creature); //Calculate the "ExhaustPile" var and put it in taliGained
        await PowerCmd.Apply<TalismanPower>(choiceContext, this.Owner.Creature, taliGained, this.Owner.Creature, //gain tali equal taliGained
            (CardModel)this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}