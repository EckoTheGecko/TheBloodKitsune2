using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Powers;


public class AcornPower() : RiasSts2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHand)
    {  
        //does nothing if card drawn belongs to someone else or its not player turn
        if (fromHand || card.Owner.Creature != this.Owner || card.Owner.Creature.CombatState.CurrentSide != card.Owner.Creature.Side) 
            return;
        BlockVar blockVar = new BlockVar(Amount, ValueProp.Unpowered); //gain block equal to power amount, does not scale with dex
        await CreatureCmd.GainBlock(Owner, blockVar, null);

    }
}