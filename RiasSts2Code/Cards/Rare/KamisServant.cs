using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards.Rare;


public class KamisServant() : RiasSts2Card(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<KamisServantPower>("KamisServantPower",2)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<KamisServantPower>(choiceContext, Owner.Creature, DynamicVars["KamisServantPower"].BaseValue, Owner.Creature, this);

    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<KamisServantPower>().UpgradeValueBy(1);
    }
}