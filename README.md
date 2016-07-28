# Pixel Magic - where colour unlocks the World of Warcraft.

Developer and copyright owner: WiNiFiX<br>
Project stated on: January 2016<br>

**Last Readme Update: 2016.07.28**

**Latest Build:**<br>
For those of you whom don't know how to compile the source and run it you can download this version here then unzip the<br> 
"PixelMagic-OpenSource-master.zip" file and look in the "PixelMagic-OpenSource-master\PixelMagic\Builds" folder and run<br> 
the application "PixelMagic.exe"<br>

**Sample Rotations:**<br>
For a good working sample rotation (with Legion support) look at the DKUnholy.cs rotation file.<br>
For a sample rotation which shows you how to create custom configuration screens for your rotation look at the Warrior.cs<br>
rotation file.

---

**Website for FAQ:** [here](http://www.ownedcore.com/forums/world-of-warcraft/world-of-warcraft-bots-programs/wow-bots-questions-requests/542750-pixel-based-bot.html)<br>
**Official Discord:** [here](https://discord.gg/0rnM62Wx5pQp8tjT)<br>
*Instructions on how to use / test / build this version are on Discord along with support*

**Currently supports WoW Versions**
- Warlords of Dreanor
- Legion

**Sample screenshots**<br>
![Alt Text](http://i.imgur.com/1nplBST.png)
<br>
![Alt Text](http://i.imgur.com/478ZRTS.png)
<br>
**Sample ingame screenshot of the addon in action**
<br>
![Alt Text](http://i.imgur.com/4Afi2pp.jpg)
<br>
**Sample recount from Unholy DK in Legion**
<br>
![Alt Text](http://i.imgur.com/xicfSBl.jpg)

**How to setup your spellbook** (sample for basic hunter)<br>
![Alt Text](http://i.imgur.com/HGhFJve.png)

---

**License Agreement**<br>
The software is provided "as is", without warranty of any kind, express or implied, including<br>
but not limited to the warranties of merchantability, fitness for a particular purpose and<br>
noninfringement. In no event shall the authors or copyright holders be liable for any claim,<br>
damages or other liability, whether in an action of contract, tort or otherwise, arising from,<br>
out of or in connection with the software or the use or other dealings in the software.<br>

Anyone using / copying any part of the software must include this license<br>

**Icon backlink:** [HADezign](http://hadezign.com)

---

**Supported Commands in combat routines**<br>
```javascript
Returns true / false
 - WoW.HasTarget
 - WoW.PlayerIsCasting
 - WoW.TargetIsCasting
 - WoW.TargetIsVisible
 - WoW.TargetIsFriend
 - WoW.TargetIsEnemy
 - WoW.IsSpellOnCooldown(string spellBookSpellName)
 - WoW.IsSpellInRange(string spellBookSpellName)
 - WoW.CanCast(string spellBookSpellName, ...)
 - WoW.HasBuff(string buffName)
 - WoW.HasDebuff(string debuffName)
 
Returns int
 - WoW.CurrentRunes;
 - WoW.CurrentComboPoints;
 - WoW.CurrentSoulShards;
 - WoW.CurrentHolyPower;
 - WoW.HealthPercent;
 - WoW.TargetHealthPercent;
 - WoW.Power;
 - WoW.Focus;
 - WoW.Mana;
 - WoW.Energy;
 - WoW.Rage;
 - WoW.Fury;
 - WoW.RunicPower;
 - WoW.HasFocus;
 - WoW.GetBuffStacks(string auraName);
 - WoW.GetDebuffTimeRemaining(string debuffName);
 - WoW.GetDebuffStacks(string debuffName);
 - WoW.GetSpellCharges(string spellName);
 
Returns void
 - WoW.CastSpellByName(string spellBookSpellName)
 - Log.Write(string message, System.Drawing.Color c)
 - WoW.SendKeyAtLocation(Keys key, int x, int y)
 - WoW.SendMacro(string macro)
 
Rotation Types
 - combatRoutine.Type = RotationType.SingleTarget or RotationType.AOE or RotationType.SingleTargetCleave 
```

---

**Sample Combat Routine**<br>
```javascript
// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class DKUnholy : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "DK Unholy Rotation";
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
            Log.Write("Welcome to DK Unholy", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse()        // Updated for Legion (tested and working for single target)
        {
            if (combatRoutine.Type == RotationType.SingleTarget)  // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy)
                {
                    if (!WoW.HasDebuff("Virulent Plague") && WoW.CurrentRunes >= 1 && WoW.CanCast("Outbreak", true, false, true, false, true))
                    {
                        WoW.CastSpellByName("Outbreak");
                    }
                    if (WoW.CanCast("Dark Transformation", true, true, true, false, true))
                    {
                        WoW.CastSpellByName("Dark Transformation");
                    }
                    if ((WoW.CanCast("Death Coil", true, true, false, false, true) && (WoW.RunicPower >= 80)) || 
                        (WoW.HasBuff("Sudden Doom") && WoW.IsSpellOnCooldown("Dark Arbiter")))
                    {
                        WoW.CastSpellByName("Death Coil");
                    }
                    if (WoW.CanCast("Festering Strike", true, true, true, false, true) && WoW.GetDebuffStacks("Festering Wound") <= 4)
                    {
                        WoW.CastSpellByName("Festering Strike");
                    }
                    if (WoW.CanCast("Clawing Shadows", true, true, false, false, true) && WoW.CurrentRunes >= 3)
                    {
                        WoW.CastSpellByName("Clawing Shadows");
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                // Do AOE stuff here
            }
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                // Do Single Target Cleave stuff here if applicable else ignore this one
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
Spell,85948,Festering Strike,D1
Spell,77575,Outbreak,D2
Spell,207311,Clawing Shadows,D3
Spell,47541,Death Coil,D4
Spell,194918,Blighted Rune Weapon,D5
Spell,63560,Dark Transformation,D6
Spell,207349,Dark Arbiter,Q
Aura,81340,Sudden Doom
Aura,194310,Festering Wound
Aura,191587,Virulent Plague
*/
```
