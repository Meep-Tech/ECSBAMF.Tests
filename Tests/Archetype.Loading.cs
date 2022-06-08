using Meep.Tech.Data.Configuration;
using Meep.Tech.Data.Examples.Enumerations;
using Meep.Tech.Data.Examples.ModelWithArchetypes;
using Meep.Tech.Data.Examples.SplayedArchetype;
using Meep.Tech.Data.Examples.UnloadableArchetypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meep.Tech.Data.Tests {
  public partial class Archetype {
    [TestClass]
    public class Loading {

      [AssemblyInitialize]
      public static void Initialize(TestContext _) {
        Loader.Settings settings = new Loader.Settings() {
          UniverseName = "Meep.Tech.ECSBAM.Tests",
          PreLoadAssemblies = new() {
            typeof(Meep.Tech.Data.Examples.ModelWithArchetypes.Apple).Assembly
          }
        };

        settings.ArchetypeAssemblyPrefixesToIgnore
          .Remove("Meep.Tech.Data");

        new Loader(settings)
          .Initialize();
      }

      [TestMethod]
      public void LoadTypeAfterSealing_Success() {
        BeefJerkey newType = new BeefJerkey();
        string _ = newType.ToString();
        Assert.IsNotNull(Item.Types.Get<BeefJerkey>());
      }

      [TestMethod]
      public void LoadTypeAfterSealingEqual_Success() {
        Meep.Tech.Data.Archetype newType = new BeefJerkey();
        Assert.AreEqual(newType.Id, Item.Types.Get(newType.GetType()).Id);
      }

      [TestMethod]
      public void LoadTypeBeforeLoaded_Failure() {
        Assert.ThrowsException<System.Collections.Generic.KeyNotFoundException>(
         () => Item.Types.Get<BeefJerkey>()
        );
      }

      [TestMethod]
      public void LoadTypeAfterSealing_Failure() {
        Assert.ThrowsException<System.InvalidOperationException>(
         () => new Apple("")
        );
      }

      public void SplayedTypesLoadedOnInitialization_Success() {
        Assert.AreEqual(
          FruitType.Apple,
          (Sticker.Types.Get<Sticker.Type>()
            .For(FruitType.Apple) 
              as Data.Archetype.IBuildOneForEach<FruitType, Sticker.Type>)
                .AssociatedEnum
        );
      }
    }
  }
}
