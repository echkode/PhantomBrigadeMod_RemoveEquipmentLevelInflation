using PhantomBrigade.Data;

namespace EchKode.PBMods.RemoveEquipmentLevelInflation
{
	internal static class DataManagerSave
	{
		internal static void LoadPartEntity(EquipmentEntity __result)
		{
			if (__result == null)
			{
				return;
			}
			__result.ReplaceLevel(1);
			DataHelperStats.RefreshStatCacheForPart(__result);
		}
	}
}
