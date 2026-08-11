using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Powers;

public class VampirismPower() : RiasSts2Power
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    // Track how many stacks were applied by an upgraded card
    public int UpgradedStacks { get; set; }

    public override async Task BeforePowerAmountChanged(
        PowerModel power, 
        decimal amount, 
        Creature target, 
        Creature? applier, 
        CardModel? cardSource)
    {
        // verify this only trigger when its vampirism being changed
        if (power == this && cardSource is Vampirism { IsUpgraded: true })
        {
            UpgradedStacks += (int)amount; //if the applying card is upgraded, increment stacks of block
        }

        await base.BeforePowerAmountChanged(power, amount, target, applier, cardSource);
    }

    public override async Task AfterDamageGiven(
        PlayerChoiceContext choiceContext,
        Creature? dealer,
        DamageResult result,
        ValueProp props,
        Creature target,
        CardModel? cardSource)
    {
        if (dealer != Owner || !props.IsPoweredAttack() || result.UnblockedDamage <= 0)
            return;

        Flash();

        // Heal based on total stacks
        await CreatureCmd.Heal(Owner, Amount);

        // Block based strictly on stacks applied by upgraded cards
        if (UpgradedStacks > 0)
        {
            await CreatureCmd.GainBlock(
                Owner, 
                new BlockVar(UpgradedStacks, ValueProp.Move), 
                null
            );
        }
    }
    public override async Task BeforeSideTurnEnd(
        PlayerChoiceContext choiceContext, 
        CombatSide side, 
        IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner))
            return;

        await PowerCmd.Remove(this);
    }
}