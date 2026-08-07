using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards.Rare;


public class Endwalker() : RiasSts2Card(2,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<EndwalkerPower>(1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<EndwalkerPower>(choiceContext, this.Owner.Creature, this.DynamicVars["EndwalkerPower"].BaseValue, this.Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<EndwalkerPower>().UpgradeValueBy(1);
    }
}