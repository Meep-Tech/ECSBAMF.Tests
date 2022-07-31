using Meep.Tech.Data.Examples.ModelWithComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Meep.Tech.Data.Tests {
  public partial class Model {
    [TestClass]
    public class Components {

      [TestMethod]
      public void InitModelWithComponentFromArchetype_Success() {
        FluxCapacitor device
          = Device.Types.Get<FluxCapacitor.Type>()
            .GetDefaultBuilders()
              .Make<FluxCapacitor>((nameof(FluxCapacitor.FluxLevel), 30));

        Assert.IsTrue(device.HasComponent(Components<CapacitorData>.Key));
      }

      [TestMethod]
      public void InitModelWithInheritedComponentFromArchetype_Success() {
        FluxCapacitor device
          = Device.Types.Get<PreBuiltCapacitor>()
            .GetDefaultBuilders()
              .Make<FluxCapacitor>((nameof(FluxCapacitor.FluxLevel), 30));

        Assert.IsTrue(device.HasComponent(Components<InfoComponent.Data>.Key));
      }

      [TestMethod]
      public void InitModelWithInheritedComponentFromArchetypeDefaultValueGetter_Success() {
        FluxCapacitor device
          = Device.Types.Get<PreBuiltCapacitor>()
            .GetDefaultBuilders()
              .Make<FluxCapacitor>((nameof(FluxCapacitor.FluxLevel), 30));

        Assert.AreEqual("Default Info", device.GetComponent<InfoComponent.Data>().Text);
      }

      [TestMethod]
      public void AddComponentToModel_Success() {
        ModularFluxCapacitor device
          = Device.Types.Get<ModularFluxCapacitor.Type>()
            .GetDefaultBuilders()
              .Make<ModularFluxCapacitor>((nameof(FluxCapacitor.FluxLevel), 30));

        ManufacturerDetails component =
          Components<ManufacturerDetails>.Factory
            .Make();

        device.AddComponent(component);

        Assert.IsTrue(device.HasComponent(component.GetKey()));
      }

      public void AddExistingComponentToModel_Failure() {
        ModularFluxCapacitor device
          = Device.Types.Get<ModularFluxCapacitor.Type>()
            .Make(out ModularFluxCapacitor _,
              (nameof(FluxCapacitor.FluxLevel), 30)
            );

        CapacitorData component =
          Components<CapacitorData>.Factory
            .Make();

        Assert.ThrowsException<ArgumentException>(() =>
          device.AddComponent(component)
        );
      }

      [TestMethod]
      public void AddRestrictedComponentToRestrictedModel_Success() {
        SafeModularDevice device
          = Device.Types.Get<SafeModularDevice.Type>()
            .Make(out SafeModularDevice _);

        ManufacturerDetails component =
          Components<ManufacturerDetails>.Factory.Make(
            (nameof(ManufacturerDetails.ManufacturerName), "test company"),
            ("ManufactureDate", new DateTime()));

        device.AddComponent(component);

        Assert.AreEqual(
          component,
          device.GetComponent(component.GetKey())
        );
      }

      [TestMethod]
      public void RemoveComponentFromWriteableModel_Success() {
        DangerousModularDevice device
          = Device.Types.Get<DangerousModularDevice.Type>()
            .GetDefaultBuilders()
              .Make<DangerousModularDevice>();

        ManufacturerDetails component =
          Components<ManufacturerDetails>.Factory
            .Make();

        device.AddComponent(component);
        device.RemoveComponent(component.GetKey());

        Assert.IsFalse(
          device.HasComponent(component.GetKey())
        );
      }
    }
  }
}
