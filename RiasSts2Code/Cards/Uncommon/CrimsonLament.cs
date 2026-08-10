using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class CrimsonLament() : RiasSts2Card(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AllEnemies)
{

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromPowerWithPowerHoverTips<TalismanPower>();

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..MakeCalculatedVar("CalculatedHits",0, (card, target) => card.Owner.Creature.GetPowerAmount<TalismanPower>()), 
        new PowerVar<TalismanPower>("TalismanPower",0), new DamageVar(5, ValueProp.Move)]; //Here just to add a tooltip
      

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (Owner.Creature.GetPowerAmount<TalismanPower>() <= 0)
            return;
            
        int hitCount = (int)((CalculatedVar)DynamicVars["CalculatedHits"]).Calculate(null); //performs calculation for above var
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .TargetingAllOpponents(CombatState)
            .WithHitCount(hitCount)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }
}