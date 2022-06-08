using Meep.Tech.Data.Examples.ModelWithArchetypes;
using Meep.Tech.Data.Examples.ModelWithComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Meep.Tech.Data.Tests {
  public partial class Model {
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
        ModularFluxCapacitor device = Device.Types.Get<ModularFluxCapacitor.Type>()
          .DefaultModelBuilders()
            .Make<ModularFluxCapacitor>(("FluxLevel", 1));
        device.AddNewComponent<ManufacturerDetails>(
          (nameof(ManufacturerDetails.ManufacturerName), "test")
        );

        JObject itemJson = device.ToJson();

        Device deserializedItem = (Device)IModel.FromJson(itemJson);

        Assert.AreEqual(device.ToJson().ToString(), deserializedItem.ToJson().ToString());
      }
    }
  }
}
