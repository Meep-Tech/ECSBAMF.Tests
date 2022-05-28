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

    [TestMethod]
    public void IDoOnAdd_AfterTrue_Success() {
      var machine = Device.Types.Get<DangerousModularDevice.Type>()
        .DefaultModelBuilders().Make<DangerousModularDevice>();

      DisplayComponent component = Components<DisplayComponent>.BuilderFactory.Make();
      machine.AddComponent(component);

      Assert.IsTrue(component.WasInitialized);
    }

    [TestMethod]
    public void IDoOnAdd_BeforeFalse_Success() {
      var machine = Device.Types.Get<DangerousModularDevice.Type>()
        .DefaultModelBuilders().Make<DangerousModularDevice>();

      DisplayComponent component = Components<DisplayComponent>.BuilderFactory.Make();

      Assert.IsFalse(component.WasInitialized);
    }

    [TestClass]
    public class Contracts {

      [TestMethod]
      public void TestContractExecuted_CapacitorFirst_Success() {
        var machine = Device.Types.Get<DangerousModularDevice.Type>()
          .DefaultModelBuilders().Make<DangerousModularDevice>();

        CapacitorData capacitor = Components<CapacitorData>.BuilderFactory.Make(
          (nameof(CapacitorData.Value), 103)  
        );
        machine.AddComponent(capacitor);

        CapacitorDetector detector = Components<CapacitorDetector>.BuilderFactory.Make();
        machine.AddComponent(detector);

        Assert.IsTrue(detector.CapacitorWasDetected);
      }

      [TestMethod]
      public void TestContractExecuted_CapacitorSecond_Success() {
        var machine = Device.Types.Get<DangerousModularDevice.Type>()
          .DefaultModelBuilders().Make<DangerousModularDevice>();

        CapacitorDetector detector = Components<CapacitorDetector>.BuilderFactory.Make();
        machine.AddComponent(detector);
        
        CapacitorData capacitor = Components<CapacitorData>.BuilderFactory.Make(
          (nameof(CapacitorData.Value), 103)  
        );
        machine.AddComponent(capacitor);

        Assert.IsTrue(detector.CapacitorWasDetected);
      }

      [TestMethod]
      public void TestContractExecuted_MultiComponentContracts_First_Success() {
        var machine = Device.Types.Get<DangerousModularDevice.Type>()
          .DefaultModelBuilders().Make<DangerousModularDevice>();

        MultiComponentDetector detector = Components<MultiComponentDetector>.BuilderFactory.Make();
        machine.AddComponent(detector);
        
        CapacitorData capacitor = Components<CapacitorData>.BuilderFactory.Make(
          (nameof(CapacitorData.Value), 103)  
        );
        machine.AddComponent(capacitor);

        Assert.IsTrue(detector.CapacitorWasDetected);
      }

      [TestMethod]
      public void TestContractExecuted_MultiComponentContracts_FirstAndSecond_Success() {
        var machine = Device.Types.Get<DangerousModularDevice.Type>()
          .DefaultModelBuilders().Make<DangerousModularDevice>();

        MultiComponentDetector detector = Components<MultiComponentDetector>.BuilderFactory.Make();
        machine.AddComponent(detector);
        
        CapacitorData capacitor = Components<CapacitorData>.BuilderFactory.Make(
          (nameof(CapacitorData.Value), 103)  
        );
        machine.AddComponent(capacitor);
        DisplayComponent display = Components<DisplayComponent>.BuilderFactory.Make();
        machine.AddComponent(display);

        Assert.IsTrue(detector.CapacitorWasDetected && detector.DisplayComponentWasDetected);
      }

      [TestMethod]
      public void TestContractExecuted_MultiComponentContracts_Second_Success() {
        var machine = Device.Types.Get<DangerousModularDevice.Type>()
          .DefaultModelBuilders().Make<DangerousModularDevice>();
        
        MultiComponentDetector detector = Components<MultiComponentDetector>.BuilderFactory.Make();
        machine.AddComponent(detector);
        
        CapacitorData capacitor = Components<CapacitorData>.BuilderFactory.Make(
          (nameof(CapacitorData.Value), 103)  
        );
        machine.AddComponent(capacitor);
        DisplayComponent display = Components<DisplayComponent>.BuilderFactory.Make();
        machine.AddComponent(display);

        Assert.IsTrue(detector.DisplayComponentWasDetected);
      }

      [TestMethod]
      public void TestContractExecuted_MultiComponentContracts_FirstAndSecond_DetectorLast_Success() {
        var machine = Device.Types.Get<DangerousModularDevice.Type>()
          .DefaultModelBuilders().Make<DangerousModularDevice>();
        
        MultiComponentDetector detector = Components<MultiComponentDetector>.BuilderFactory.Make();
        CapacitorData capacitor = Components<CapacitorData>.BuilderFactory.Make(
          (nameof(CapacitorData.Value), 103)  
        );
        DisplayComponent display = Components<DisplayComponent>.BuilderFactory.Make();

        machine.AddComponent(capacitor);
        machine.AddComponent(display);
        machine.AddComponent(detector);


        Assert.IsTrue(detector.CapacitorWasDetected);
      }

      [TestMethod]
      public void TestContractExecuted_MultiComponentContracts_FirstAndSecond_DetectorMiddle_Success() {
        var machine = Device.Types.Get<DangerousModularDevice.Type>()
          .DefaultModelBuilders().Make<DangerousModularDevice>();
        
        MultiComponentDetector detector = Components<MultiComponentDetector>.BuilderFactory.Make();
        CapacitorData capacitor = Components<CapacitorData>.BuilderFactory.Make(
          (nameof(CapacitorData.Value), 103)  
        );
        DisplayComponent display = Components<DisplayComponent>.BuilderFactory.Make();

        machine.AddComponent(capacitor);
        machine.AddComponent(detector);
        machine.AddComponent(display);


        Assert.IsTrue(detector.CapacitorWasDetected);
      }

      [TestMethod]
      public void TestContractNotExecuted_CapacitorNotAdded_Success() {
        var machine = Device.Types.Get<DangerousModularDevice.Type>()
          .DefaultModelBuilders().Make<DangerousModularDevice>();

        CapacitorDetector detector = Components<CapacitorDetector>.BuilderFactory.Make();
        machine.AddComponent(detector);

        Assert.IsFalse(detector.CapacitorWasDetected);
      }
    }
  }
}
