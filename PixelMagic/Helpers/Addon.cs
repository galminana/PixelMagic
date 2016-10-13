//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

namespace PixelMagic.Helpers
{
    public static class Addon
    {
        public static string LuaContents = @"
-- Configurable Variables
local size = 3.5;	-- this is the size of the ""pixels"" at the top of the screen that will show stuff, currently 5x5 because its easier to see and debug with

-- Actual Addon Code below

local f = CreateFrame(""frame"")
f:SetSize(5 * size, size);  -- Width, Height
f:SetPoint(""TOPLEFT"", 0, 0)
f:RegisterEvent(""ADDON_LOADED"")
local hpframes = {}
local cooldownframes = {}
local updateSpellChargesFrame = {}
local buffFrames = {}
local targetDebuffFrames = {}
local spellInRangeFrames = {}
local damageModifierFrames = {}
local healthFrames = {}
local targetHealthFrames = {}
local isTargetFriendlyFrame = nil
local hasTargetFrame = nil
local powerFrames = {}
local playerIsCastingFrame = nil
local targetIsCastingFrame = nil
local unitIsVisibleFrame = nil
local unitPetFrame = nil
local lastCooldownState = {}
local lastSpellChargeCharges = {}
local lastBuffState = {}
local lastDebuffState = {}
local TargetBuffs = { }
local buffLastState  = { }


local runePrev = 0
local ssPrev = 0
local ccPrev = 0
local hpPrev = 0

local lastCombat = nil
local function updateCombat()
local IsInCombat = UnitAffectingCombat(""player"");
local playerdead = UnitIsDead(""player"")
local isDead = UnitIsDead(""target"")
    if (IsInCombat == false) then
        if (IsInCombat ~= lastCombat) then
          --  print(""We are not in combat"")

            unitCombatFrame.t:SetColorTexture(1, 1, 1, 1)

            lastCombat = IsInCombat
        end
    else
        if IsInCombat ~= lastCombat and not playerdead and not IsDead then

          --  print(""We are in Combat!"")

            unitCombatFrame.t:SetColorTexture(1, 0, 0, 1)

            lastCombat = IsInCombat
        end
    end
end

local function roundNumber(num)
    under = math.floor(num)
    upper = math.floor(num) + 1
    underV = -(under - num)
    upperV = upper - num
    if (upperV > underV) then
        return under
    else
        return upper
    end
end


local function updateTargetBuffs()
	for _, auraId in pairs(buffs) do
        local buff = ""Unitbuff"";
        local auraName = GetSpellInfo(auraId)

        if auraName == nil then
            if (buffLastState[auraId] ~= ""BuffOff"") then
                TargetBuffs[auraId].t:SetColorTexture(1, 1, 1, 1)
                TargetBuffs[auraId].t:SetAllPoints(false)
                buffLastState[auraId] = ""BuffOff""          
            end
    
            return
        end



       local name, rank, icon, count, dispelType, duration, expires, caster, isStealable, nameplateShowPersonal, spellID, canApplyAura, isBossDebuff, _, nameplateShowAll, timeMod, value1, value2, value3 = UnitBuff(""Target"", auraName)

		if (name == auraName) then -- We have Aura up and Aura ID is matching our list
            local getTime = GetTime()
            local remainingTime = math.floor(expires - getTime + 0.5) 	

			if (buffLastState[auraId] ~= ""BuffOn"" .. count..remainingTime) then
            local green = 0
                local blue = 0
                local strcount = ""0.0""..count;
        local strbluecount = ""0.0""..remainingTime;
                
                if(remainingTime <= 0 or remainingTime <= -0 or remainingTime == 0) then
                blue = 0

                strbluecount = 0
				end

                if (count >= 10) then
                    strcount = ""0.""..count;
        end

                if(remainingTime >= 10) then
                   strbluecount = ""0.""..remainingTime;
        end

        green = tonumber(strcount)
        blue = tonumber(strbluecount)



                TargetBuffs[auraId].t:SetColorTexture(0, green, blue, 1)

                    TargetBuffs[auraId].t:SetAllPoints(false)


                buffLastState[auraId] = ""BuffOn"" .. count..remainingTime
            end
        else
            if (buffLastState[auraId] ~= ""BuffOff"") then
                TargetBuffs[auraId].t:SetColorTexture(1, 1, 1, 1)
                TargetBuffs[auraId].t:SetAllPoints(false)
                buffLastState[auraId] = ""BuffOff""
              
            end
        end
    end
end

local function updateHolyPower(self, event)
    local power = UnitPower(""player"", 9)
	
	if power ~= hpPrev then	
		print(""Holy Power: "" .. power)   
	   
		local i = 1

		while i <= power do
			hpframes[i].t:SetColorTexture(1, 0, 0, 1)
			hpframes[i].t:SetAllPoints(false)
			i = 1 + i
		end
		
		while i <= 5 do
			hpframes[i].t:SetColorTexture(0, 1, 1, 1)
			hpframes[i].t:SetAllPoints(false)
			i = 1 + i
		end
		
		hpPrev = power
	 end
 end

local function updateComboPoints(self, event)
    local playerClass, englishClass, classIndex = UnitClass(""player"");
    local power = UnitPower(""player"", 4)
	
    if power ~= ccPrev then
        local i = 1

        while i <= power do
            hpframes[i].t:SetColorTexture(1, 0, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end
		
       if(classIndex == 11) then		
        while i <= 5 do
            hpframes[i].t:SetColorTexture(1, 1, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end

        end
		
		if(classIndex == 4) then		
        while i <= 8 do
            hpframes[i].t:SetColorTexture(1, 1, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end

        end

        ccPrev = power
    end
end

local function updateChiPower()
    local power = UnitPower(""player"", 12)
    if power ~= ChiPrev then
        --print(""Chi: "" .. power)
        local i = 1
        while i <= power do
            hpframes[i].t:SetTexture(1, 0, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end
        while i <= 5 do
            hpframes[i].t:SetTexture(1, 1, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end
        ChiPrev = power
    end
end

local function updateArcaneCharges(self, event)
    local power = UnitPower(""player"", 16)	
    if power ~= ssPrev then	    
        local i = 1

        while i <= power do
            hpframes[i].t:SetColorTexture(1, 0, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end
		    
        while i <= 4 do
            hpframes[i].t:SetColorTexture(1, 1, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end

        ssPrev = power
    end
end

local function updateSoulShards(self, event)
    local power = UnitPower(""player"", 7)
	
    if power ~= ssPrev then	    
        local i = 1

        while i <= power do
            hpframes[i].t:SetColorTexture(1, 0, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end
		    
        while i <= 5 do
            hpframes[i].t:SetColorTexture(1, 1, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end

        ssPrev = power
    end
end

local function updateRunes(self, event)
    local CurrentMaxRunes = UnitPower(""player"", SPELL_POWER_RUNES);
    local selRune = 6
    local i = 1
    for i = 1, CurrentMaxRunes do
        local RuneReady = select(3, GetRuneCooldown(i))
        if not RuneReady then
            selRune = selRune - 1
        end
    end
    if selRune ~= runePrev then	

        while i <= selRune do
            hpframes[i].t:SetColorTexture(1, 0, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end
    
        while i <= 6 do
            hpframes[i].t:SetColorTexture(1, 1, 1, 1)
            hpframes[i].t:SetAllPoints(false)
            i = i + 1
		end

        runePrev = selRune     
    end
end

local lastPet = nil
local function updateUnitPet(self, event)
    local pet = UnitExists(""pet"")

    local petdead = UnitIsDead(""pet"")
    local playerdead = UnitIsDead(""player"")
	if (pet == false or petdead) then			
        if (pet ~= lastPet) then
            print(""Pet Is Not Up"")


            unitPetFrame.t:SetColorTexture(1, 1, 1, 1)

            lastPet = pet
        end
	else
		if pet ~= lastPet and not playerdead  then
            print(""Pet is up!"")


            unitPetFrame.t:SetColorTexture(1, 0, 0, 1)

            lastPet = pet
        end

    end
end






local function updateSpellCooldowns(self, event) 
    for _, spellId in pairs(cooldowns) do
		-- start is the value of GetTime() at the point the spell began cooling down
		-- duration is the total duration of the cooldown, NOT the remaining
		local start, duration, _ = GetSpellCooldown(spellId)
		if duration ~= 0 then -- the spell is not ready to be cast
			if (lastCooldownState[spellId] ~= ""onCD"") then										 
				--print(""Spell with Id = "" .. spellId .. "" is on CD"")
					
				cooldownframes[spellId].t:SetColorTexture(1, 0, 0, 1)
				cooldownframes[spellId].t:SetAllPoints(false)
					
				lastCooldownState[spellId] = ""onCD""
			end				
		else
			if (lastCooldownState[spellId] ~= ""offCD"") then
				--print(""Spell with Id = "" .. spellId .. "" is off CD and can be cast"")
					
				cooldownframes[spellId].t:SetColorTexture(1, 1, 1, 1)
				cooldownframes[spellId].t:SetAllPoints(false)
					
				lastCooldownState[spellId] = ""offCD""
			end
		end						
	end
end

local function updateSpellCharges(self, event) 
    
	for _, spellId in pairs(cooldowns) do
        charges, maxCharges, start, duration = GetSpellCharges(spellId)

        if (lastSpellChargeCharges[spellId] ~= charges) then
            local green = 0             
            local strcount = ""0.0"" .. charges;
                    
            if (charges >= 10) then
                strcount = ""0."" .. charges;
            end
            green = tonumber(strcount)

            --print(""Spell with Id = "" .. spellId .. "" has charges: "" .. charges .. "" Green = "" .. green)

            updateSpellChargesFrame[spellId].t:SetColorTexture(0, green, 0, 1)
		    updateSpellChargesFrame[spellId].t:SetAllPoints(false)
		    		
		    lastSpellChargeCharges[spellId] = charges		
        end        
	end
end

local function updateMyBuffs(self, event) 
    
	for _, auraId in pairs(buffs) do
        local buff = ""UnitBuff"";
		local auraName = GetSpellInfo(auraId)
		
		if auraName == nil then
			if (lastBuffState[auraId] ~= ""BuffOff"") then
                buffFrames[auraId].t:SetColorTexture(1, 1, 1, 1)
                buffFrames[auraId].t:SetAllPoints(false)
                lastBuffState[auraId] = ""BuffOff""
                --print(""["" .. buff .. ""] "" .. auraName.. "" Off"")
            end
			return
		end
		
		local name, rank, icon, count, debuffType, duration, expirationTime, unitCaster, isStealable, shouldConsolidate, spellId = UnitBuff(""player"", auraName)		
		
		if (name == auraName) then -- We have Aura up and Aura ID is matching our list					
			if (lastBuffState[auraId] ~= ""BuffOn"" .. count .. expirationTime) then
                local green = 0             
                local blue = 0
                local strcount = ""0.0"" .. count;
                local strbluecount = ""0.0"" .. expirationTime;
                
                if (count >= 10) then
                    strcount = ""0."" .. count;
                end
                if(expirationTime >= 10) then
                   strbluecount = ""0."" .. remainingTime
                end
                green = tonumber(strcount)
                blue = tonumber(strbluecount)

                buffFrames[auraId].t:SetColorTexture(0, green, blue, 1)
				buffFrames[auraId].t:SetAllPoints(false)
                --print(""["" .. buff .. ""] "" .. auraName.. "" "" .. count .. "" Green: "" .. green)
                lastBuffState[auraId] = ""BuffOn"" .. count 
            end
        else
            if (lastBuffState[auraId] ~= ""BuffOff"") then
                buffFrames[auraId].t:SetColorTexture(1, 1, 1, 1)
                buffFrames[auraId].t:SetAllPoints(false)
                lastBuffState[auraId] = ""BuffOff""
                --print(""["" .. buff .. ""] "" .. auraName.. "" Off"")
            end
        end
    end
end

local function updateTargetDebuffs(self, event)
    
	for _, auraId in pairs(debuffs) do
        local buff = ""UnitDebuff"";
		local auraName = GetSpellInfo(auraId)

        if auraName == nil then
            if (lastDebuffState[auraId] ~= ""DebuffOff"") then
                targetDebuffFrames[auraId].t:SetColorTexture(1, 1, 1, 1)
                targetDebuffFrames[auraId].t:SetAllPoints(false)
                lastDebuffState[auraId] = ""DebuffOff""               
            end
    
            return
        end
        
		--print(""Getting debuff for Id = "" .. auraName)
		
        local name, rank, icon, count, debuffType, duration, expirationTime, unitCaster, isStealable, shouldConsolidate, spellId, canApplyAura, isBossDebuff, value1, value2, value3 = UnitDebuff(""target"", auraName)		        

		if (name == auraName) then -- We have Aura up and Aura ID is matching our list					
            local getTime = GetTime()
            local remainingTime = math.floor(expirationTime - getTime + 0.5) 	

			if (lastDebuffState[auraId] ~= ""DebuffOn"" .. count .. remainingTime) then
                local green = 0
                local blue = 0             
                local strcount = ""0.0"" .. count;
                local strbluecount = ""0.0"" .. remainingTime;
                
                if (count >= 10) then
                    strcount = ""0."" .. count;
                end

                if(remainingTime >= 10) then
                   strbluecount = ""0."" .. remainingTime
                end

                green = tonumber(strcount)
                blue = tonumber(strbluecount)

                targetDebuffFrames[auraId].t:SetColorTexture(0, green, blue, 1)
				targetDebuffFrames[auraId].t:SetAllPoints(false)
                --print(""["" .. buff .. ""] "" .. auraName.. "" "" .. count .. "" Green: "" .. green .. "" Blue: "" .. blue)
                lastDebuffState[auraId] = ""DebuffOn"" .. count .. remainingTime
            end
        else
            if (lastDebuffState[auraId] ~= ""DebuffOff"") then
                targetDebuffFrames[auraId].t:SetColorTexture(1, 1, 1, 1)
                targetDebuffFrames[auraId].t:SetAllPoints(false)
                lastDebuffState[auraId] = ""DebuffOff""
                --print(""["" .. buff .. ""] "" .. auraName.. "" Off"")
            end
        end
    end
end

local lastSpellInRange = {}

local function updateSpellInRangeFrames(self, event) 
    
	for _, spellId in pairs(cooldowns) do		
		local inRange = 0		
		local name, rank, icon, castTime, minRange, maxRange = GetSpellInfo(spellId)
		
		if (name == nil) then		
			inRange = 0		
		else
			-- http://wowwiki.wikia.com/wiki/API_IsSpellInRange	
			inRange = IsSpellInRange(name, ""target"")  -- '0' if out of range, '1' if in range, or 'nil' if the unit is invalid.	
		end
								
		if lastSpellInRange[spellId] ~= inRange then
			if (inRange == 1) then
				spellInRangeFrames[spellId].t:SetColorTexture(1, 0, 0, 1)
			else
				spellInRangeFrames[spellId].t:SetColorTexture(1, 1, 1, 1)
			end 
			spellInRangeFrames[spellId].t:SetAllPoints(false)
			
			--print(""Spell: "" .. name .. "" InRange = "" .. inRange)
			
			lastSpellInRange[spellId] = inRange	
		end				
	end
end

function ToBinary(num)
	local bits = 8

    -- returns a table of bits
    local t={} -- will contain the bits
    for b=bits,1,-1 do
        rest=math.fmod(num,2)
        t[b]=rest
        num=(num-rest)/2
    end
	
	return table.concat(t)
end

local lastHealth = 0

local function updateHealth(self, event)
    
	local health = UnitHealth(""player"");		
	local maxHealth = UnitHealthMax(""player"");
	local percHealth = ceil((health / maxHealth) * 100)
	
	if (percHealth ~= lastHealth) then		
		local binaryHealth = ToBinary(percHealth)
		--print (""Health = "" .. percHealth .. "" binary = "".. binaryHealth)
		
		for i = 1, string.len(binaryHealth) do
			local currentBit = string.sub(binaryHealth, i, i)
			
			if (currentBit == ""1"") then
				healthFrames[i].t:SetColorTexture(1, 0, 0, 1)
			else
				healthFrames[i].t:SetColorTexture(1, 1, 1, 1)
			end
			healthFrames[i].t:SetAllPoints(false)
		end
		
		lastHealth = percHealth
	end
end

local lastDamageModifier = 100
local function updateDamageModifier()
    local lowDmg, hiDmg, offlowDmg, offhiDmg, posBuff, negBuff, percentmod = UnitDamage(""player"");
    local damageModifier = roundNumber((percentmod* 100))
    
    if(damageModifier ~= lastDamageModifier) then
        local binaryModifier = ToBinary(damageModifier)
        --print (""Modifier = "" .. damageModifier .. "" binary = "".. binaryModifier)
        for i = 1, string.len(binaryModifier) do
            local currentBit = string.sub(binaryModifier, i, i)
            if (currentBit == ""1"") then
                damageModifierFrames[i].t:SetColorTexture(1, 0, 0, 1)
            else
                damageModifierFrames[i].t:SetColorTexture(1, 1, 1, 1)
            end
            damageModifierFrames[i].t:SetAllPoints(false)
        end
        lastDamageModifier = damageModifier
    end
end


local lastTargetHealth = 0

local function updateTargetHealth(self, event)
    
	local guid = UnitGUID(""target"")

    local health = 0		
    local maxHealth = 100
    local percHealth = 0

    if (guid ~= nil) then
	    health = UnitHealth(""target"");		
	    maxHealth = UnitHealthMax(""target"");
	    percHealth = ceil((health / maxHealth) * 100)
    end	
	
	if (percHealth ~= lastTargetHealth) then		
		local binaryHealth = ToBinary(percHealth)
		--print (""Target Health = "" .. percHealth .. "" binary = "".. binaryHealth)
		
		for i = 1, string.len(binaryHealth) do
			local currentBit = string.sub(binaryHealth, i, i)
			
			if (currentBit == ""1"") then
				targetHealthFrames[i].t:SetColorTexture(0, 0, 1, 1)
			else
				targetHealthFrames[i].t:SetColorTexture(1, 1, 1, 1)
			end
			targetHealthFrames[i].t:SetAllPoints(false)
		end
		
		lastTargetHealth = percHealth
	end
end

local lastPower = 0
local playerClass, englishClass, classIndex = UnitClass(""player"");
local currentSpec = GetSpecialization()
local currentSpecId = currentSpec and select(1, GetSpecializationInfo(currentSpec)) or 0

local function updatePower(self, event)
    
	local power = UnitPower(""player"");		
	local maxPower = UnitPowerMax(""player"");

	
	if (power ~= lastPower) then
		lastPower = power
			
		-- If the class uses mana, then we need to calculate percent mana to show its power
		-- http://wowwiki.wikia.com/wiki/API_UnitClass
		-- http://wowwiki.wikia.com/wiki/SpecializationID
		if (
			((classIndex == 7) and (currentSpecId == 264))  -- Shaman Restoration
		 or (classIndex == 2)  -- Paladin
		 or (classIndex == 5)  -- Priest 
		 or (classIndex == 8)  -- Mage
		 or (classIndex == 9)  -- Lock
		 or ((classIndex == 11) and (currentSpecId == 102)) -- Druid Balance
		 or ((classIndex == 11) and (currentSpecId == 105)) -- Druid Resto 
		   ) 
		then 
			power = ceil((power / maxPower) * 100)
		end
		
		local binaryPower = ToBinary(power)			
		--print (""Power = "" .. power .. "" binary = "".. binaryPower)	
		--print (""Current Spec = "" .. currentSpecId)
		
		for i = 1, string.len(binaryPower) do
			local currentBit = string.sub(binaryPower, i, i)
			
			if (currentBit == ""1"") then
				powerFrames[i].t:SetColorTexture(0, 1, 0, 1)
			else
				powerFrames[i].t:SetColorTexture(1, 1, 1, 1)
			end
			powerFrames[i].t:SetAllPoints(false)
		end	
	end
end
 
local lastIsFriend = true 
 
local function updateIsFriendly(self, event)
    
	isFriend = UnitIsFriend(""player"",""target"");
	
	if (isFriend ~= lastIsFriend) then
	
		if (isFriend == true) then
			--print (""Unit is friendly: True"")
			
			isTargetFriendlyFrame.t:SetColorTexture(0, 1, 0, 1)
		else
			--print (""Unit is friendly: False"")
			
			isTargetFriendlyFrame.t:SetColorTexture(0, 0, 1, 1)
		end
	
		lastIsFriend = isFriend
	end
end

local lastTargetGUID = """"

local function hasTarget()
	guid = UnitGUID(""target"")
		
	if (guid ~= lastTargetGUID) then
		if (guid == nil) then
			--print (""Target GUID: None"" )	
			
			hasTargetFrame.t:SetColorTexture(0, 0, 0, 1)
		else			
			--print (""Target GUID: "" .. guid )	
			
			hasTargetFrame.t:SetColorTexture(1, 0, 0, 1)
		end
			
		lastTargetGUID = guid		
	end
end

local lastCastID = 0
local lastChanneling = """"

local function updatePlayerIsCasting(self, event)
    
    spell, rank, displayName, icon, startTime, endTime, isTradeSkill, castID, interrupt = UnitCastingInfo(""player"")
    name, nameSubtext, text, texture, startTime, endTime, isTradeSkill, notInterruptible = UnitChannelInfo(""player"")

	if castID ~= nil then	
		if castID ~= lastCastID then
			--print(""Casting spell: "" .. spell)
		
			playerIsCastingFrame.t:SetColorTexture(1, 0, 0, 1)
		
			lastCastID = castID		
		end
	else
		if castID ~= lastCastID then
			--print(""Not casting"")
			
			playerIsCastingFrame.t:SetColorTexture(1, 1, 1, 1)
			
			lastCastID = castID		
		end	
	end		

    if name ~= nil then
        if text ~= lastChanneling then
        playerIsCastingFrame.t:SetColorTexture(0,1,0,1)
        --   print(text)
        lastChanneling = text
        end

	else
	    if text ~= lastChanneling then
        playerIsCastingFrame.t:SetColorTexture(1, 1, 1, 1)
        lastChanneling = text
        end

    end
end

local lastTargetCastID = 0

local function updateTargetIsCasting(self, event)	
    
	spell, rank, displayName, icon, startTime, endTime, isTradeSkill, castID, interrupt = UnitCastingInfo(""target"")
		
	if castID ~= nil then	
		if castID ~= lastTargetCastID then
			--print(""Casting spell: "" .. spell)
		
			targetIsCastingFrame.t:SetColorTexture(1, 0, 0, 1)
		
			lastTargetCastID = castID		
		end
	else
		if castID ~= lastTargetCastID then
			--print(""Not casting"")
			
			targetIsCastingFrame.t:SetColorTexture(1, 1, 1, 1)
			
			lastTargetCastID = castID		
		end	
	end
end

local lastVis = nil

local function updateUnitIsVisible(self, event)
    
	local vis = UnitIsVisible(""target"")
		
	if vis == nil then			
        if (vis ~= lastVis) then
		    --print(""Target Is Not Visible"")		

	        unitIsVisibleFrame.t:SetColorTexture(1, 1, 1, 1)
		    lastVis = vis				
        end
	else
		if vis ~= lastVis then
			--print(""Target Is Visible"")			

			unitIsVisibleFrame.t:SetColorTexture(1, 0, 0, 1)			
			lastVis = vis		
		end	
	end
end
 
local function initFrames()
    local playerClass, englishClass, classIndex = UnitClass(""player"");
    local i = 0

	--print (""Initialising Health Frames"")
	for i = 1, 8 do
		healthFrames[i] = CreateFrame(""frame"")
		healthFrames[i]:SetSize(size, size)
		healthFrames[i]:SetPoint(""TOPLEFT"", (i - 1) * size, 0)                -- column 1 - 8, row 1
		healthFrames[i].t = healthFrames[i]:CreateTexture()        
		healthFrames[i].t:SetColorTexture(1, 1, 1, 1)
		healthFrames[i].t:SetAllPoints(healthFrames[i])
        healthFrames[i]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
        healthFrames[i]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
		healthFrames[i]:Show()		
		
		healthFrames[i]:SetScript(""OnUpdate"", updateHealth)
	end


	--print (""Initialising Power Frames (Rage, Energy, etc...)"")  
    local start = 8
	for i = 9, 16 do
		powerFrames[i-start] = CreateFrame(""frame"")
		powerFrames[i-start]:SetSize(size, size)
		powerFrames[i-start]:SetPoint(""TOPLEFT"", (i - 1) * size, 0)           -- column 9-16, row 1
		powerFrames[i-start].t = powerFrames[i-start]:CreateTexture()        
		powerFrames[i-start].t:SetColorTexture(1, 1, 1, 1)
		powerFrames[i-start].t:SetAllPoints(powerFrames[i-start])
        powerFrames[i-start]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
        powerFrames[i-start]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
		powerFrames[i-start]:Show()		
		
		powerFrames[i-start]:SetScript(""OnUpdate"", updatePower)
	end
  
	--print (""Initialising Target Health Frames"")
    start = 16
	for i = 17, 24 do
		targetHealthFrames[i-start] = CreateFrame(""frame"")
		targetHealthFrames[i-start]:SetSize(size, size)
		targetHealthFrames[i-start]:SetPoint(""TOPLEFT"", (i - 1) * size, 0)    -- column 17 - 24, row 1        
		targetHealthFrames[i-start].t = targetHealthFrames[i-start]:CreateTexture()        
		targetHealthFrames[i-start].t:SetColorTexture(1, 1, 1, 1)
		targetHealthFrames[i-start].t:SetAllPoints(targetHealthFrames[i-start])
        targetHealthFrames[i-start]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
        targetHealthFrames[i-start]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
		targetHealthFrames[i-start]:Show()		
		
		targetHealthFrames[i-start]:SetScript(""OnUpdate"", updateTargetHealth)
	end
	
    --print (""Initialising modifier Frames"")
        start = 24
	for i = 25, 32 do
		damageModifierFrames[i-start] = CreateFrame(""frame"")
		damageModifierFrames[i-start]:SetSize(size, size)
		damageModifierFrames[i-start]:SetPoint(""TOPLEFT"", (i - 1) * size, 0)    -- column 25 - 32, row 1        
		damageModifierFrames[i-start].t = damageModifierFrames[i-start]:CreateTexture()        
		damageModifierFrames[i-start].t:SetColorTexture(1, 1, 1, 1)
		damageModifierFrames[i-start].t:SetAllPoints(damageModifierFrames[i-start])
        damageModifierFrames[i-start]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
        damageModifierFrames[i-start]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
		damageModifierFrames[i-start]:Show()		
		
		damageModifierFrames[i-start]:SetScript(""OnUpdate"", updateDamageModifier)
	end


	--print (""Initialising Spell Cooldown Frames"")
	i = 1
	for _, spellId in pairs(cooldowns) do	
		cooldownframes[spellId] = CreateFrame(""frame"")
		cooldownframes[spellId]:SetSize(size, size)
		cooldownframes[spellId]:SetPoint(""TOPLEFT"", (i - 1) * size, -size)          -- column 1+, row 2
		cooldownframes[spellId].t = cooldownframes[spellId]:CreateTexture()        
		cooldownframes[spellId].t:SetColorTexture(1, 1, 1, 1)
		cooldownframes[spellId].t:SetAllPoints(cooldownframes[spellId])
        cooldownframes[spellId]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
        cooldownframes[spellId]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
		cooldownframes[spellId]:Show()
		               
		cooldownframes[spellId]:SetScript(""OnUpdate"", updateSpellCooldowns)
		i = i + 1
	end

	--print (""Initialising Spell Charges Frames"")
	i = 1
	for _, spellId in pairs(cooldowns) do	
		updateSpellChargesFrame[spellId] = CreateFrame(""frame"")
		updateSpellChargesFrame[spellId]:SetSize(size, size)
		updateSpellChargesFrame[spellId]:SetPoint(""TOPLEFT"", (i - 1) * size, -size * 8)          -- column 1+, row 9
		updateSpellChargesFrame[spellId].t = updateSpellChargesFrame[spellId]:CreateTexture()        
		updateSpellChargesFrame[spellId].t:SetColorTexture(1, 1, 1, 1)
		updateSpellChargesFrame[spellId].t:SetAllPoints(updateSpellChargesFrame[spellId])
        updateSpellChargesFrame[spellId]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
        updateSpellChargesFrame[spellId]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
		updateSpellChargesFrame[spellId]:Show()
		               
		updateSpellChargesFrame[spellId]:SetScript(""OnUpdate"", updateSpellCharges)
		i = i + 1
	end

    if classIndex == 10 then                                 -- Monk
        --print (""Initialising Chi Frames"")
	    for i = 1, 5 do
		    hpframes[i] = CreateFrame(""frame"");
		    hpframes[i]:SetSize(size, size)
		    hpframes[i]:SetPoint(""TOPLEFT"", i * size - 5, -size * 6)          -- column 1 - 5, row 7
		    hpframes[i].t = hpframes[i]:CreateTexture()        
		    hpframes[i].t:SetColorTexture(0, 1, 1, 1)
		    hpframes[i].t:SetAllPoints(hpframes[i])
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
		    hpframes[i]:Show()
		
		    hpframes[i]:SetScript(""OnUpdate"", updateChiPower)
	    end
    end

    if classIndex == 2 then                                 -- Paladin
        --print (""Initialising Holy Power Frames"")
	    for i = 1, 5 do
		    hpframes[i] = CreateFrame(""frame"");
		    hpframes[i]:SetSize(size, size)
		    hpframes[i]:SetPoint(""TOPLEFT"", i * size - 5, -size * 6)          -- column 1 - 5, row 7
		    hpframes[i].t = hpframes[i]:CreateTexture()        
		    hpframes[i].t:SetColorTexture(0, 1, 1, 1)
		    hpframes[i].t:SetAllPoints(hpframes[i])
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
		    hpframes[i]:Show()
		
		    hpframes[i]:SetScript(""OnUpdate"", updateHolyPower)
	    end
    end

    if classIndex == 9 then                                 -- Warlock
        --print (""Initialising Soulshard Frames"")
	    for i = 1, 5 do
		    hpframes[i] = CreateFrame(""frame"");
		    hpframes[i]:SetSize(size, size)
		    hpframes[i]:SetPoint(""TOPLEFT"", i * size - 5, -size * 6)          -- column 1 - 5, row 7
		    hpframes[i].t = hpframes[i]:CreateTexture()        
		    hpframes[i].t:SetColorTexture(0, 1, 1, 1)
		    hpframes[i].t:SetAllPoints(hpframes[i])
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
		    hpframes[i]:Show()
		
		    hpframes[i]:SetScript(""OnUpdate"", updateSoulShards)
	    end
    end
    if classIndex == 8 then                                 -- Mage
        --print (""Initialising ArcaneCharges Frames"")
	    for i = 1, 4 do
		    hpframes[i] = CreateFrame(""frame"");
		    hpframes[i]:SetSize(size, size)
		    hpframes[i]:SetPoint(""TOPLEFT"", i * size - 5, -size * 6)          -- column 1 - 5, row 7
		    hpframes[i].t = hpframes[i]:CreateTexture()        
		    hpframes[i].t:SetColorTexture(0, 1, 1, 1)
		    hpframes[i].t:SetAllPoints(hpframes[i])
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
		    hpframes[i]:Show()
		
		    hpframes[i]:SetScript(""OnUpdate"", updateArcaneCharges)
	    end
    end

    if classIndex == 6 then                                 -- DeathKnight
	    --print(""Initialising Unit Is Visible Frame"")
    
        unitPetFrame = CreateFrame(""frame"");
        unitPetFrame:SetSize(size, size);
        unitPetFrame:SetPoint(""TOPLEFT"", i* size, -size* 4)           -- column 5 row 3
	    unitPetFrame.t = unitPetFrame:CreateTexture()
    
        unitPetFrame.t:SetColorTexture(0, 1, 0, 1)

        unitPetFrame.t:SetAllPoints(unitPetFrame)
        unitPetFrame:RegisterEvent(""PLAYER_REGEN_DISABLED"")
        unitPetFrame:RegisterEvent(""PLAYER_REGEN_ENABLED"")
        unitPetFrame:Show()


        unitPetFrame:SetScript(""OnUpdate"", updateUnitPet)
    end
    if classIndex == 3 then                                 -- Hunter
	    --print(""Initialising Unit Is Visible Frame"")

        unitPetFrame = CreateFrame(""frame"");
        unitPetFrame:SetSize(size, size);
        unitPetFrame:SetPoint(""TOPLEFT"", i* size, -size* 4)           -- column 5 row 3
	    unitPetFrame.t = unitPetFrame:CreateTexture()

        unitPetFrame.t:SetColorTexture(0, 1, 0, 1)

        unitPetFrame.t:SetAllPoints(unitPetFrame)
        unitPetFrame:RegisterEvent(""PLAYER_REGEN_DISABLED"")
        unitPetFrame:RegisterEvent(""PLAYER_REGEN_ENABLED"")
        unitPetFrame:Show()


        unitPetFrame:SetScript(""OnUpdate"", updateUnitPet)
    end

    if classIndex == 4 then                                 -- Rogue 
        --print (""Initialising Combo Point Frames - Class Index = "" .. classIndex)
	    for i = 1, 8 do
		    hpframes[i] = CreateFrame(""frame"");
            hpframes[i]:SetSize(size, size)
            hpframes[i]:SetPoint(""TOPLEFT"", i* size - 5, -size* 6)          -- column 1 - 5, row 7
		    hpframes[i].t = hpframes[i]:CreateTexture()
            hpframes[i].t:SetColorTexture(0, 1, 1, 1)
            hpframes[i].t:SetAllPoints(hpframes[i])
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
            hpframes[i]:Show()
            hpframes[i]:SetScript(""OnUpdate"", updateComboPoints)

        end
    end

    
	
	if classIndex == 11 then                                 -- Druid Feral
        --print(""Initialising Combo Point Frames - Class Index = "" .. classIndex)
	    for i = 1, 5 do
		    hpframes[i] = CreateFrame(""frame"");
            hpframes[i]:SetSize(size, size)
            hpframes[i]:SetPoint(""TOPLEFT"", i* size - 5, -size* 6)          -- column 1 - 5, row 7
		    hpframes[i].t = hpframes[i]:CreateTexture()
            hpframes[i].t:SetColorTexture(0, 1, 1, 1)
            hpframes[i].t:SetAllPoints(hpframes[i])
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
            hpframes[i]:Show()
            hpframes[i]:SetScript(""OnUpdate"", updateComboPoints)
        end
    end

    if classIndex == 6 then                                 -- DeathKnight
        --print (""Initialising Runes Frames"")
	    for i = 1, 6 do
		    hpframes[i] = CreateFrame(""frame"");
		    hpframes[i]:SetSize(size, size)
		    hpframes[i]:SetPoint(""TOPLEFT"", i * size - 5, -size * 6)          -- column 1 - 6, row 7
		    hpframes[i].t = hpframes[i]:CreateTexture()        
		    hpframes[i].t:SetColorTexture(0, 1, 1, 1)
		    hpframes[i].t:SetAllPoints(hpframes[i])
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
            hpframes[i]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
		    hpframes[i]:Show()	
		    hpframes[i]:SetScript(""OnUpdate"", updateRunes)
	    end
    end
	
	--print (""Initialising Spell In Range Frames"")
	local i = 0
	for _, spellId in pairs(cooldowns) do	
		spellInRangeFrames[spellId] = CreateFrame(""frame"")
		spellInRangeFrames[spellId]:SetSize(size, size)
		spellInRangeFrames[spellId]:SetPoint(""TOPLEFT"", i * size, -size * 5)  -- entire row 6
		spellInRangeFrames[spellId].t = spellInRangeFrames[spellId]:CreateTexture()        
		spellInRangeFrames[spellId].t:SetColorTexture(1, 1, 1, 1)
		spellInRangeFrames[spellId].t:SetAllPoints(spellInRangeFrames[spellId])
        spellInRangeFrames[spellId]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
        spellInRangeFrames[spellId]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
		spellInRangeFrames[spellId]:Show()
		               
		spellInRangeFrames[spellId]:SetScript(""OnUpdate"", updateSpellInRangeFrames)
		i = i + 1
	end
	
	--print (""Initialising IsTargetFriendly Frame"")
	isTargetFriendlyFrame = CreateFrame(""frame"");
	isTargetFriendlyFrame:SetSize(size, size);
	isTargetFriendlyFrame:SetPoint(""TOPLEFT"", 0, -(size * 2))                 -- column 1 row 3
	isTargetFriendlyFrame.t = isTargetFriendlyFrame:CreateTexture()        
	isTargetFriendlyFrame.t:SetColorTexture(0, 1, 0, 1)
	isTargetFriendlyFrame.t:SetAllPoints(isTargetFriendlyFrame)
    isTargetFriendlyFrame:RegisterEvent(""PLAYER_REGEN_ENABLED"")
    isTargetFriendlyFrame:RegisterEvent(""PLAYER_REGEN_DISABLED"")
	isTargetFriendlyFrame:Show()		
		
	isTargetFriendlyFrame:SetScript(""OnUpdate"", updateIsFriendly)
	
	--print (""Initialising HasTarget Frame"")
	hasTargetFrame = CreateFrame(""frame"");
	hasTargetFrame:SetSize(size, size);
	hasTargetFrame:SetPoint(""TOPLEFT"", size, -(size * 2))                     -- column 2 row 3
	hasTargetFrame.t = hasTargetFrame:CreateTexture()        
	hasTargetFrame.t:SetColorTexture(0, 1, 0, 1)
	hasTargetFrame.t:SetAllPoints(hasTargetFrame)
    hasTargetFrame:RegisterEvent(""PLAYER_REGEN_ENABLED"")
    hasTargetFrame:RegisterEvent(""PLAYER_REGEN_DISABLED"")
	hasTargetFrame:Show()		
		
	hasTargetFrame:SetScript(""OnUpdate"", hasTarget)
	
	--print (""Initialising PlayerIsCasting Frame"")
	playerIsCastingFrame = CreateFrame(""frame"");
	playerIsCastingFrame:SetSize(size, size);
	playerIsCastingFrame:SetPoint(""TOPLEFT"", size * 2, -(size * 2))           -- column 3 row 3
	playerIsCastingFrame.t = playerIsCastingFrame:CreateTexture()        
	playerIsCastingFrame.t:SetColorTexture(1, 1, 1, 1)
	playerIsCastingFrame.t:SetAllPoints(playerIsCastingFrame)
    playerIsCastingFrame:RegisterEvent(""PLAYER_REGEN_ENABLED"")
    playerIsCastingFrame:RegisterEvent(""PLAYER_REGEN_DISABLED"")
	playerIsCastingFrame:Show()		
		
	playerIsCastingFrame:SetScript(""OnUpdate"", updatePlayerIsCasting)
	
	--print (""Initialising TargetIsCasting Frame"")
	targetIsCastingFrame = CreateFrame(""frame"");
	targetIsCastingFrame:SetSize(size, size);
	targetIsCastingFrame:SetPoint(""TOPLEFT"", size * 3, -(size * 2))           -- column 4 row 3
	targetIsCastingFrame.t = targetIsCastingFrame:CreateTexture()        
	targetIsCastingFrame.t:SetColorTexture(1, 1, 1, 1)
	targetIsCastingFrame.t:SetAllPoints(targetIsCastingFrame)
    targetIsCastingFrame:RegisterEvent(""PLAYER_REGEN_ENABLED"")
    targetIsCastingFrame:RegisterEvent(""PLAYER_REGEN_DISABLED"")
	targetIsCastingFrame:Show()		
		
	targetIsCastingFrame:SetScript(""OnUpdate"", updateTargetIsCasting)

	--print (""Initialising Unit Is Visible Frame"")
	unitIsVisibleFrame = CreateFrame(""frame"");
	unitIsVisibleFrame:SetSize(size, size);
	unitIsVisibleFrame:SetPoint(""TOPLEFT"", size * 4, -(size * 2))             -- column 5 row 3
	unitIsVisibleFrame.t = unitIsVisibleFrame:CreateTexture()        
	unitIsVisibleFrame.t:SetColorTexture(0, 1, 0, 1)
	unitIsVisibleFrame.t:SetAllPoints(unitIsVisibleFrame)
    unitIsVisibleFrame:RegisterEvent(""PLAYER_REGEN_ENABLED"")
    unitIsVisibleFrame:RegisterEvent(""PLAYER_REGEN_DISABLED"")
	unitIsVisibleFrame:Show()		
		
	unitIsVisibleFrame:SetScript(""OnUpdate"", updateUnitIsVisible)
		
	--print (""Initialising Player Buff Frames"")
	local i = 5
	for _, buffId in pairs(buffs) do
		buffFrames[buffId] = CreateFrame(""frame"")
		buffFrames[buffId]:SetSize(size, size)
		buffFrames[buffId]:SetPoint(""TOPLEFT"", i * size, -(size * 2))         -- column 6+ row 3
		buffFrames[buffId].t = buffFrames[buffId]:CreateTexture()        
		buffFrames[buffId].t:SetColorTexture(1, 1, 1, 1)
		buffFrames[buffId].t:SetAllPoints(buffFrames[buffId])
        buffFrames[buffId]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
        buffFrames[buffId]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
		buffFrames[buffId]:Show()
		               
		buffFrames[buffId]:SetScript(""OnUpdate"", updateMyBuffs)
		i = i + 1
	end

	--print (""Initialising Target Debuff Frames"")
	local i = 0
	for _, debuffId in pairs(debuffs) do
		targetDebuffFrames[debuffId] = CreateFrame(""frame"")
		targetDebuffFrames[debuffId]:SetSize(size, size)
		targetDebuffFrames[debuffId]:SetPoint(""TOPLEFT"", i * size, -(size * 7))         -- column 1+ row 8
		targetDebuffFrames[debuffId].t = targetDebuffFrames[debuffId]:CreateTexture()        
		targetDebuffFrames[debuffId].t:SetColorTexture(1, 1, 1, 1)
		targetDebuffFrames[debuffId].t:SetAllPoints(targetDebuffFrames[debuffId])
        targetDebuffFrames[debuffId]:RegisterEvent(""PLAYER_REGEN_ENABLED"")
        targetDebuffFrames[debuffId]:RegisterEvent(""PLAYER_REGEN_DISABLED"")
		targetDebuffFrames[debuffId]:Show()
		               
		targetDebuffFrames[debuffId]:SetScript(""OnUpdate"", updateTargetDebuffs)
		i = i + 1
	end

    local i = 0
	for _, buffId in pairs(buffs) do
		TargetBuffs[buffId] = CreateFrame(""frame"")

        TargetBuffs[buffId]:SetSize(size, size)

        TargetBuffs[buffId]:SetPoint(""TOPLEFT"", i* size, -(size* 10))                     -- column 1+ row 11
		TargetBuffs[buffId].t = TargetBuffs[buffId]:CreateTexture()

        TargetBuffs[buffId].t:SetColorTexture(1, 1, 1, 1)

        TargetBuffs[buffId].t:SetAllPoints(TargetBuffs[buffId])

        TargetBuffs[buffId]:Show()


        TargetBuffs[buffId]:SetScript(""OnUpdate"", TargetBuff)

        i = i + 1
	end

    unitCombatFrame = CreateFrame(""frame"");
    unitCombatFrame:SetSize(size, size);
    unitCombatFrame:SetPoint(""TOPLEFT"", 0, -size* 11)           -- column 1 row 12 <-------
    unitCombatFrame.t = unitCombatFrame:CreateTexture()
    unitCombatFrame.t:SetColorTexture(1, 1, 1, 1)
    unitCombatFrame.t:SetAllPoints(unitCombatFrame)
    unitCombatFrame:Show()
    unitCombatFrame:SetScript(""OnUpdate"", updateCombat)

    local function PlayerNotMove()
        if GetUnitSpeed(""Player"") == 0
        then
        Movetime = GetTime()
        PlayerMovingFrame.t:SetColorTexture(0, 0, 0, 1)
        else PlayerMovingFrame.t:SetColorTexture(1, 0, 0, 1)
        end
    end


    PlayerMovingFrame = CreateFrame(""frame"");
    PlayerMovingFrame:SetSize(size, size);
    PlayerMovingFrame:SetPoint(""TOPLEFT"", 0, -size* 9)           -- column 1 row 10 <-------
    PlayerMovingFrame.t = PlayerMovingFrame:CreateTexture()
    PlayerMovingFrame.t:SetColorTexture(1, 1, 1, 1)
    PlayerMovingFrame.t:SetAllPoints(PlayerMovingFrame)
    PlayerMovingFrame:Show()    
    PlayerMovingFrame:SetScript(""OnUpdate"", PlayerNotMove)


    local function AutoAtacking()
        if IsCurrentSpell(6603)
        then
            AutoAtackingFrame.t:SetColorTexture(1, 0, 0, 1)
        else AutoAtackingFrame.t:SetColorTexture(0, 0, 0, 1)
        end
    end


    AutoAtackingFrame = CreateFrame(""frame"");
    AutoAtackingFrame:SetSize(size, size);
    AutoAtackingFrame:SetPoint(""TOPLEFT"", size, -size* 9)           -- column 2 row 10 <-------
    AutoAtackingFrame.t = AutoAtackingFrame:CreateTexture()
    AutoAtackingFrame.t:SetColorTexture(1, 1, 1, 1)
    AutoAtackingFrame.t:SetAllPoints(AutoAtackingFrame)
    AutoAtackingFrame:Show()
    AutoAtackingFrame:SetScript(""OnUpdate"", AutoAtacking)

	
	--print (""Initialization Complete"")
end

local function eventHandler(self, event, ...)
	local arg1 = ...
	if event == ""ADDON_LOADED"" then
		if (arg1 == ""DoIt"") then
			--print(""Addon Loaded... DoIt"")
			--print(""Tracking "" .. table.getn(cooldowns) .. "" cooldowns"")
			initFrames()
		end
	end
end	

f:SetScript(""OnEvent"", eventHandler)
";

    }
}
