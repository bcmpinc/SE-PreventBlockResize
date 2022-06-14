using HarmonyLib;
using Sandbox.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PreventBlockResize {

    [HarmonyPatch(typeof(MyCubeBuilder))] 
    class PatchMyCubeBuilder : MyCubeBuilder {

        internal static PropertyInfo GetProperty(Type type, string property) {
            var res = AccessTools.Property(type, property);
            if (res == null) {
                throw new MissingMemberException($"ERROR: property {type}.{property} not found.");
            }
            return res;
        }

		[HarmonyTranspiler]
		[HarmonyPatch(nameof(PatchMyCubeBuilder.CurrentGrid), MethodType.Setter)]
        static IEnumerable<CodeInstruction> Transpiler_CurrentGrid(IEnumerable<CodeInstruction> instructions) {
            List<CodeInstruction> list = instructions.ToList();
            
            // Find `if (this.IsCubeSizeModesAvailable`
            int start = list.FindIndex(i => 
                i.opcode == OpCodes.Callvirt && 
                i.operand.Equals(GetProperty(typeof(MyCubeBuilder), nameof(MyCubeBuilder.IsCubeSizeModesAvailable)).GetGetMethod())
            ) - 1;
            
            // Determine end of if block
            int end = list.FindIndex(i => i.labels.Contains((Label)list[start+2].operand));

            // Remove if block
            list.RemoveRange(start, end-start);
            return list;
        }
    }
}
