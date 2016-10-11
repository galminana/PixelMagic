// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class Feral : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "Feral Rotation";
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
            Log.Write("Welcome to Feral rotation", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)
            {
                if (WoW.HasTarget&& WoW.TargetIsEnemy&&WoW.HasBuff("Cat Form"))
                {
                        

                        if (WoW.CanCast("Healing Touch") && WoW.HasBuff("PredatorySwiftness")&&WoW.HealthPercent<=80)
                        {
                            WoW.CastSpellByName("Healing Touch");
                            return;
                        }

                        if ((WoW.IsSpellInRange("Ferocious Bite")&&WoW.CanCast("Ferocious Bite") && WoW.CurrentComboPoints >= 5 && WoW.HasDebuff("Rip"))&&WoW.GetDebuffTimeRemaining("Rip") >= 5&&(WoW.Energy >= 50||WoW.HasBuff("Incarnation")||WoW.HasBuff("Berserk")))
                        {
                            WoW.CastSpellByName("Ferocious Bite");
                            return;
                        }

                        if ((WoW.IsSpellInRange("Rake")&&WoW.CanCast("Rake") && (!WoW.HasDebuff("Rake") || WoW.GetDebuffTimeRemaining("Rake") <= 3))&&(WoW.Energy >= 35||WoW.HasBuff("Incarnation")||WoW.HasBuff("Berserk")))
                        {
                            WoW.CastSpellByName("Rake");
                            return;
                        }
						if (WoW.IsSpellInRange("Frenesi")&&WoW.CanCast("Frenesi") && !WoW.IsSpellOnCooldown("Frenesi")&& WoW.CurrentComboPoints <=2)
                        {
                            WoW.CastSpellByName("Frenesi");
                            return;
                        }

                        if ((WoW.IsSpellInRange("Rip")&&WoW.CanCast("Rip") && WoW.CurrentComboPoints >= 5 && (!WoW.HasDebuff("Rip") || WoW.GetDebuffTimeRemaining("Rip") <= 5))&&(WoW.Energy >= 30||WoW.HasBuff("Incarnation")||WoW.HasBuff("Berserk")))
                        {
                            WoW.CastSpellByName("Rip");
                            return;
                        }

                        if ((WoW.IsSpellInRange("Shred")&&WoW.CanCast("Shred") && WoW.CurrentComboPoints < 5&&WoW.HasDebuff("Rake")&&WoW.IsSpellOnCooldown("Brutal Slash"))&&(WoW.Energy >= 40||WoW.HasBuff("Incarnation")||WoW.HasBuff("Berserk")))
                        {
                            WoW.CastSpellByName("Shred");
                            return;
                        }
						if (WoW.CanCast("Tigers Fury") && WoW.Energy<=20&&!WoW.IsSpellOnCooldown("Tigers Fury"))
                        {
                            WoW.CastSpellByName("Tigers Fury");
                            return;
                        }
						if ((WoW.IsSpellInRange("Shred")&&WoW.CanCast("Brutal Slash") && !WoW.IsSpellOnCooldown("Brutal Slash"))&&(WoW.Energy>=20||WoW.HasBuff("Incarnation")||WoW.HasBuff("Berserk")))
                        {
                            WoW.CastSpellByName("Brutal Slash");
                            return;
                        }
						
                    }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget&& WoW.TargetIsEnemy&&WoW.HasBuff("Cat Form"))
                {
                        

                        if (WoW.CanCast("Healing Touch") && WoW.HasBuff("PredatorySwiftness")&&WoW.HealthPercent<=80)
                        {
                            WoW.CastSpellByName("Healing Touch");
                            return;
                        }

                        if ((WoW.IsSpellInRange("Ferocious Bite")&&WoW.CanCast("Ferocious Bite") && WoW.CurrentComboPoints >= 5 && WoW.HasDebuff("Rip"))&&WoW.GetDebuffTimeRemaining("Rip") >= 5&&(WoW.Energy >= 50||WoW.HasBuff("Incarnation")||WoW.HasBuff("Berserk")))
                        {
                            WoW.CastSpellByName("Ferocious Bite");
                            return;
                        }

                        if ((WoW.IsSpellInRange("Rake")&&WoW.CanCast("Rake") && (!WoW.HasDebuff("Rake") || WoW.GetDebuffTimeRemaining("Rake") <= 3))&&(WoW.Energy >= 35||WoW.HasBuff("Incarnation")||WoW.HasBuff("Berserk")))
                        {
                            WoW.CastSpellByName("Rake");
                            return;
                        }
						if (WoW.IsSpellInRange("Frenesi")&&WoW.CanCast("Frenesi") && !WoW.IsSpellOnCooldown("Frenesi")&& WoW.CurrentComboPoints <=2)
                        {
                            WoW.CastSpellByName("Frenesi");
                            return;
                        }

                        if ((WoW.IsSpellInRange("Rip")&&WoW.CanCast("Rip") && WoW.CurrentComboPoints >= 5 && (!WoW.HasDebuff("Rip") || WoW.GetDebuffTimeRemaining("Rip") <= 5))&&(WoW.Energy >= 30||WoW.HasBuff("Incarnation")||WoW.HasBuff("Berserk")))
                        {
                            WoW.CastSpellByName("Rip");
                            return;
                        }

                        if ((WoW.IsSpellInRange("Shred")&&WoW.CanCast("Shred") && WoW.CurrentComboPoints < 5&&WoW.HasDebuff("Rake")&&WoW.IsSpellOnCooldown("Brutal Slash"))&&(WoW.Energy >= 40||WoW.HasBuff("Incarnation")||WoW.HasBuff("Berserk")))
                        {
                            WoW.CastSpellByName("Shred");
                            return;
                        }
						if (WoW.CanCast("Tigers Fury") && WoW.Energy<=40&&!WoW.IsSpellOnCooldown("Tigers Fury"))
                        {
                            WoW.CastSpellByName("Tigers Fury");
                            return;
                        }
						if ((WoW.IsSpellInRange("Shred")&&WoW.CanCast("Brutal Slash") && !WoW.IsSpellOnCooldown("Brutal Slash"))&&(WoW.Energy>=20||WoW.HasBuff("Incarnation")||WoW.HasBuff("Berserk")))
                        {
                            WoW.CastSpellByName("Brutal Slash");
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
Spell,5217,Tigers Fury,F8
Spell,5185,Healing Touch,Y
Spell,22568,Ferocious Bite,X
Spell,1079,Rip,D4
Spell,5221,Shred,D2
Spell,1822,Rake,D1
Spell,202028,Brutal Slash,I
Spell,210722,Frenesi,R

Aura,768,Cat Form
Aura,102543,Incarnation
Aura,135700,Clearcasting
Aura,5217,Tigers Fury
Aura,106830,Thrash
Aura,69369,PredatorySwiftness
Aura,1079,Rip
Aura,155722,Rake
Aura,106951,Berserk
*/

