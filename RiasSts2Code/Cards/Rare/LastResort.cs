using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards.Rare;


public class LastResort() : RiasSts2Card(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TalismanPower>("TalismanPower",5), 
        ..MakeCalculatedVar("halfHP", 0, (card, target) => card.Owner?.Creature != null ? card.Owner.Creature.MaxHp / 2 : 0, 1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (Owner.Creature.CurrentHp >= Owner.Creature.MaxHp / 2)
            return;
        await PowerCmd.Apply<TalismanPower>(choiceContext, Owner.Creature, DynamicVars["TalismanPower"].BaseValue, Owner.Creature, this);

    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<TalismanPower>().UpgradeValueBy(3);
    }
}