using Meep.Tech.Data.Examples.ModelWithComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Meep.Tech.Data.Tests {
  public partial class Model {
    [TestClass]
    public class Components {

      [TestMethod]
      public void AddComponentToModel_Success() {
        ModularFluxCapacitor device
          = Device.Types.Get<ModularFluxCapacitor.Type>()
            .DefaultModelBuilders()
              .Make<ModularFluxCapacitor>((nameof(FluxCapacitor.FluxLevel), 30));

        ManufacturerDetails component =
          Components<ManufacturerDetails>.BuilderFactory
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
          Components<CapacitorData>.BuilderFactory
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
          Components<ManufacturerDetails>.BuilderFactory.Make(
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
            .DefaultModelBuilders()
              .Make<DangerousModularDevice>();

        ManufacturerDetails component =
          Components<ManufacturerDetails>.BuilderFactory
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
