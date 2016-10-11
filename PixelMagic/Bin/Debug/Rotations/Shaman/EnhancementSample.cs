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
    public class ShamanEnhancement : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "Enhancement Sample";
            }
        }

        public override string Class
        {
            get
            {
                return "Shaman";
            }
        }

        public override void Initialize()
        {
            Log.Write("Welcome to Enhancement Sample", Color.Green);
        }

        public override void Stop()
        {
            
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)  // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy)
                {
                    if (WoW.CanCast("Stormstrike", true, true, false, false))
                    {
                        WoW.CastSpellByName("Stormstrike");
                    }

                    if (WoW.CanCast("Boulderfist", true, true, false, false) && !WoW.HasBuff("Boulderfist"))
                    {
                        WoW.CastSpellByName("Boulderfist");
                    }

                    if (WoW.CanCast("Flametounge", true, true, false, false))
                    {
                        WoW.CastSpellByName("Flametounge");
                    }

                    if (WoW.CanCast("Crash Lightning", true, true, false, false) && WoW.Power > 80)
                    {
                        WoW.CastSpellByName("Crash Lightning");
                    }

                    if (WoW.CanCast("Lava Lash", true, true, false, false) && WoW.Power > 90)
                    {
                        WoW.CastSpellByName("Lava Lash");
                    }

                    if (WoW.CanCast("Boulderfist", true, true, false, false))
                    {
                        WoW.CastSpellByName("Boulderfist");
                    }
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
Spell,196884,Feral Lunge,D1
Spell,51533,Feral Spirit, D2
Spell,196834,Frostbrand, D3
Spell,187874,Crash Lightning, D4
Spell,193796,Flametounge, D5
Spell,201897,Boulderfist, D6
Spell,60103,Lava Lash, D7
Spell,17364,Stormstrike, D8
Spell,187837,Lightning Bolt, D9
Aura,187878,Crashing Storm
Aura,218825,Boulderfist
*/

