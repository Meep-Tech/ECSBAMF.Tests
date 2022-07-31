using Meep.Tech.Data.Examples.SimpleModelsWithOneFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meep.Tech.Data.Tests {
  public partial class Model {

    [TestClass]
    public class Factory {

      [TestMethod]
      public void TestBuildModelFromFactory_User() {
        IModel<User>.Factory factory = Models<User>.Factory;

        var user = factory.Make(((string key, object value))(nameof(User.Name), "tom"));
        Assert.IsInstanceOfType(user, typeof(User));
      }

      [TestMethod]
      public void TestBuildModelFromFactory_AutoBuildField_User() {
        var name = "tom";
        var user = Models<User>.Factory.Make((nameof(User.Name), name));
        Assert.AreEqual(name, user.Name);
      }

      [TestMethod]
      public void TestBuildModelFromFactory_AutoBuildPostAction_User() {
        var name = "harold";
        var user = Models<User>.Factory.Make((nameof(User.Name), name));
        Assert.AreEqual("was-harold", user.Name);
      }

      [TestMethod]
      public void TestBuildModelFromFactory_Character() {
        var character = Models<Character>.Factory.Make((nameof(Character.Name), "tom"));
        Assert.IsInstanceOfType(character, typeof(Character));
      }

      [TestMethod]
      public void TestBuildModelFromFactory_FactoryIsCorrect_Character() {
        Assert.AreEqual(nameof(Character) + ".Test", Models<Character>.Factory.Id.Name);
      }

      [TestMethod]
      public void TestBuildModelFromFactory_FactoryModelCtorSet_Character() {
        var factory = Models<Character>.Factory;
        var modelCtorProperty = factory.GetType().GetProperty("ModelConstructor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var modelCtor = modelCtorProperty.GetValue(factory);
        Assert.IsNotNull(modelCtor);
      }

      [TestMethod]
      public void TestBuildModelFromFactory_AutoBuildField_Character() {
        var name = "tom";
        var character = Models<Character>.Factory.Make((nameof(Character.Name), name));
        Assert.AreEqual(name, character.Name);
      }

      [TestMethod]
      public void TestBuildModelFromFactory_World() {
        var world = Models<World>.Factory.Make();
        Assert.IsInstanceOfType(world, typeof(World));
      }

      [TestMethod]
      public void TestBuildModelFromFactory_FactoryIsCorrect_World() {
        Assert.IsInstanceOfType(Models<World>.Factory, typeof(WorldFactory));
      }

      [TestMethod]
      public void TestBuildModelFromFactory_FactoryModelCtorSet_World() {
        var factory = Models<World>.Factory;
        var modelCtorProperty = factory.GetType().GetProperty("ModelConstructor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var modelCtor = modelCtorProperty.GetValue(factory);
        Assert.IsNotNull(modelCtor);
      }
    }
  }
}
