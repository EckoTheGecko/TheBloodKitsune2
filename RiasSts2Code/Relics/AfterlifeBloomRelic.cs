using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace RiasSts2.RiasSts2Code.Relics;

public class AfterlifeBloomRelic() : RiasSts2Relic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    // Set to 1 energy per turn
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1)];

    public override async Task AfterPlayerTurnStartLate(
        PlayerChoiceContext choiceContext,
        Player player)
    {
        // Only trigger for the relic owner on turns 1, 2, and 3
        if (player != Owner || Owner.PlayerCombatState.TurnNumber > 3)
            return;

        // Flash relic UI icon when activated
        this.Flash();

        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
    }
}