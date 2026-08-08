using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Powers;
using RiasSts2.RiasSts2Code.Relics;

namespace RiasSts2.RiasSts2Code.Relics;


public class ProtectionTalismansRelic() : RiasSts2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(3, ValueProp.Move)];

    public override async Task AfterDamageReceived(
        PlayerChoiceContext choiceContext,
        Creature target,
        DamageResult result,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        if (target != this.Owner.Creature || result.UnblockedDamage <= 0 || Owner.Creature.CombatState.CurrentSide != this.Owner.Creature.Side)
            return;
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, null);
    }
    
}