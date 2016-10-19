// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class Balance : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "Balance Rotation";
            }
        }

        public override string Class
        {
            get
            {
                return "Druid";
            }
        } 

        public override void Initialize()
        {
            Log.Write("Welcome to Balance rotation", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)
            {
                if (WoW.HasTarget&& WoW.TargetIsEnemy&&WoW.HasBuff("Moonkin")&&!WoW.IsMoving)
                {	
					if(WoW.IsSpellInRange("Moonfire")&&WoW.CanCast("Moonfire")&&!WoW.HasDebuff("Moonfire"))
					{
						WoW.CastSpellByName("Moonfire");
						return;
					}
					if(WoW.IsSpellInRange("Sunfire")&&WoW.CanCast("Sunfire")&&!WoW.HasDebuff("Sunfire"))
					{
						WoW.CastSpellByName("Sunfire");
						return;
					}
					if(WoW.IsSpellInRange("LStrike")&&WoW.CanCast("LStrike")&&WoW.GetBuffStacks("LunarEmp")==3&&WoW.CurrentAstralPower<=80)
					{
						WoW.CastSpellByName("LStrike");
						return;
					}
					if((WoW.IsSpellInRange("SolarW")&&WoW.CanCast("SolarW")&&WoW.HasBuff("SolarEmp")&&WoW.CurrentAstralPower<=80)||(WoW.IsSpellInRange("SolarW")&&
					WoW.CurrentAstralPower<=40&&WoW.GetSpellCharges("Moon")<1&&WoW.GetSpellCharges("HalfMoon")<1&&WoW.GetSpellCharges("FullMoon")<1&&WoW.GetBuffStacks("LunarEmp")<=2))
					{
						WoW.CastSpellByName("SolarW");
						return;
					}
					if(WoW.IsSpellInRange("StarSurge")&&WoW.CanCast("StarSurge")&&WoW.CurrentAstralPower>=40&&!(WoW.GetBuffStacks("LunarEmp")==3)&&!(WoW.GetBuffStacks("SolarEmp")==3))
					{
						WoW.CastSpellByName("StarSurge");
						return;
					}
					if(WoW.IsSpellInRange("Moon")&&WoW.CanCast("Moon")&&WoW.CurrentAstralPower<=60&&WoW.HasDebuff("Moonfire")&&WoW.HasDebuff("Sunfire"))
					{
						WoW.CastSpellByName("Moon");
						return;
					}
                }
				if (WoW.HasTarget&& WoW.TargetIsEnemy&&WoW.HasBuff("Moonkin")&&WoW.IsMoving)
				{
					if(WoW.IsSpellInRange("Moonfire")&&WoW.CanCast("Moonfire")&&WoW.CurrentAstralPower<=40)
					{
						WoW.CastSpellByName("Moonfire");
						return;
					}
					if(WoW.IsSpellInRange("StarSurge")&&WoW.CanCast("StarSurge")&&WoW.CurrentAstralPower>=40)
					{
						WoW.CastSpellByName("StarSurge");
						return;
					}
					if(WoW.IsSpellInRange("Sunfire")&&WoW.CanCast("Sunfire")&&!WoW.HasDebuff("Sunfire"))
					{
						WoW.CastSpellByName("Sunfire");
						return;
					}
				}
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget&& WoW.TargetIsEnemy&&WoW.HasBuff("Moonkin")&&!WoW.IsMoving)
                {	
					if(WoW.CanCast("Moonfire") && !WoW.HasDebuff("Moonfire"))
					{
						WoW.CastSpellByName("Moonfire");
						return;
					}
					if(WoW.CanCast("Sunfire") && !WoW.HasDebuff("Sunfire"))
					{
						WoW.CastSpellByName("Sunfire");
						return;
					}
					if(WoW.CanCast("SolarW")&&WoW.GetBuffStacks("SolarEmp") == 3 && WoW.CurrentAstralPower <= 80)
					{
						WoW.CastSpellByName("SolarW");
						return;
					}
					if((WoW.CanCast("LStrike") && WoW.HasBuff("LunarEmp") && WoW.CurrentAstralPower <= 80)||(
					WoW.CurrentAstralPower<=40 && WoW.GetSpellCharges("Moon") < 1 && WoW.GetSpellCharges("HalfMoon") < 1 && WoW.GetSpellCharges("FullMoon") < 1 && WoW.GetBuffStacks("SolarEmp")<=2))
					{
						WoW.CastSpellByName("LStrike");
						return;
					}
					if(WoW.CanCast("StarSurge") && WoW.CurrentAstralPower>=40 && !(WoW.GetBuffStacks("LunarEmp")==3) && !(WoW.GetBuffStacks("SolarEmp")==3))
					{
						WoW.CastSpellByName("StarSurge");
						return;
					}
					if(WoW.CanCast("Moon")&&WoW.CurrentAstralPower<=60&&WoW.HasDebuff("Sunfire")&&WoW.HasDebuff("Moonfire"))
					{
						WoW.CastSpellByName("Moon");
						return;
					}
                }
				if (WoW.HasTarget&& WoW.TargetIsEnemy&&WoW.HasBuff("Moonkin") && WoW.IsMoving)
				{
					if(WoW.IsSpellInRange("Moonfire")&&WoW.CanCast("Moonfire") && WoW.CurrentAstralPower<=40)
					{
						WoW.CastSpellByName("Moonfire");
						return;
					}
					if(WoW.IsSpellInRange("StarSurge")&&WoW.CanCast("StarSurge") && WoW.CurrentAstralPower>=40)
					{
						WoW.CastSpellByName("StarSurge");
						return;
					}
					if(WoW.IsSpellInRange("Sunfire")&&WoW.CanCast("Sunfire") && !WoW.HasDebuff("Sunfire"))
					{
						WoW.CastSpellByName("Sunfire");
						return;
					}
				}
            }
        }


              

        public override Form SettingsForm { get; set; }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Dani
AddonName=dreamweaver
WoWVersion=Legion - 70000
[SpellBook.db]
Spell,8921,Moonfire,U
Spell,93402,Sunfire,I
Spell,202767,Moon,K
Spell,78674,StarSurge,J
Spell,194153,LStrike,D2
Spell,190984,SolarW,D1
Spell,202771,FullMoon,Y
Spell,202768,HalfMoon,Y

Aura,164547,LunarEmp
Aura,164545,SolarEmp
Aura,164812,Moonfire
Aura,93402,Sunfire
Aura,24858,Moonkin
*/

