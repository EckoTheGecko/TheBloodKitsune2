using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards.Rare;


public class BloodBarrier() : RiasSts2Card(2,
    CardType.Power, CardRarity.Rare,
    TargetType.AllAllies)
{
    protected override HashSet<CardTag> CanonicalTags => [RiasTags.Blood];
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<PlatingPower>(4)];

    
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        foreach (Creature creature in CombatState.GetTeammatesOf(Owner.Creature).Where(c => c != null && c.IsAlive && c.IsPlayer))
            await PowerCmd.Apply<PlatingPower>(choiceContext, creature, DynamicVars["PlatingPower"].BaseValue, Owner.Creature, this);
        
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<PlatingPower>().UpgradeValueBy(2);
    }
}