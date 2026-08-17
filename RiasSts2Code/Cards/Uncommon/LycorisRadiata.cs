using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Powers;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards;


public class LycorisRadiata() : RiasSts2Card(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<RegenPower>(4)
    ];

    protected override bool IsPlayable
    {
        get
        {
            int playsThisTurn = CombatManager.Instance.History.CardPlaysStarted
                .Count(e => e.Actor == Owner.Creature && e.CardPlay.IsFirstInSeries && e.HappenedThisTurn(CombatState));

            return base.IsPlayable && playsThisTurn == 0;
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        // Counts how many cards this player has played so far this turn (including this card)
        int playsThisTurn = CombatManager.Instance.History.CardPlaysStarted
            .Count(e => e.Actor == Owner.Creature && e.CardPlay.IsFirstInSeries && e.HappenedThisTurn(CombatState));

        // If playsThisTurn is 1, this card is the first card played this turn
        if (playsThisTurn <= 1)
        {
            await PowerCmd.Apply<RegenPower>(
                choiceContext,
                Owner.Creature,
                DynamicVars.Power<RegenPower>().IntValue,
                Owner.Creature,
                this
            );
        }
        else
            return;

        // Apply TangledPower (cannot play Attacks for 1 turn)
        await PowerCmd.Apply<TangledPower>(
            choiceContext,
            Owner.Creature,
            1,
            Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade()
    {
        
        DynamicVars.Power<RegenPower>().UpgradeValueBy(2);
    }
}