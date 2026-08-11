using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Powers;


public class PerseverancePower() : RiasSts2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar("SelfDamage", 0M, ValueProp.Unblockable | ValueProp.Unpowered)];
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != this.Owner.Player)
            return;
        this.Flash();
        DamageVar dynamicVar = (DamageVar) this.DynamicVars["SelfDamage"];
        IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(choiceContext, this.Owner, dynamicVar.BaseValue, dynamicVar.Props, this.Owner, (CardModel) null);
        await PowerCmd.Apply<VigorPower>(choiceContext, Owner, Amount, Owner, null);
    
    }

    public void IncrementSelfDamage()
    {
        this.AssertMutable();
        ++this.DynamicVars["SelfDamage"].BaseValue;
    }
}