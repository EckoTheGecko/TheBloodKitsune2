using BaseLib.Abstracts;
using BaseLib.Patches.UI;
using BaseLib.Utils.NodeFactories;
using RiasSts2.RiasSts2Code.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Cards;
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
    public override int StartingHp => 75;

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

    // Paths using explicit forward slashes and res://
    public override string CustomIconTexturePath => "res://RiasSts2/images/charui/character_icon_char_rias.png";
    public override string CustomCharacterSelectIconPath => "res://RiasSts2/images/charui/char_select_rias.png";
    public override string CustomCharacterSelectLockedIconPath => "res://RiasSts2/images/charui/char_select_char_name_locked.png";
    public override string CustomMapMarkerPath => "res://RiasSts2/images/charui/map_marker_char_rias.png";
    public override string CustomIconOutlineTexturePath => "res://RiasSts2/images/charui/character_icon_outline_rias.png";
    public override string CustomIconPath => "res://RiasSts2/images/charui/sp_rias_icon.tscn";
    public override string CustomCharacterSelectBg => "res://RiasSts2/images/charui/RiasBG.tscn";
    public override string CustomVisualPath => "res://RiasSts2/images/animations/rias_anims.tscn";
    public override RelicIconData? CustomYummyCookie => new RelicIconData("res://RiasSts2/images/relics/big/yummy_cookie_relic_rias.png",
        "res://RiasSts2/images/relics/yummy_cookie_relic_rias.png",
        "res://RiasSts2/images/relics/yummy_cookie_relic_outline_rias.png"
    );

    public override string CustomMerchantAnimPath => "res://RiasSts2/images/charui/rias_static_merchant.tscn";
    public override string CustomRestSiteAnimPath => "res://RiasSts2/images/charui/rias_static_rest.tscn";
    
}