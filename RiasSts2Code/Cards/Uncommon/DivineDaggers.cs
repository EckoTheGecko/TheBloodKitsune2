using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class DivineDaggers() : RiasSts2Card(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{


    protected override IEnumerable<DynamicVar> CanonicalVars => [];
      
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromCardWithCardHoverTips<ConjuredDagger>();
    

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        // CombatState.CreateCard creates proper non-canonical runtime instances tied to the Owner
        List<CardModel> daggers = [
            this.CombatState.CreateCard<ConjuredDagger>(this.Owner),
            this.CombatState.CreateCard<ConjuredDagger>(this.Owner)
        ];

        if (this.IsUpgraded)
        {
            foreach (CardModel dagger in daggers)
            { 
                CardCmd.Upgrade(dagger);
            }
        }
        await CardPileCmd.AddGeneratedCardsToCombat(
            daggers,
            PileType.Hand,
            this.Owner
        );
    }
    
    protected override void OnUpgrade()
    {
        
    }
}