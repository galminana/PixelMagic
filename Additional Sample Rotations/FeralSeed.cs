// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class FeralSeed : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "Feral Seed Rotation";
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
            Log.Write("Welcome to Feral Seed", Color.Green);
        }

        public override void Stop()
        {
            // Move pet to me
           // WoW.SendKeyAtLocation(WoW.Keys.Z, 900, 500);   // Pet Passive and Move To = /petpassive /petmoveto
        }

        public override void Pulse()
        {   
			if (combatRoutine.Type == RotationType.SingleTarget)  // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy)
                {
                    if (!WoW.IsSpellOnCooldown("Swipe"))
                    {
                        if (WoW.CanCast("Tigers Fury", true, true, false, false)) // Wtf
                        {
                            WoW.CastSpellByName("Tigers Fury");
                            return;
                        }

                        if (WoW.CanCast("Healing Touch", true, false, false, false) && WoW.CurrentComboPoints >= 2 && WoW.HasBuff("PredatorySwiftness") && !WoW.HasBuff("Bloodtalons")) //
                        {
                            WoW.CastSpellByName("Healing Touch");
                            return;
                        }

                        if (WoW.CanCast("Ferocious Bite", true, false, true, false) && WoW.CurrentComboPoints >= 5 && WoW.Energy >= 25 && (WoW.TargetHealthPercent <= 25 || WoW.GetDebuffTimeRemaining("Rip") >= 10)) // 
                        {
                            WoW.CastSpellByName("Ferocious Bite");
                            return;
                        }

                        if (WoW.CanCast("Rake", true, false, true, false) && WoW.Energy >= 35 && (!WoW.HasDebuff("Rake") || WoW.GetDebuffTimeRemaining("Rake") <= 3)) // Rake
                        {
                            WoW.CastSpellByName("Rake");
                            return;
                        }

                        if (WoW.CanCast("Rip", true, false, true, false) && WoW.Energy >= 30 && WoW.CurrentComboPoints >= 5 && (!WoW.HasDebuff("Rip") || (WoW.GetDebuffTimeRemaining("Rip") <= 4.7)))  // 
                        {
                            WoW.CastSpellByName("Rip");
                            return;
                        }

                        if (WoW.CanCast("Shred", true, true, true, false) && WoW.Energy >= 40 && WoW.CurrentComboPoints <= 5)  // 
                        {
                            WoW.CastSpellByName("Shred");
                            return;
                        }
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                // Do AOE Stuff here

                // Log.Write("Has Aura: " + WoW.HasAura("Furious Howl"));
                // Log.Write("Aura Count: " + WoW.GetAuraCount("Furious Howl"));
                // Log.Write("Aura Count: " + WoW.GetAuraCount("Taste for Blood"));
            }            
        }

        public override Form SettingsForm { get; set; }
    }
}

/*
[AddonDetails.db]
AddonAuthor=ThomasTrainWoop
AddonName=Engine
WoWVersion=Legion - 70000
[SpellBook.db]
Spell,5217,Tigers Fury, D1
Spell,5185,Healing Touch, D0
Spell,22568,Ferocious Bite, D7
Spell,1079,Rip, D6
Spell,5221,Shred, D5
Spell,1822,Rake, D4
Spell,106830,Thrash, D9
Spell,106785,Swipe, D8

Aura,145152,Bloodtalons
Aura,102543,Incarnation
Aura,135700,Clearcasting
Aura,5217,Tigers Fury
Aura,69369,PredatorySwiftness
Aura,1079,Rip
Aura,155722,Rake
*/

