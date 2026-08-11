using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Character;

namespace RiasSts2.RiasSts2Code.Cards;
[Pool(typeof(TokenCardPool))]
public class ConjuredDagger() : RiasSts2Card(0,
    CardType.Attack, CardRarity.Token,
    TargetType.AnyEnemy)
{
    

    protected override HashSet<CardTag> CanonicalTags => [RiasTags.Blood, RiasTags.DivineDagger];
    
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(4, ValueProp.Move), new HealVar(1)];

    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target, nameof(play.Target));

        // Color for the dagger throw
        Color daggerColor = new Color("#721e1e");

        // Execute attack with complete attacker flurry + hit impact node VFX
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithAttackerFx(() => NDaggerSprayFlurryVfx.Create(Owner.Creature, daggerColor, true))
            .WithHitVfxNode((target) => NDaggerSprayImpactVfx.Create(target, daggerColor,  true))
            .Execute(choiceContext);

        // Apply Regen Power
        await CreatureCmd.Heal(Owner.Creature, DynamicVars.Heal.BaseValue);

    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
        DynamicVars.Heal.UpgradeValueBy(1);
    }
}