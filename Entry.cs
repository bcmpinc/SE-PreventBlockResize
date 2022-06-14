using HarmonyLib;
using Sandbox.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage.Plugins;

namespace PreventBlockResize
{
    public class Entry : IPlugin {
        public void Dispose() {
        }

        public void Init(object gameInstance) {
            var harmony = new Harmony("bcmpinc.PreventBlockResize");
            harmony.PatchAll();
        }

        public void Update() {}
    }
}
