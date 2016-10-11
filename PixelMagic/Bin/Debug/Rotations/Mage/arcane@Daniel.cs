// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class ArcaneMage : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "Arcane Mage";
            }
        }

        public override string Class
        {
            get
            {
                return "Mage";
            }
        }

        public override void Initialize()
        {
            Log.Write("Welcome to Arcane Mage", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)  // Do Single Target Stuff here
            {
				if (WoW.CanCast("Barreira de Gelo")&&!WoW.PlayerIsChanneling&&!WoW.HasBuff("Invis"))
                {	
					WoW.CastSpellByName("Barreira de Gelo");
					return;
					
                }
                if (WoW.HasTarget && WoW.TargetIsEnemy&&!WoW.PlayerIsCasting&&!WoW.PlayerIsChanneling&&!WoW.HasBuff("Invis"))
                {	
					
					if(WoW.CanCast("Supernova"))
					{
						WoW.CastSpellByName("Supernova");
						return;
					}
					
					if(WoW.CanCast("Retardar")&&!WoW.HasDebuff("Retardar"))
					{
						WoW.CastSpellByName("Retardar");
						return;
					}
					if((WoW.CanCast("Rune of Power")&&WoW.GetSpellCharges("Rune of Power")==2&&!WoW.HasBuff("Rune of Power")&&!WoW.IsSpellOnCooldown("Rune of Power"))
						||WoW.IsSpellOnCooldown("Aluneth")&&!WoW.HasBuff("Rune of Power")&&!WoW.IsSpellOnCooldown("Rune of Power"))
					{
						WoW.CastSpellByName("Rune of Power");
						return;
					}
					if(WoW.CanCast("Poder Arcano"))
					{
						WoW.CastSpellByName("Poder Arcano");
						return;
					}
					if(WoW.CanCast("Aluneth")&&WoW.HasBuff("Rune of Power"))
					{
						WoW.CastSpellByName("Aluneth");
						return;
					}
					if(WoW.CanCast("Evocation")&&WoW.Mana<=20)
					{
						WoW.CastSpellByName("Evocation");
						return;
					}
					if(WoW.CanCast("Tempest")&&!WoW.HasDebuff("Tempest")&&WoW.CurrentArcaneCharges>=4)
					{
						WoW.CastSpellByName("Tempest");
						return;
					}
					if(WoW.CanCast("Arcane Miss")&&WoW.HasBuff("Arcane Missiles"))
					{
						WoW.CastSpellByName("Arcane Miss");
						return;
					}
					if((WoW.CanCast("Impacto Arcano")&&WoW.Mana>=25&&WoW.IsSpellOnCooldown("Evocation")&&!WoW.HasBuff("Arcane Missiles"))
						||WoW.CurrentArcaneCharges<=3)
					{
						WoW.CastSpellByName("Impacto Arcano");
						return;
					}
					if((WoW.CanCast("Salva")&&WoW.Mana<=40&&!WoW.HasBuff("Arcane Missiles")&&WoW.CurrentArcaneCharges==4)
						||(WoW.CurrentArcaneCharges==4&&!WoW.IsSpellOnCooldown("Evocation")))
					{
						WoW.CastSpellByName("Salva");
						return;
					}
					
					
                }
				if (WoW.CanCast("Familiar")&&!WoW.PlayerIsCasting&&!WoW.PlayerIsChanneling&&!WoW.HasBuff("Invis")&&!WoW.HasBuff("Familiar"))
                {	
					WoW.CastSpellByName("Familiar");
					return;
					
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
AddonAuthor=Dan
AddonName=dreamweaver
WoWVersion=Legion - 70000
[SpellBook.db]
Spell,11426,Barreira de Gelo,K
Spell,205022,Familiar,Y
Spell,30451,Impacto Arcano,D1
Spell,114923,Tempest,D4
Spell,12051,Evocation,G
Spell,5143,Arcane Miss,D2
Spell,157980,Supernova,J
Spell,116011,Rune of Power,F1
Spell,224968,Aluneth,I
Spell,44425,Salva,D3
Spell,12042,Poder Arcano,F2
Spell,157801,Retardar,U

Aura,110960,Invis
Aura,157801,Retardar
Aura,205022,Familiar
Aura,114923,Tempest
Aura,79683,Arcane Missiles
Aura,116014,Rune of Power
Aura,11426,Barreira de Gelo
*/

