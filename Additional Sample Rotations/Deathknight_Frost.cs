// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.GUI;
using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class DKFrost : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "DK Frost Rotation";
            }
        }

        public override string Class
        {
            get
            {
                return "Deathknight";
            }
        }

        public override void Initialize()
        {
            Log.Write("Welcome to DK Frost", Color.Green);
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
                   // On GCD
                       if (!WoW.IsSpellOnCooldown("Frost Strike") && WoW.IsSpellInRange("Frost Strike"))
                    {
                        if (WoW.CanCast("Howling Blast", true, true, false, false) && !WoW.HasDebuff("Frost Fever"))
                        {
                            WoW.CastSpellByName("Howling Blast");
                        }

                        if (WoW.CanCast("Pillar of Frost", true, true, false, false) && WoW.TargetHealthPercent >= 30)
                        {
                            WoW.CastSpellByName("Pillar of Frost");
                        }

                        if (WoW.CanCast("Howling Blast", true, true, false, false) && WoW.HasBuff("Rime"))
                        {
                            WoW.CastSpellByName("Howling Blast");
                        }

                        if (WoW.CanCast("Frost Strike", true, true, false, false) && WoW.RunicPower >= 80)
                        {
                            WoW.CastSpellByName("Frost Strike");
                        }

                        if (WoW.CanCast("Glacial Advance", true, true, false, false, true) && WoW.CurrentRunes >= 1)
                        {
                            WoW.CastSpellByName("Glacial Advance");
                        }


                        if (WoW.CanCast("Obliterate", true, true, false, false) && WoW.HasBuff("Killing Machine") && !WoW.HasBuff("Rime") && WoW.CurrentRunes >= 2)
                        {
                            WoW.CastSpellByName("Obliterate");
                        }

                        if (WoW.CanCast("Obliterate", true, true, false, false) && WoW.CurrentRunes >= 4)
                        {
                            WoW.CastSpellByName("Obliterate");
                        }


                        if (WoW.CanCast("Frost Strike", true, true, false, false) && WoW.GetDebuffStacks("Razorice") >= 5 && WoW.CurrentRunes <= 3 && WoW.RunicPower >= 25)
                        {
                            WoW.CastSpellByName("Frost Strike");
                        }

                        if (WoW.CanCast("Obliterate", true, true, false, false) && WoW.CurrentRunes >= 2)
                        {
                            WoW.CastSpellByName("Obliterate");
                        }

                        if (WoW.CanCast("Frost Strike", true, true, false, false) && WoW.CurrentRunes == 0 && WoW.RunicPower >= 25)
                        {
                            WoW.CastSpellByName("Frost Strike");
                        }


                    }

                    //Off gcd
                    
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                
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
Spell,47568,Empower Rune Weapon,D5
Spell,49143,Frost Strike, D1
Spell,49184,Howling Blast, D2
Spell,49020,Obliterate, D3
Spell,51271,Pillar of Frost, D4
Spell,194913,Glacial Advance, D6

Aura,51714,Razorice
Aura,55095,Frost Fever
Aura,51271,Pillar of Frost
Aura,59052,Rime
Aura,51124,Killing Machine

*/

