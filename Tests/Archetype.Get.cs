using Meep.Tech.Data.Examples.ModelWithArchetypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meep.Tech.Data.Tests {
  public partial class Archetype {
    /// <summary>
    /// Getting archetypes from static sources
    /// </summary>
    [TestClass]
    public class Get {

      [TestMethod]
      public void GetByStaticId_Exists() {
        Assert.IsNotNull(Apple.Id.Archetype);
        Assert.IsNotNull(Sword.Id.Archetype);
      }

      [TestMethod]
      public void GetByStaticId_Success() {
        Assert.AreEqual(
          Apple.Id.Archetype.Id.Key,
          "Meep.Tech.Data.Examples.ModelWithArchetypes.Item.Apple"
        );
        Assert.AreEqual(
          Sword.Id.Key,
          "Meep.Tech.Data.Examples.ModelWithArchetypes.Item.Weapon.Sword"
        );
      }

      // TOOD: builder stuff should be under Model.Builder
      public void GetFromSystemType_Success() {
        Assert.AreEqual(Apple.Id.Archetype, typeof(Apple).AsArchetype());
        Assert.AreEqual(Sword.Id.Archetype, typeof(Sword).AsArchetype());
      }

      public void GetFromSystemTypeGenerics_Success() {
        Assert.AreEqual(Apple.Id.Archetype, typeof(Apple).AsArchetype<Item.Type>());
        Assert.AreEqual(Apple.Id.Archetype, typeof(Sword).AsArchetype<Sword>());
        Assert.AreEqual(Apple.Id.Archetype, typeof(Apple).AsArchetype<Apple>());
      }
    }
  }
}
