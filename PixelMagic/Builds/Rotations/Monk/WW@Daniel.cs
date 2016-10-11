// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class WindWalker : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "WW Rotation";
            }
        }

        public override string Class
        {
            get
            {
                return "Monk";
            }
        } 

        public override void Initialize()
        {
            Log.Write("Welcome to WW rotation", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)
            {
                if (WoW.HasTarget&& WoW.TargetIsEnemy&&!WoW.PlayerIsChanneling&&!WoW.PlayerIsCasting&&!WoW.HasDebuff("Paralysis")&&WoW.IsSpellInRange("Tiger Palm"))
                {
					if(WoW.CanCast("Energizing Elixir")&&!WoW.IsSpellOnCooldown("Energizing Elixir")&&!WoW.IsSpellOnCooldown("Fists of Fury")&&WoW.CurrentChi<=1)
					{
						WoW.CastSpellByName("Energizing Elixir");
						return;
					}
					if(WoW.CanCast("Strike of the Windlord")&&WoW.CurrentChi>=2&&!WoW.lastSpell.Equals("Strike of the Windlord"))
					{
						WoW.CastSpellByName("Strike of the Windlord");
						return;
					}
					if(WoW.CanCast("Fists of Fury")&&WoW.CurrentChi>=3&&!WoW.lastSpell.Equals("Fists of Fury"))
					{
						WoW.CastSpellByName("Fists of Fury");
						return;
					}
					if(WoW.CanCast("Rising Sun Kick")&&!WoW.lastSpell.Equals("Rising Sun Kick")&&WoW.CurrentChi>=2)
					{
						WoW.CastSpellByName("Rising Sun Kick");
						return;
					}
					if(WoW.CanCast("Whirling Dragon Punch")&&WoW.IsSpellOnCooldown("Rising Sun Kick")&&WoW.IsSpellOnCooldown("Fists of Fury"))
					{
						WoW.CastSpellByName("Whirling Dragon Punch");
						return;
					}
					if(WoW.CanCast("Chi Wave")&&!WoW.lastSpell.Equals("Chi Wave"))
					{
						WoW.CastSpellByName("Chi Wave");
						return;
					}
					if(WoW.CanCast("Touch of Death")&&!WoW.lastSpell.Equals("Touch of Death"))
					{
						WoW.CastSpellByName("Touch of Death");
						return;
					}
					if(WoW.CanCast("Blackout Kick")&&!WoW.lastSpell.Equals("Blackout Kick")&&(WoW.CurrentChi>=1||WoW.HasBuff("Blackout Kick!")))
					{
						WoW.CastSpellByName("Blackout Kick");
						return;
					}
					if(WoW.CanCast("Tiger Palm")&&!WoW.lastSpell.Equals("Tiger Palm")&&WoW.CurrentChi<4&&WoW.Energy>=50)
					{
						WoW.CastSpellByName("Tiger Palm");
						return;
					}
					
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget&& WoW.TargetIsEnemy)
                {                   
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
Spell,115080,Touch of Death,K
Spell,113656,Fists of Fury,X
Spell,205320,Strike of the Windlord,Z
Spell,100780,Tiger Palm,Y
Spell,107428,Rising Sun Kick,U
Spell,115098,Chi Wave,F8
Spell,100784,Blackout Kick,I
Spell,101546,Spinning Crane Kick,F7
Spell,152175,Whirling Dragon Punch,D5
Spell,115288,Energizing Elixir,J
Aura,115078,Paralysis
Aura,152173,Serenidade
Aura,116768,Blackout Kick!
*/

