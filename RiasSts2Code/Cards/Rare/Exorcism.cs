using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using RiasSts2.RiasSts2Code.Cards;

namespace RiasSts2.RiasSts2Code.Cards.Rare;


public class Exorcism() : RiasSts2Card(2,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [..MakeCalculatedDamage("totalDamage", 0, (card, target) => card.Owner?.Creature != null ? card.Owner.Creature.MaxHp - card.Owner.Creature.CurrentHp : 0, 1)];
    
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int totalDamage = (int)((CalculatedDamageVar)DynamicVars["totalDamage"]).Calculate(play.Target);

        await DamageCmd.Attack(totalDamage)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitVfxNode((target) =>
            {
                // get anim paths
                string scenePath = SceneHelper.GetScenePath(VfxCmd.screamVfx);
                Node2D screamNode = PreloadManager.Cache.GetScene(scenePath).Instantiate<Node2D>();

                if (screamNode != null)
                {
                    // tint the effect (red)
                    screamNode.Modulate = new Color("#8B0000");

                    // Position the node on the enemy
                    NCreature creatureNode = target.GetCreatureNode();
                    if (creatureNode != null)
                    {
                        screamNode.GlobalPosition = creatureNode.VfxSpawnPosition;
                    }
                }

                return screamNode;
            })
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}


// using Godot;
// using MegaCrit.Sts2.Core.Commands;
// using MegaCrit.Sts2.Core.Entities.Cards;
// using MegaCrit.Sts2.Core.GameActions.Multiplayer;
// using MegaCrit.Sts2.Core.Localization.DynamicVars;
// using MegaCrit.Sts2.Core.Nodes.Combat;
// using MegaCrit.Sts2.Core.Nodes.Vfx;
// using MegaCrit.Sts2.Core.ValueProps;
// using RiasSts2.RiasSts2Code.Cards;
//
// namespace RiasSts2.RiasSts2Code.Cards.Rare;
//
// public class Exorcism() : RiasSts2Card(2,
//     CardType.Attack, CardRarity.Rare,
//     TargetType.AnyEnemy)
// {
//     protected override IEnumerable<DynamicVar> CanonicalVars =>
//     [..MakeCalculatedDamage("totalDamage", 0, (card, target) => card.Owner?.Creature != null ? card.Owner.Creature.MaxHp - card.Owner.Creature.CurrentHp : 0, 1)];
//     
//     public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
//
//     protected override async Task OnPlay(
//         PlayerChoiceContext choiceContext,
//         CardPlay play)
//     {
//         int totalDamage = (int)((CalculatedDamageVar)DynamicVars["totalDamage"]).Calculate(play.Target);
//
//         await DamageCmd.Attack(totalDamage)
//             .FromCard(this)
//             .Targeting(play.Target)
//             .WithHitVfxNode((target) =>
//             {
//                 NCreature creatureNode = target.GetCreatureNode();
//                 if (creatureNode == null) return null;
//
//                 // Instantiate the actual StS2 Doom VFX node class
//                 NDoomVfx doomVfx = NDoomVfx.Create(
//                     creatureNode.Visuals, 
//                     creatureNode.Hitbox.GlobalPosition, 
//                     creatureNode.Hitbox.Size, 
//                     shouldDie: false // false so it only plays the hit impact without killing/removing the creature
//                 );
//
//                 if (doomVfx != null)
//                 {
//                     doomVfx.Modulate = new Color("#721e1e"); // Tint deep red
//                 }
//
//                 return doomVfx;
//             })
//             .Execute(choiceContext);
//     }
//
//     protected override void OnUpgrade()
//     {
//         RemoveKeyword(CardKeyword.Exhaust);
//     }
// }