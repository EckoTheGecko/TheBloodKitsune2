using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using RiasSts2.RiasSts2Code.Relics;

namespace RiasSts2.RiasSts2Code.Ancient;


public class Inari : CustomAncientModel
{
    protected override OptionPools MakeOptionPools => new OptionPools(
        
            [AncientOption<Nunchaku>()],
            
            [AncientOption<Lantern>()],
            
            [AncientOption<TalismansRelic>()]
            
            //more relic options
        
    );
    
    public override bool IsValidForAct(ActModel act)
    {
        return act.ActNumber() == 2;
    }

    public override string CustomMapIconPath => "RiasSts2/images/ancient/inari_map_icon.png";
    public override string CustomMapIconOutlinePath => "RiasSts2/images/ancient/inari_map_icon_outline.png";
    public override string CustomScenePath => "RiasSts2/images/ancient/inari_scene.tscn";
    public override string CustomRunHistoryIconPath => "RiasSts2/images/ancient/inari_run_history.png";
    public override string CustomRunHistoryIconOutlinePath => "RiasSts2/images/ancient/inari_run_history_outline.png";
}