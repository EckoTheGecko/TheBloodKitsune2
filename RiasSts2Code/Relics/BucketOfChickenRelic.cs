// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Relics.Girya
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 97F10687-C306-4798-AB75-8B9F23F34DFB
// Assembly location: c:\program files (x86)\steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll
// XML documentation location: c:\program files (x86)\steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.xml

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;
using RiasSts2.RiasSts2Code.Relics;
using RiasSts2.RiasSts2Code.RestSite;

namespace MegaCrit.Sts2.Core.Models.Relics;



public sealed class BucketOfChickenRelic : RiasSts2Relic
{
  private int timesEaten;
  public const int maxEats = 3;

  public override RelicRarity Rarity => RelicRarity.Ancient;

  public override bool ShowCounter => true;

  public override int DisplayAmount => TimesEaten;

  [SavedProperty]
  public int TimesEaten
  {
    get => timesEaten;
    set
    {
      AssertMutable();
      timesEaten = value;
      InvokeDisplayAmountChanged();
    }
  }

  public override async Task AfterRoomEntered(AbstractRoom room)
  {
    if (timesEaten <= 0 || !(room is CombatRoom))
      return;
    Flash();
    DexterityPower dexterityPower = await PowerCmd.Apply<DexterityPower>(new ThrowingPlayerChoiceContext(), Owner.Creature, timesEaten, Owner.Creature, null);
  }

  public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
  {
    if (player != Owner || timesEaten >= 3)
      return false;
    options.Add(new EatChickenRestSiteOption(player));
    return true;
  }
}
