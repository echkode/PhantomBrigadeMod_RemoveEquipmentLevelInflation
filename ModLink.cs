using HarmonyLib;
using PBModManager = PhantomBrigade.Mods.ModManager;
using UnityEngine;

namespace EchKode.PBMods.RemoveEquipmentLevelInflation
{
	public class ModLink : PhantomBrigade.Mods.ModLink
	{
		internal static int modIndex;
		internal static string modID;
		internal static string modPath;

		public override void OnLoad(Harmony harmonyInstance)
		{
			modIndex = PBModManager.loadedMods.Count;
			modID = metadata.id;
			modPath = metadata.path;
			var patchAssembly = typeof(ModLink).Assembly;
			Debug.LogFormat(
				"Mod {0} is executing OnLoad | Using HarmonyInstance.PatchAll on assembly ({1}) | Directory: {2} | Full path: {3}",
				metadata.id,
				patchAssembly.FullName,
				metadata.directory,
				metadata.path);
			harmonyInstance.PatchAll(patchAssembly);
		}
	}
}
