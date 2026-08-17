using Godot;
using HarmonyLib;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using RiasSts2.RiasSts2Code.Powers;
using System.Collections.Generic;

namespace RiasSts2.Patches;

[HarmonyPatch(typeof(NHealthBar), "RefreshText")]
public static class NHealthBarPatch
{
    // Updated requested colors
    private static readonly Color TalismanLethalFontColor = new Color("ff8ba9");    // Light Rosy Pink
    private static readonly Color TalismanLethalOutlineColor = new Color("62192c"); // Deep Wine/Maroon
    private static readonly Color TalismanLethalBarColor = new Color("a52240");     // Crimson/Berry Red

    public static void Postfix(NHealthBar __instance)
    {
        var traverse = Traverse.Create(__instance);

        var creature = traverse.Field("_creature").GetValue<Creature>();
        var hpLabel = traverse.Field("_hpLabel").GetValue<MegaLabel>();

        if (creature == null || hpLabel == null || creature.CurrentHp <= 0)
            return;

        int pendingTalismanDamage = GetPendingTalismanDamage(creature);

        // Check if Talisman damage exceeds total effective health (HP + Block)
        if (IsTalismanLethal(creature, pendingTalismanDamage))
        {
            // 1. Override Font & Outline colors using STS2 ThemeConstants pattern
            hpLabel.AddThemeColorOverride(ThemeConstants.Label.FontColor, TalismanLethalFontColor);
            hpLabel.AddThemeColorOverride(ThemeConstants.Label.FontOutlineColor, TalismanLethalOutlineColor);
            
            // 2. Tint the HP Foreground Texture
            var hpForeground = traverse.Field("_hpForeground").GetValue<Control>();
            if (hpForeground != null)
            {
                hpForeground.SelfModulate = TalismanLethalBarColor;
            }
        }
    }

    private static bool IsTalismanLethal(Creature target, int damage)
    {
        if (damage <= 0)
            return false;

        // Effective HP includes current Block since Talismans hit Block first
        int totalEffectiveHp = target.CurrentHp + target.Block;
        return damage >= totalEffectiveHp;
    }

    private static int GetPendingTalismanDamage(Creature targetEnemy)
    {
        if (targetEnemy.CombatState == null)
            return 0;

        IEnumerable<Creature> opposingCreatures = targetEnemy.IsEnemy 
            ? targetEnemy.CombatState.Allies 
            : targetEnemy.CombatState.HittableEnemies;

        int totalDamage = 0;

        foreach (var ally in opposingCreatures)
        {
            var talisman = ally.GetPower<TalismanPower>();
            if (talisman != null && talisman.Amount > 0)
            {
                int damagePerTalisman = talisman.DynamicVars.Damage.IntValue;
                int baseTalismanDamage = talisman.Amount * damagePerTalisman;

                // Check for DemonBloodPower stack count
                var demonBlood = ally.GetPower<DemonBloodPower>();
                int multipliers = 1 + (demonBlood != null ? demonBlood.Amount : 0);

                totalDamage += baseTalismanDamage * multipliers;
            }
        }

        return totalDamage;
    }
}