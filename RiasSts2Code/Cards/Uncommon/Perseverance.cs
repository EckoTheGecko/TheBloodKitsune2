using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Powers;

namespace RiasSts2.RiasSts2Code.Cards;


public class Perseverance() : RiasSts2Card(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<PerseverancePower>("PerseverancePower", 6)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        NPowerUpVfx.CreateNormal(this.Owner.Creature);
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "PowerUp", this.Owner.Character.PowerUpAnimDelay);
        await PowerCmd.Apply<PerseverancePower>(choiceContext, this.Owner.Creature, this.DynamicVars["PerseverancePower"].BaseValue, this.Owner.Creature, (CardModel) this);
    } 

    protected override void OnUpgrade()
    {
        DynamicVars.Power<PerseverancePower>().UpgradeValueBy(2);
    }
}