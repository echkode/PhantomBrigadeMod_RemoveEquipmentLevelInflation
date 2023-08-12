// Copyright (c) 2023 EchKode
// SPDX-License-Identifier: BSD-3-Clause

using PhantomBrigade.Data;

using UnityEngine;

namespace EchKode.PBMods.RemoveEquipmentLevelInflation
{
	internal static class UnitUtilities
	{
		internal static void CreateSubsystemEntity(string subsystemBlueprintName, EquipmentEntity __result)
		{
			if (__result == null)
			{
				Debug.LogWarningFormat(
					"Mod {0} ({1}) CreateSubsystemEntity() returned null equipment entity | subsystem: {2}",
					ModLink.modIndex,
					ModLink.modID,
					subsystemBlueprintName);
				return;
			}
			__result.ReplaceLevel(1);
		}

		internal static void CreatePartEntityFromPreset(DataContainerPartPreset partPreset, EquipmentEntity __result)
		{
			if (__result == null)
			{
				Debug.LogWarningFormat(
					"Mod {0} ({1}) CreatePartEntityFromPreset() returned null equipment entity | part: {2}",
					ModLink.modIndex,
					ModLink.modID,
					partPreset.key);
				return;
			}
			__result.ReplaceLevel(1);
			DataHelperStats.RefreshStatCacheForPart(__result);
		}
	}
}
