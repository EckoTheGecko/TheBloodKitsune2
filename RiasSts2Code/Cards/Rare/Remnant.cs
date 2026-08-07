using System.Buffers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards.Rare;


public class Remnant() : RiasSts2Card(2,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(12, ValueProp.Move),
        .. MakeCalculatedVar("halfHP", 0,
            (card, target) => card.Owner?.Creature != null ? card.Owner.Creature.MaxHp / 2 : 0, 1)
    ];



    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }


    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(6);
    }

    private void ReduceCost() => this.EnergyCost.SetUntilPlayed(0);


    public override Task AfterCardEnteredCombat(CardModel card)
    {
        if (card != this || Owner.Creature.CurrentHp >= Owner.Creature.MaxHp / 2)
            return Task.CompletedTask;
        this.ReduceCost();
        return Task.CompletedTask;
    }

    public override Task AfterCurrentHpChanged(Creature creature, decimal delta)
    {
        if (Owner.Creature.CurrentHp >= Owner.Creature.MaxHp / 2)
            return Task.CompletedTask;
        this.ReduceCost();
        return Task.CompletedTask;
    }
}

