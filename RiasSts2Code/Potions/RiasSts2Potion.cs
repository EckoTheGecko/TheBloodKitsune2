using BaseLib.Abstracts;
using BaseLib.Utils;
using RiasSts2.RiasSts2Code.Character;

namespace RiasSts2.RiasSts2Code.Potions;

[Pool(typeof(RiasSts2PotionPool))]
public abstract class RiasSts2Potion : CustomPotionModel;