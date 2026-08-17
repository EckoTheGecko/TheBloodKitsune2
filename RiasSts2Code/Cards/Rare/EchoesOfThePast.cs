using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace RiasSts2.RiasSts2Code.Cards.Rare;


public class EchoesOfThePast() : RiasSts2Card(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        ..MakeCalculatedVar("ExhaustedDaggers", 0,(card,_)=> PileType.Exhaust.GetPile(card.Owner).Cards.Count(c => c.Tags.Contains(RiasTags.DivineDagger)))];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        IEnumerable<CardModel> list = PileType.Exhaust.GetPile(Owner).Cards.Where(c => c.Tags.Contains(RiasTags.DivineDagger)).ToList();
        bool flag = true;
        foreach (CardModel card in list)
        {
            if (IsUpgraded)
                CardCmd.Upgrade(card, CardPreviewStyle.None);
            await CardCmd.AutoPlay(choiceContext, card, play.Target, skipCardPileVisuals: !flag);
            flag = false;
        }
    }

    protected override void OnUpgrade()
    {
        
    }
}