using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using RiasSts2.RiasSts2Code.Relics;

namespace RiasSts2.RiasSts2Code.Relics;


public class AfterlifeBloomRelic() : RiasSts2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(2)];

    public override async Task AfterPlayerTurnStartLate(
        PlayerChoiceContext choiceContext,
        Player player)
    {
        if (player != Owner || Owner.PlayerCombatState.TurnNumber > 1)
            return;
        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
    }
}