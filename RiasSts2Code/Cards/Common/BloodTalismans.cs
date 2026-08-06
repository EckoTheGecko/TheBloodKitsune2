using BaseLib.Extensions;
using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;




public class BloodTalismans() : RiasSts2Card(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TalismanPower>("TalismanPower",2).WithTooltip("TALISMAN")];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<TalismanPower>(choiceContext, this.Owner.Creature, this.DynamicVars["TalismanPower"].BaseValue, this.Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Power<TalismanPower>().UpgradeValueBy(1);
    }
}