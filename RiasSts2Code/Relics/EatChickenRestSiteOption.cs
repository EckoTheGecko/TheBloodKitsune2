using System;
using System.Threading;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.RestSite;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using RiasSts2.RiasSts2Code.Relics;

namespace RiasSts2.RiasSts2Code.RestSite;

public class EatChickenRestSiteOption(Player owner) : CustomRestSiteOption(owner)
{
    // Reference for rest_site_ui.json 
    public override string OptionId => "EAT";
    
    //custom image path for the icon at the rest site
    public override string? CustomIconPath => "RiasSts2/images/events/option_eat_chicken.png";
    


    //  when chosen
    public override async Task<bool> OnSelect()
    {
        BucketOfChickenRelic relic = Owner.GetRelic<BucketOfChickenRelic>();
        if (relic != null)
        {
            relic.TimesEaten++;
        }
        return await Task.FromResult(true);
    }

    // VFX 
    public override Task DoLocalPostSelectVfx(CancellationToken ct = default)
    {
        NGame.Instance?.ScreenShake(ShakeStrength.Medium, ShakeDuration.Short);
        return Task.CompletedTask;
    }

    public override Task DoRemotePostSelectVfx()
    {
        NRestSiteRoom instance = NRestSiteRoom.Instance;
        NRestSiteCharacter characterNode = instance?.Characters.FirstOrDefault(c => c.Player == Owner);
        
        NRelicFlashVfx flashVfx = NRelicFlashVfx.Create((RelicModel)ModelDb.Relic<BucketOfChickenRelic>());
        if (flashVfx != null && characterNode != null)
        {
            characterNode.AddChildSafely((Node)flashVfx);
            flashVfx.Position = Vector2.Zero;
        }
        
        return Task.CompletedTask;
    }
}