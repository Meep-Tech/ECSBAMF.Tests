﻿using Meep.Tech.Data.Examples.ModelWithArchetypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meep.Tech.Data.Tests {
  public partial class Archetype {
    [TestClass]
    public class Identity {

      /// <summary>
      /// Get a static id
      /// </summary>
      [TestMethod]
      public void Equals_Success() {
        Assert.AreEqual(Apple.Id, Apple.Id);
        Assert.AreEqual(Axe.Id, Axe.Id);
      }

      /// <summary>
      /// Get a static id
      /// </summary>
      [TestMethod]
      public void NotEquals_Success() {
        Assert.AreNotEqual(HealingPotion.Id, Apple.Id.Key);
      }

      /// <summary>
      /// Get a static id
      /// </summary>
      [TestMethod]
      public void StaticIdGet_Success() {
        Assert.AreEqual("Meep.Tech.Data.Examples.ModelWithArchetypes.Item.Apple", Apple.Id.Key);
      }

      /// <summary>
      /// Get a static id
      /// </summary>
      [TestMethod]
      public void StaticIdGetFromString_Success() {
        Assert.AreEqual("Meep.Tech.Data.Examples.ModelWithArchetypes.Item.Apple", Archetypes.Id["Meep.Tech.Data.Examples.ModelWithArchetypes.Item.Apple"].Key);
      }
    }
  }
}
