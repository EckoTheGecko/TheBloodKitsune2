using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class CeremonialSash() : RiasSts2Card(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TalismanPower>("TalismanPower",1).WithTooltip("TALISMAN"), new EnergyVar(1), new CardsVar(1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        await PowerCmd.Apply<TalismanPower>(choiceContext, this.Owner.Creature, this.DynamicVars["TalismanPower"].BaseValue, this.Owner.Creature, (CardModel) this);
        await CardPileCmd.Draw(choiceContext,DynamicVars.Cards.BaseValue, this.Owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<TalismanPower>().UpgradeValueBy(1);
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}