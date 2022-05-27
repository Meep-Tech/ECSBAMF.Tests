using Meep.Tech.Data.Examples;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Meep.Tech.Data.Tests {
  [TestClass]
  public class Components {

    [TestMethod]
    public void SerializeArchetypeComponent() {
      Capacitor capacitor = Components<Capacitor>.BuilderFactory.Make(
        (nameof(Capacitor.DefaultCapacityValue), 103)  
      );

      JObject json = capacitor.ToJson();
      Assert.AreEqual(103, json.TryGetValue<int>(nameof(Capacitor.DefaultCapacityValue)));
    }

    [TestMethod]
    public void SerializeModelComponent() {
      CapacitorData capacitor = new CapacitorData(103);

      JObject json = capacitor.ToJson();
      Assert.AreEqual(103, json.TryGetValue<int>(nameof(CapacitorData.Value)));
    }
  }
}
