using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards.Rare;


public class DivineAssistance() : RiasSts2Card(2,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        
        HoverTipFactory.FromCardWithCardHoverTips<ConjuredDagger>();
    
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<DivineAssistancePower>("DivineAssistancePower", 1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<DivineAssistancePower>(choiceContext, Owner.Creature, DynamicVars["DivineAssistancePower"].BaseValue, Owner.Creature, this);

    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}