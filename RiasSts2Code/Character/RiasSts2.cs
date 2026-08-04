using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using RiasSts2.RiasSts2Code.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using RiasSts2.RiasSts2Code.Cards;
using RiasSts2.RiasSts2Code.Relics;

namespace RiasSts2.RiasSts2Code.Character;

  

public class RiasSts2 : PlaceholderCharacterModel
{
    public const string CharacterId = "RiasSts2";

    public static readonly Color Color = new("721e1e");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 80;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<BloodClaws>(),
        ModelDb.Card<StrikeRias>(),
        ModelDb.Card<StrikeRias>(),
        ModelDb.Card<StrikeRias>(),
        ModelDb.Card<StrikeRias>(),
        ModelDb.Card<DefendRias>(),
        ModelDb.Card<DefendRias>(),
        ModelDb.Card<DefendRias>(),
        ModelDb.Card<DefendRias>(),
        ModelDb.Card<DefendRias>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<HiganbanaRelic>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<RiasSts2CardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<RiasSts2RelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<RiasSts2PotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "character_icon_char_rias.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_rias.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_rias.png".CharacterUiPath();

    public override string CustomIconOutlineTexturePath => "character_icon_outline_rias.png".CharacterUiPath();
    public override string CustomCharacterSelectBg => "RiasBG.tscn".CharacterUiPath();
    public override string CustomVisualPath => "RiasSts2/images/animations/rias_anims.tscn";
    
    
    // public override string? CustomRestSiteAnimPath => "rias_static_rest.tscn".CharacterUiPath();
    // public override string? CustomMerchantAnimPath => "rias_static_merchant.tscn".CharacterUiPath();
}