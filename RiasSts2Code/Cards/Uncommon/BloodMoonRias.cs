using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class BloodMoonRias() : RiasSts2Card(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => [RiasTags.Blood];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BloodMoonRiasPower>("BloodMoonRiasPower",3)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<BloodMoonRiasPower>(choiceContext, this.Owner.Creature, this.DynamicVars["BloodMoonRiasPower"].BaseValue, this.Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<BloodMoonRiasPower>().UpgradeValueBy(2);
        
    }
}