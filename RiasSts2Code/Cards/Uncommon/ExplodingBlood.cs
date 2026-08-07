using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class ExplodingBlood() : RiasSts2Card(2,
    CardType.Power, CardRarity.Uncommon,
    TargetType.AllEnemies)
{
    protected override HashSet<CardTag> CanonicalTags => [RiasTags.Blood];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ExplodingBloodPower>("ExplodingBloodPower",3)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ExplodingBloodPower>(choiceContext, Owner.Creature, DynamicVars["ExplodingBloodPower"].BaseValue, Owner.Creature,  this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<ExplodingBloodPower>().UpgradeValueBy(2);
    }
}