// Copyright (c) 2023 EchKode
// SPDX-License-Identifier: BSD-3-Clause

using HarmonyLib;

using PhantomBrigade.Data;
using PBDataManagerSave = PhantomBrigade.Data.DataManagerSave;
using PBDataMultiLinkerUnitStats = PhantomBrigade.Data.DataMultiLinkerUnitStats;
using PBUnitUtilities = PhantomBrigade.Data.UnitUtilities;

using UnityEngine;

namespace EchKode.PBMods.RemoveEquipmentLevelInflation
{
	[HarmonyPatch]
	static class Patch
	{
		[HarmonyPatch(typeof(PBDataManagerSave), "LoadPartEntity")]
		[HarmonyPostfix]
		static void Dms_LoadPartEntityPostfix(ref EquipmentEntity __result)
		{
			DataManagerSave.LoadPartEntity(__result);
		}

		[HarmonyPatch(typeof(PBDataMultiLinkerUnitStats), "OnAfterDeserialization")]
		[HarmonyPostfix]
		static void Dmus_OnAfterDeserializationPostfix()
		{
			foreach (var kvp in PBDataMultiLinkerUnitStats.data)
			{
				if (kvp.Value.increasePerLevel == null)
				{
					continue;
				}

				var increase = kvp.Value.increasePerLevel.f;
				kvp.Value.increasePerLevel = null;
				Debug.LogFormat(
					"Mod {0} ({1}) Removed increase per level | stat: {2} | value: {3}",
					ModLink.modIndex,
					ModLink.modID,
					kvp.Key,
					increase);
			}
		}

		[HarmonyPatch(typeof(PBUnitUtilities), "CreateSubsystemEntity")]
		[HarmonyPostfix]
		static void Uu_CreateSubsystemEntityPostfix(string subsystemBlueprintName, ref EquipmentEntity __result)
		{
			UnitUtilities.CreateSubsystemEntity(subsystemBlueprintName, __result);
		}

		[HarmonyPatch(typeof(PBUnitUtilities), "CreatePartEntityFromPreset", new System.Type[] { typeof(DataContainerPartPreset), typeof(float), typeof(float), typeof(int), typeof(int) })]
		[HarmonyPostfix]
		static void Uu_CreatePartEntityFromPresetPostfix(DataContainerPartPreset partPreset, ref EquipmentEntity __result)
		{
			UnitUtilities.CreatePartEntityFromPreset(partPreset, __result);
		}

		[HarmonyPatch(typeof(PBUnitUtilities), "CreateSubsystemsFromSave")]
		[HarmonyPrefix]
		static void Uu_CreateSubsystemsFromSavePrefix(EquipmentEntity part)
		{
			part.ReplaceLevel(1);
		}
	}
}
