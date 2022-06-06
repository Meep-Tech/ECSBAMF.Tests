using Meep.Tech.Data.Examples.AutoBuilder;
using Meep.Tech.Data.Examples.ModelWithArchetypes;
using Meep.Tech.Data.Examples.ModelWithComponents;
using Meep.Tech.Data.Examples.StructOnlyModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using static Meep.Tech.Data.Examples.AutoBuilder.Animal;

namespace Meep.Tech.Data.Tests {

  [TestClass]
  public class Model {

    [TestMethod]
    public void ConstructionWithoutNeededParameter_Failure() {
      Assert.ThrowsException<IModel.Builder.Param.MissingException>(() => {
        Device.Types.Get<ModularFluxCapacitor.Type>()
          .Make();
      });
    }

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

    [TestClass]
    public class Builder {

      [TestClass]
      public class Auto {

        [TestMethod]
        public void MakeWithAutoBuilderRequiredField_Snake_Name_Success() {
          const string snakeName = "Snakey";
          var snake = Animal.Types.Get<Snake>()
            .Make<Animal>((nameof(Animal.Name), snakeName));

          Assert.AreEqual(snakeName, snake.Name);
        }

        [TestMethod]
        public void MakeWithAutoBuilderOverrideDefaultField_Snake_NumberOfLegs_Success() {
          const string snakeName = "Snakey";
          var snake = Animal.Types.Get<Snake>()
            .Make((nameof(Animal.Name), snakeName), (nameof(Animal.NumberOfLegs), 1));

          Assert.AreEqual(1, snake.NumberOfLegs);
        }

        [TestMethod]
        public void MakeWithAutoBuilderOverrideDefaultField_Snake_CanClimb_Success() {
          const string snakeName = "Snakey";
          var snake = Animal.Types.Get<Snake>()
            .Make((nameof(Animal.Name), snakeName), (nameof(Animal.IsAClimber), false));

          Assert.AreEqual(false, snake.IsAClimber);
        }

        [TestMethod]
        public void MakeWithAutoBuilderDefaultField_Snake_CanClimb_Success() {
          const string snakeName = "Snakey";
          var snake = Animal.Types.Get<Snake>()
            .Make((nameof(Animal.Name), snakeName));

          Assert.AreEqual(snake.Archetype.CanClimb, snake.IsAClimber);
        }

        [TestMethod]
        public void MakeWithAutoBuilderOverrideDefaultField_Snake_NumberOfLegs_Failure() {
          const string snakeName = "Snakey";
          Assert.ThrowsException<ArgumentException>(() =>
            Animal.Types.Get<Snake>()
              .Make((nameof(Animal.Name), snakeName), (nameof(Animal.NumberOfLegs), -1)));
        }

        [TestMethod]
        public void MakeWithAutoBuilderDefaultField_Snake_NumberOfLegs_Success() {
          const string snakeName = "Snakey";
          var snake = Animal.Types.Get<Snake>()
            .Make<Animal>((nameof(Animal.Name), snakeName));

          Assert.AreEqual(snake.Archetype.DefaultNumberOfLegs, snake.NumberOfLegs);
        }

        [TestMethod]
        public void MakeWithAutoBuilderRequiredFieldMissing_Snake_Name_Failure() {
          Assert.ThrowsException<IModel.Builder.Param.MissingException>(() => 
            Animal.Types.Get<Snake>()
              .Make<Animal>());
        }

        [TestMethod]
        public void MakeWithAutoBuilderVirtualDefaultField_Cat_Name_Success() {
          var cat = Animal.Types.Get<Cat.Type>()
            .Make<Animal>();

          Assert.AreEqual("Kitty", cat.Name);
        }

        [TestMethod]
        public void MakeWithAutoBuilderVirtualOverrideDefaultField_Cat_Name_Success() {
          const string catName = "Mowster";
          var cat = Animal.Types.Get<Cat.Type>()
            .Make<Animal>((nameof(Animal.Name), catName));

          Assert.AreEqual(catName, cat.Name);
        }

        [TestMethod]
        public void MakeWithAutoBuilderAttributeDefaultField_Dog_Name_Success() {
          var dog = Animal.Types.Get<Dog.Type>()
            .Make<Animal>();

          Assert.AreEqual("Friend", dog.Name);
        }

        [TestMethod]
        public void MakeWithAutoBuilderAttributeOverrideDefaultField_Dog_Name_Success() {
          const string dogName = "goofy";
          var dog = Animal.Types.Get<Dog.Type>()
            .Make<Animal>((nameof(Animal.Name), dogName));

          Assert.AreEqual(dogName, dog.Name);
        }
      }

      [TestMethod]
      public void MakesModelWithCorrectArchetype_DoubleGeneric_ClassBased() {
        var apple = Archetypes<Apple>._.Make();
        Assert.AreEqual(Archetypes<Apple>._, apple.Archetype);
      }

      [TestMethod]
      public void MakesModelWithCorrectArchetype_DoubleGeneric_FromInterface() {
        var grassTile = Tile.Types.Get<Grass>().Make();
        Assert.AreEqual(Archetypes<Grass>._, grassTile.Archetype);
      }

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
