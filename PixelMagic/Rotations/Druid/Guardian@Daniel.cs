// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class Guardian : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "Guardian Rotation";
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
            Log.Write("Welcome Guardian", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy&&WoW.HasBuff("Bear Form"))
                {
					if (WoW.IsSpellInRange("Mangle")&&WoW.CanCast("Mangle")&&(!(WoW.GetDebuffStacks("Thrash") <=2)||WoW.IsSpellOnCooldown("Thrash")))
                    {
                        WoW.CastSpellByName("Mangle");
                        return;
                    }
					if (WoW.CanCast("Swipe")&&WoW.IsSpellOnCooldown("Mangle")&&
					(!(WoW.GetDebuffTimeRemaining("Thrash") <= 2)||!(WoW.GetDebuffStacks("Thrash") <=2)))
                    {
                        WoW.CastSpellByName("Swipe");
                        return;
                    }
					if (WoW.IsSpellInRange("Moonfire")&&WoW.CanCast("Moonfire")&&!WoW.HasDebuff("Moonfire")||WoW.HasBuff("Galactic Guardian"))
                    {
                        WoW.CastSpellByName("Moonfire");
                        return;
                    }
					if (WoW.IsSpellInRange("growl")&&WoW.CanCast("growl")&&!WoW.HasDebuff("Intimidated"))
                    {
                        WoW.CastSpellByName("growl");
                        return;
                    }
					if (WoW.CanCast("Frenzied Regeneration") && WoW.HealthPercent <=85&& !WoW.HasBuff("Frenzied Regeneration"))
                    {
                        WoW.CastSpellByName("Frenzied Regeneration");
						return;
                    }
					if (WoW.CanCast("Ironfur")&& (WoW.Rage >= 45))
                    {
                        WoW.CastSpellByName("Ironfur");
						return;
                    }
					if (WoW.CanCast("Thrash") && WoW.GetDebuffStacks("Thrash") <3 || WoW.GetDebuffTimeRemaining("Thrash") <3)
                    {
                        WoW.CastSpellByName("Thrash");
                        return;
                    }
					

                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy&&WoW.HasBuff("Bear Form"))
                {
                    if (WoW.IsSpellInRange("Mangle")&&WoW.CanCast("Mangle")&&(!(WoW.GetDebuffStacks("Thrash") <=2)||WoW.IsSpellOnCooldown("Thrash")))
                    {
                        WoW.CastSpellByName("Mangle");
                        return;
                    }
					if (WoW.CanCast("Swipe")&&WoW.IsSpellOnCooldown("Mangle")&&
					(!(WoW.GetDebuffTimeRemaining("Thrash") <= 2)||!(WoW.GetDebuffStacks("Thrash") <=2)))
                    {
                        WoW.CastSpellByName("Swipe");
                        return;
                    }
					if (WoW.IsSpellInRange("Moonfire")&&WoW.CanCast("Moonfire")&&!WoW.HasDebuff("Moonfire")||WoW.HasBuff("Galactic Guardian"))
                    {
                        WoW.CastSpellByName("Moonfire");
                        return;
                    }
					if (WoW.IsSpellInRange("growl")&&WoW.CanCast("growl")&&!WoW.HasDebuff("Intimidated"))
                    {
                        WoW.CastSpellByName("growl");
                        return;
                    }
					if (WoW.CanCast("Frenzied Regeneration") && WoW.HealthPercent <=85&& !WoW.HasBuff("Frenzied Regeneration"))
                    {
                        WoW.CastSpellByName("Frenzied Regeneration");
						return;
                    }
					if (WoW.CanCast("Thrash") && WoW.GetDebuffStacks("Thrash") <3 || WoW.GetDebuffTimeRemaining("Thrash") <3)
                    {
                        WoW.CastSpellByName("Thrash");
                        return;
                    }
					
					if (WoW.CanCast("Ironfur")&&WoW.HasBuff("Mark of Ursol")&& (WoW.Rage >= 45))
                    {
                        WoW.CastSpellByName("Ironfur");
						return;
                    }
					if (WoW.CanCast("Mark of Ursol")&& (WoW.Rage >= 45) && !WoW.HasBuff("Mark of Ursol"))
                    {
                        WoW.CastSpellByName("Mark of Ursol");
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
Spell,77758,Thrash, D1
Spell,210037,growl,D5
Spell,80313,Pulverize,X
Spell,33917,Mangle,D2
Spell,164812,Moonfire,I
Spell,106785,Swipe, D4
Spell,192081,Ironfur,U
Spell,192083,Mark of Ursol,Y
Spell,22842,Frenzied Regeneration,K
Buff,22842,Frenzied Regeneration
Buff,192083,Mark of Ursol
Buff,192081,Ironfur
Debuff,77758,Thrash
Debuff,164812,Moonfire
Debuff,206891,Intimidated
Buff,203964,Galactic Guardian
Buff,5487,Bear Form
*/

