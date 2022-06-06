using Meep.Tech.Data.Examples.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Meep.Tech.Data.Tests {

  [TestClass]
  public class Enumeration {

    [TestMethod]
    public void DefaultKey_IsEqual() {
      Assert.AreEqual($"{nameof(FruitType)}.{nameof(FruitType.Apple)}", FruitType.Apple.ExternalId);
    }

    [TestMethod]
    public void EditedKey_IsEqual() {
      Assert.AreEqual(nameof(TreeType.Poplar), TreeType.Poplar.ExternalId);
    }

    [TestMethod]
    public void GetFromUniverse_Generic_Success() {
      var foundType = Archetypes.DefaultUniverse.Enumerations.Get<AnimalType>($"{nameof(AnimalType)}.{nameof(AnimalType.Frog)}");
      Assert.AreEqual(
        AnimalType.Frog,
        foundType
      );
    }

    [TestMethod]
    public void GetFromUniverse_Success() {
      var foundType = Archetypes.DefaultUniverse.Enumerations.Get(typeof(AnimalType), $"{nameof(AnimalType)}.{nameof(AnimalType.Frog)}");

      Assert.AreEqual(
        AnimalType.Frog,
        foundType
      );
    }

    [TestMethod]
    public void GetFromUniverse_ByBasicObjectKey_Success() {
      object key = nameof(AnimalType.Frog);
      object externalId = AnimalType.GetUniqueIdFromBaseObjectKey(key);

      Assert.AreEqual(
        AnimalType.Frog.ExternalId,
        externalId
      );
    }

    [TestMethod]
    public void GetFromUniverse_ByBasicObjectKey_Alt_Success() {
      object key = nameof(TreeType.Poplar);
      object externalId = TreeType.GetUniqueIdFromBaseObjectKey(key);

      Assert.AreEqual(
        TreeType.Poplar.ExternalId,
        externalId
      );
    }

    [TestMethod]
    public void AllContainsAny_IsTrue() {
      Assert.IsTrue(AnimalType.All.Contains(AnimalType.Frog));
    }

    [TestMethod]
    public void AllContainsCorrectCount_InitialLoad_4() {
      Assert.AreEqual(4, FruitType.All.Count());
    }

    [TestMethod]
    public void AllContainsCorrectCount_AfterLazyload_5() {
      var banana = LazyAdditionalFruitTypes.Banana;
      Assert.AreEqual(banana, LazyAdditionalFruitTypes.Banana);
      Assert.AreEqual(5, FruitType.All.Count());
    }
  }
}
