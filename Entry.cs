using HarmonyLib;
using System.Reflection;
using VRage.Plugins;

namespace PreventBlockResize {
  public class Entry : IPlugin {
    public void Dispose() {
    }

    public void Init(object gameInstance) {
      var harmony = new Harmony("bcmpinc.PreventBlockResize");
      harmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    public void Update() { }
  }
}
