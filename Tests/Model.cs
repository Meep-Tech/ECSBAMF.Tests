﻿using Meep.Tech.Data.Examples;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;

namespace Meep.Tech.Data.Tests {

  [TestClass]
  public class Model {

    [TestMethod]
    public void ConstructionWithoutNeededParameter_Failure() {
      Assert.ThrowsException<IModel.Builder.Param.MissingException>(() => {
        ModularFluxCapacitor device
          = Device.Types.Get<ModularFluxCapacitor.Type>()
            .Make<ModularFluxCapacitor>();
      });
    }

    [TestClass]
    public class Components {

      [TestMethod]
      public void AddComponentToModel_Success() {
        ModularFluxCapacitor device
          = Device.Types.Get<ModularFluxCapacitor.Type>()
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
            .Make<ModularFluxCapacitor>((nameof(FluxCapacitor.FluxLevel), 30));

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
            .Make<SafeModularDevice>();

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

    [TestClass]
    public class Builder {

      [TestMethod]
      public void MakeNewBuilderAndCopyParamsTest() {

      }
    }

    [TestClass]
    public class JsonSerialization {

      [TestMethod]
      public void SerializeSimpleItem_Success() {
        Item item = Item.Types.Get<Sword>().Make<Weapon>();
        JObject itemJson = item.ToJson();

        Item deserializedItem = (Item)IModel.FromJson(itemJson);

        Assert.AreEqual(((IUnique)item).Id, ((IUnique)deserializedItem).Id);
      }

      [TestMethod]
      public void SerializeUniqueId_Success() {
        Item item = Item.Types.Get<Sword>().Make<Weapon>();
        JObject itemJson = item.ToJson();

        Assert.AreEqual(((IUnique)item).Id, itemJson.Value<string>("id"));
      }

      [TestMethod]
      public void SerializedUniqueIdDeserialization_Success() {
        Item item = Item.Types.Get<Sword>().Make<Weapon>();
        JObject itemJson = item.ToJson();

        Item deserializedItem = (Item)IModel.FromJson(itemJson);

        Assert.AreEqual(((IUnique)item).Id, ((IUnique)deserializedItem).Id);
      }

      [TestMethod]
      public void SerializeSimpleItemDeserialization_Success() {
        Item item = Item.Types.Get<Sword>().Make<Weapon>();
        JObject itemJson = item.ToJson();

        Item deserializedItem = (Item)IModel.FromJson(itemJson);

        Assert.AreEqual(item, deserializedItem);
      }

      [TestMethod]
      public void SerializeSimpleItemDeserializationBaseGeneric_Success() {
        Item item = Item.Types.Get<Sword>().Make<Weapon>();
        JObject itemJson = item.ToJson();

        Item deserializedItem = IModel<Item>.FromJson(itemJson);

        Assert.AreEqual(item, deserializedItem);
      }

      [TestMethod]
      public void SerializeSimpleItemDeserializationGeneric_Success() {
        Item item = Item.Types.Get<Sword>().Make<Weapon>();
        JObject itemJson = item.ToJson();

        Item deserializedItem = Item.FromJson(itemJson);

        Assert.AreEqual(item, deserializedItem);
      }

      [TestMethod]
      public void SerializeSimpleItemDeserializationGenericConvert_Success() {
        Item item = Item.Types.Get<Sword>().Make<Weapon>();
        JObject itemJson = item.ToJson();

        Item deserializedItem = Item.FromJsonAs<Weapon>(itemJson);

        Assert.AreEqual(item, deserializedItem);
      }

      [TestMethod]
      public void SerializeSimpleItemDeserializationSpecificType_Success() {
        Item item = Item.Types.Get<Sword>().Make<Weapon>();
        JObject itemJson = item.ToJson();

        Item deserializedItem = Item.FromJsonAs<Weapon>(itemJson, typeof(Weapon));

        Assert.AreEqual(item, deserializedItem);
      }

      [TestMethod]
      public void SimpleItemReserialization_Success() {
        Item item = Item.Types.Get<Sword>().Make<Weapon>();
        JObject itemJson = item.ToJson();

        Item deserializedItem = (Item)IModel.FromJson(itemJson);

        Assert.AreEqual(item.ToJson().ToString(), deserializedItem.ToJson().ToString());
      }

      [TestMethod]
      public void ItemWithComponentsReserialization_Success() {
        ModularFluxCapacitor item = Device.Types.Get<ModularFluxCapacitor.Type>()
          .Make<ModularFluxCapacitor>(("FluxLevel", 1));
        item.AddNewComponent<ManufacturerDetails>(
          (nameof(ManufacturerDetails.ManufacturerName), "test")
        );

        JObject itemJson = item.ToJson();

        Device deserializedItem = (Device)IModel.FromJson(itemJson);

        Assert.AreEqual(item.ToJson().ToString(), deserializedItem.ToJson().ToString());
      }
    }
  }
}
