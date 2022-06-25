using Meep.Tech.Data.Examples.ModelWithArchetypes;
using Meep.Tech.Data.Examples.UnloadableArchetypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Meep.Tech.Data.Tests {
  public partial class Archetype {
    [TestClass]
    public class Unloading {

      [TestMethod]
      public void UnloadedTypeNotFoundInTypesAll() {
        FishTaco toUnload = Item.Types.Get<FishTaco>();
        toUnload.Unload();
        Assert.IsFalse(Item.Types.All.Contains(toUnload));
      }

      [TestMethod]
      public void UnloadedTypeNotFoundInAllAll() {
        FishTaco toUnload = Item.Types.Get<FishTaco>();
        toUnload.Unload();
        Assert.IsFalse(Archetypes.All.Contains(toUnload));
      }

      [TestMethod]
      public void UnloadedTypeNotFoundStaticAccess() {
        FishTaco toUnload = Item.Types.Get<FishTaco>();
        toUnload.Unload();
        Assert.ThrowsException<System.Collections.Generic.KeyNotFoundException>(
         () => Archetypes<FishTaco>._
        );
      }
    }
  }
}
