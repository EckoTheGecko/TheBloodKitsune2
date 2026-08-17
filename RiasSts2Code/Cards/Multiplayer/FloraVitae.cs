using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards.Rare;

public class FloraVitae() : RiasSts2Card(
    1,
    CardType.Skill, 
    CardRarity.Uncommon,
    TargetType.AllAllies)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        
        HoverTipFactory.FromCardWithCardHoverTips<ConjuredDagger>(IsUpgraded);

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        IEnumerable<Creature> allies = CombatState.GetTeammatesOf(Owner.Creature)
            .Where(c => c != null && c.IsAlive && c.IsPlayer);

        foreach (Creature creature in allies)
        {
            // Get the Player model corresponding to the teammate creature
            Player? teammatePlayer = creature.Player;
            if (teammatePlayer == null) continue;

            // Instantiate a distinct card instance for each player
            CardModel dagger = CombatState.CreateCard<ConjuredDagger>(teammatePlayer);

            if (IsUpgraded)
            {
                CardCmd.Upgrade(dagger);
            }

            // Add the new instance to this player's hand
            await CardPileCmd.AddGeneratedCardToCombat(dagger, PileType.Hand, teammatePlayer);
        }
    }

    protected override void OnUpgrade()
    {
        
    }
}