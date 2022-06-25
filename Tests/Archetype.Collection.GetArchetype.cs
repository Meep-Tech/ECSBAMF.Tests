using Meep.Tech.Data.Examples.ModelWithArchetypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meep.Tech.Data.Tests {
  public partial class Archetype {
    public partial class Collection {
      /// <summary>
      /// Ways to get Archetypes from an Archetype collection
      /// </summary>
      [TestClass]
      public class GetArchetype {

        /// <summary>
        /// Simple get by static identity.
        /// </summary>
        [TestMethod]
        public void GetByExternalId_Success() {
          Assert.AreEqual(
            Apple.Id.Archetype,
            Item.Types.Get("Meep.Tech.Data.Examples.ModelWithArchetypes.Item.Apple")
          );
        }

        /// <summary>
        /// Simple get by static identity.
        /// </summary>
        [TestMethod]
        public void GetByIdentity_Success() {
          Assert.AreEqual(
            Apple.Id.Archetype,
            Item.Types.Get(Apple.Id)
          );
        }

        /// <summary>
        /// Simple get by static identity.
        /// </summary>
        [TestMethod]
        public void GetFromById_Success() {
          Assert.AreEqual(
            Apple.Id.Archetype,
            Item.Types.ById[Apple.Id]
          );
        }

        /// <summary>
        /// Simple get by static identity.
        /// </summary>
        [TestMethod]
        public void GetBySystemTypeRuntime_Success() {
          Assert.AreEqual(
            Apple.Id.Archetype,
            Item.Types.Get(typeof(Apple))
          );
        }

        /// <summary>
        /// Simple get by static identity.
        /// </summary>
        [TestMethod]
        public void GetBySystemTypeGeneric_Success() {
          Assert.AreEqual(
            Apple.Id.Archetype,
            Item.Types.Get<Apple>()
          );
        }
      }
    }
  }
}
