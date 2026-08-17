using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Screens.Shops;
using Godot;

[HarmonyPatch(typeof(NMerchantCharacter), "_Ready")]
public static class NMerchantCharacter_BypassSpinePatch
{
    [HarmonyPrefix]
    public static bool Prefix(NMerchantCharacter __instance)
    {
        // If the bound node is not a Spine object (e.g. standard Node2D with AnimationPlayer),
        // skip vanilla NMerchantCharacter Spine initialization to prevent the crash.
        if (__instance is Node2D node && !node.HasMeta("IsSpine"))
        {
            // Optional: Start your AnimationPlayer here if present
            var animPlayer = node.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
            animPlayer?.Play("idle");

            return false; // Skip vanilla Spine binding code
        }

        return true; // Run normal Spine code for default merchants
    }
}