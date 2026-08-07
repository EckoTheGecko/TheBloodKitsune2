using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Runs;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Relics;

namespace RiasSts2.RiasSts2Code.Ancient;


public class Inari : CustomAncientModel
{
    protected override OptionPools MakeOptionPools => new OptionPools(
        
            [AncientOption<BucketOfChickenRelic>()],
            
            [AncientOption<Lantern>()],
            
            GetClassSpecificPool3()
            
            //more relic options
        
    );
    
    public override bool IsValidForAct(ActModel act)
    {
        return act.ActNumber() == 2;
    }


    private WeightedList<AncientOption> GetClassSpecificPool3()
    {
        CharacterModel characterModel = this.Owner.Character; //get current class

        // switch to correct class and return related relic
        switch (characterModel)
        {
            case Ironclad:
                return [AncientOption<YummyCookie>()];

            case Silent:
                return [AncientOption<OldCoin>()];

            case Defect:
                return [AncientOption<GamblingChip>()];

            case Necrobinder:
                return [AncientOption<RedSkull>()];

            case Regent:
                return [AncientOption<SneckoEye>()];

            case Character.RiasSts2:
                return [AncientOption<TalismansRelic>()];

            default:
                // Fallback relic option if unhandled or null
                return [AncientOption<TalismansRelic>()];
        }
    }

    public override string CustomMapIconPath => "RiasSts2/images/ancient/inari_map_icon.png";
    public override string CustomMapIconOutlinePath => "RiasSts2/images/ancient/inari_map_icon_outline.png";
    public override string CustomScenePath => "RiasSts2/images/ancient/inari_scene.tscn";
    public override string CustomRunHistoryIconPath => "RiasSts2/images/ancient/inari_run_history.png";
    public override string CustomRunHistoryIconOutlinePath => "RiasSts2/images/ancient/inari_run_history_outline.png";
}