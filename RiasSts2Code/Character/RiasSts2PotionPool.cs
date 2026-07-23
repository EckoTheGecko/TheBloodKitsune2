using BaseLib.Abstracts;
using RiasSts2.RiasSts2Code.Extensions;
using Godot;

namespace RiasSts2.RiasSts2Code.Character;

public class RiasSts2PotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => RiasSts2.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}