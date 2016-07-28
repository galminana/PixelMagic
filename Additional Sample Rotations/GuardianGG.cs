// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class GuardianGG : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "Guardian GG Rotation";
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
                        // mangle>moonfire (with GG proc)>thrash> moonfire (less than 3 seconds)>swipe
                        if (WoW.CanCast("Mangle")) // Mangle on CD 
                        {
                            WoW.CastSpellByName("Mangle");
                            return;
                        }

                        if (WoW.HasBuff("GalacticGuardian"))// Mangle on CD 
                        {
                            WoW.CastSpellByName("Moonfire");
                            return;
                        }

                        if (WoW.CanCast("Thrash") && WoW.IsSpellInRange("Mangle")) // Mangle on CD 
                        {
                            WoW.CastSpellByName("Thrash");
                            return;
                        }

                        if (WoW.GetDebuffTimeRemaining("Moonfire") <= 3 || !WoW.HasDebuff("Moonfire")) // Mangle on CD 
                        {
                            WoW.CastSpellByName("Moonfire");
                            return;
                        }

                        if (WoW.IsSpellInRange("Mangle")) // Mangle on CD 
                        {
                            WoW.CastSpellByName("Swipe");
                            return;
                        }
                    }


                    if (WoW.Rage >= 80 && WoW.CanCast("Maul"))
                    {
                        WoW.CastSpellByName("Maul");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (!WoW.IsSpellOnCooldown("Swipe"))
                {   

                    if (WoW.CanCast("Thrash") && WoW.IsSpellInRange("Mangle")) // Mangle on CD 
                    {
                        WoW.CastSpellByName("Thrash");
                        return;
                    }

                    if (WoW.IsSpellInRange("Mangle")) // Mangle on CD 
                    {
                        WoW.CastSpellByName("Swipe");
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
AddonAuthor=ThomasTrainWoop
AddonName=Engine
WoWVersion=70000
[SpellBook.db]
Spell,33917,Mangle, D1
Spell,77758,Thrash, D2
Spell,6807,Maul, D3
Spell,213771,Swipe, D4
Spell,8921,Moonfire, D5


Aura,213708,GalacticGuardian
Aura,192090,Thrash
Aura,164812,Moonfire

*/

