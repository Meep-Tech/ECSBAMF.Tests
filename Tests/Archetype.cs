using Meep.Tech.Data.Configuration;
using Meep.Tech.Data.Examples;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meep.Tech.Data.Tests {

  [TestClass]
  public class Archetype {

    [TestMethod]
    public void IsBaseFalse_Success() {
      Assert.IsFalse(Apple.Id.Archetype.IsBase);
    }

    [TestMethod]
    public void BaseTypeEqualsAbstract_Success() {
      Assert.AreEqual(
        Apple.Id.Archetype.BaseType,
        typeof(Item.Type)
      );
    }
    

    [TestMethod]
    public void EqualityOpperator_Success() {
      Assert.IsTrue(Apple.Id.Archetype == Apple.Id.Archetype);
      Assert.IsTrue(Sword.Id.Archetype == Sword.Id.Archetype);
    }
    

    [TestMethod]
    public void Equals_Success() {
      Assert.AreEqual(Apple.Id.Archetype, Apple.Id.Archetype);
      Assert.AreEqual(Apple.Id.Archetype, Archetypes<Apple>._);
    }

    [TestMethod]
    public void EqualityOpperator_Failure () {
      Assert.IsFalse(Apple.Id.Archetype == Sword.Id.Archetype);
    }

    [TestMethod]
    public void InEqualityOpperator_Success () {
      Assert.IsTrue(Apple.Id.Archetype != Sword.Id.Archetype);
    }
    

    [TestMethod]
    public void NotEquals_Success() {
      Assert.AreNotEqual(Apple.Id.Archetype, Sword.Id.Archetype);
    }

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
        Assert.AreEqual("Meep.Tech.Data.Examples.Item.Apple", Apple.Id.Key);
      }

      /// <summary>
      /// Get a static id
      /// </summary>
      [TestMethod]
      public void StaticIdGetFromString_Success() {
        Assert.AreEqual("Meep.Tech.Data.Examples.Item.Apple", Archetypes.Id["Meep.Tech.Data.Examples.Item.Apple"].Key);
      }
    }

    [TestClass]
    public class Loading {

      [AssemblyInitialize]
      public static void Initialize(TestContext _) {
        Loader.Settings settings = new Loader.Settings() {
          UniverseName = "Meep.Tech.ECSBAM.Tests"
        };

        new Loader(settings)
          .Initialize();
      }
    }

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
          "Meep.Tech.Data.Examples.Item.Apple"
        );
        Assert.AreEqual(
          Sword.Id.Key,
          "Meep.Tech.Data.Examples.Item.Weapon.Sword"
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

    [TestClass]
    public class Collection {

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
            Item.Types.Get("Meep.Tech.Data.Examples.Item.Apple")
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

      /// <summary>
      /// Ways to get/access Archetype.Collections
      /// </summary>
      [TestClass]
      public class Get {

        // TODO: test child archetypes and models that have a new type of collection.

        [TestMethod]
        public void StaticFromChildArchetype_Exists() {
          Assert.IsNotNull(Archetypes.GetCollectionFor(Archetypes<Apple>._));
        }

        [TestMethod]
        public void StaticCollectionFromBaseArchetype_Exists() {
          Assert.IsNotNull(Archetypes<Apple>.Collection);
        }

        [TestMethod]
        public void StaticCollectionFromBaseArchetype_Equals() {
          Assert.IsNotNull(Archetypes<Apple>.Collection);
          Assert.IsNotNull(Archetypes<Sword>.Collection);
        }

        [TestMethod]
        public void StaticCollectionFromBaseArchetype_NotEquals() {
          Assert.IsNotNull(Archetypes<Apple>.Collection);
          Assert.IsNotNull(Archetypes<ModularFluxCapacitor.Type>.Collection);
        }

        [TestMethod]
        public void StaticCollectionFromChildArchetype_Exists() {
          Assert.IsNotNull(Archetypes<Apple>.Collection);
        }

        [TestMethod]
        public void DefaultTypesCollectionForBaModel_Exists() {
          Assert.IsNotNull(Item.Types);
        }

        [TestMethod]
        public void DefaultCollectionForBaseArchetype_Exists() {
          Assert.IsNotNull(Item.Type.DefaultCollection);
        }

        [TestMethod]
        public void DefaultTypesCollectionForChildArchetype_Exists() {
          Assert.IsNotNull(Apple.DefaultCollection);
        }
      }

      [TestMethod]
      public void DefaultTypesCollectionForBaseModel_TypeIsCorrect() {
        Assert.IsInstanceOfType(
          Item.Types,
          typeof(Archetype<Item, Item.Type>.Collection)
        );
      }

      [TestMethod]
      public void StaticCollectionForBaseModel_TypeIsCorrect() {
        Assert.IsInstanceOfType(
          Archetypes.DefaultUniverse.Archetypes.GetCollectionFor(typeof(Item.Type)),
          typeof(Archetype<Item, Item.Type>.Collection)
        );
      }

      [TestMethod]
      public void StaticCollectionForChildModel_TypeIsCorrect() {
        Assert.IsInstanceOfType(
          Archetypes.GetCollectionFor(Archetypes<Apple>._),
          typeof(Archetype<Item, Item.Type>.Collection)
        );
      }

      [TestMethod]
      public void DefaultCollectionForBaseArchetype_TypeIsCorrect() {
        Assert.IsInstanceOfType(
          Item.Type.DefaultCollection,
          typeof(Archetype<Item, Item.Type>.Collection)
        );
      }

      [TestMethod]
      public void DefaultCollectionForChildArchetype_TypeIsCorrect() {
        Assert.IsInstanceOfType(
          Apple.DefaultCollection,
          typeof(Archetype<Item, Item.Type>.Collection)
        );
      }
    }

    [TestClass]
    public class Make {

      [TestMethod]
      public void MakeDefaultBasic_Success() {
        Item made = Archetypes<Apple>._.Make();
        Assert.AreEqual(
          made.Archetype.Id,
          Apple.Id
        );
      }
    }

    [TestMethod]
    public void MakeDefaultBasic_IsCorrectModelType() {
      Item made = Archetypes<Apple>.Instance.Make();
      Assert.IsInstanceOfType(
        made,
        typeof(Item)
      );
    }

    [TestMethod]
    public void MakeDefaultChild_Success() {
      Item made = Archetypes<Sword>.Instance.Make();
      Assert.AreEqual(
        made.Archetype.Id,
        Sword.Id
      );
    }

    [TestMethod]
    public void MakeDefaultChild_DefaultValueWasSetOnModel() {
      Item made = Archetypes<Sword>.Instance.Make();
      Assert.AreEqual(
        (made as Weapon)?.DamagePerHit,
        Archetypes<Sword>._.DefaultDamagePerHit
      );
    }

    [TestMethod]
    public void MakeDefaultChild2_DefaultValueWasSetOnModel() {
      Item made = Archetypes<HealingPotion>.Instance.Make();
      Assert.AreEqual(
        (made as Potion)?.remainingUses,
        Archetypes<HealingPotion>._.MaxUses
      );
    }

    [TestMethod]
    public void MakeDefaultChild_IsCorrectModelType() {
      Item made = Archetypes<Sword>.Instance.Make();
      Assert.IsInstanceOfType(
        made,
        typeof(Weapon)
      );
    }

    [TestMethod]
    public void MakeChildWithParameter_Success() {
      Item made = Archetypes<Sword>.Instance.Make((Weapon.Param.DamagerPerHit, 15));
      Assert.AreEqual(
        made.Archetype.Id,
        Sword.Id
      );
    }

    [TestMethod]
    public void MakeChildWithBuilder_Success() {
      Weapon made = Archetypes<Sword>.Instance.Make<Weapon>(builder => {
        builder.SetParam(Weapon.Param.DamagerPerHit, 15);
      });
      Assert.AreEqual(
        made.Archetype.Id,
        Sword.Id
      );
    }

    [TestMethod]
    public void MakeChildWithBuilder_ParameterWasSetOnModel() {
      Item made = Archetypes<Sword>.Instance.Make((Weapon.Param.DamagerPerHit, 14));
      Assert.AreEqual(
        (made as Weapon)?.DamagePerHit,
        14
      );
    }

    [TestMethod]
    public void MakeChildWithFullBuilder_ParameterWasSetOnModel() {
      IBuilder<Item> builder = Archetypes<Sword>.Instance.MakeDefaultBuilder();
      builder.SetParam(Weapon.Param.DamagerPerHit, 30);
      Item made = Archetypes<Sword>.Instance.Make(builder);
      Assert.AreEqual(
        (made as Weapon)?.DamagePerHit,
        30
      );
    }

    [TestMethod]
    public void MakeChildWithStringParam_ParameterWasSetOnModel() {
      Item made = Archetypes<HealingPotion>.Instance.Make((nameof(Potion.remainingUses), 14));
      Assert.AreEqual(
        (made as Potion)?.remainingUses,
        14
      );
    }

    [TestMethod]
    public void MakeChildWithBuilderWithStringParam_ParameterWasSetOnModel() {
      Item made = Archetypes<HealingPotion>.Instance.Make<Potion>(builder => {
        builder.SetParam(nameof(Potion.remainingUses), 5);
      });

      Assert.AreEqual(
        (made as Potion)?.remainingUses,
        5
      );
    }

    [TestMethod]
    public void MakeChildWithParameter_ParameterWasSetOnModel() {
      Weapon? made = Archetypes<Sword>.Instance.Make(builder => {
        builder.SetParam(Weapon.Param.DamagerPerHit, 15);
      }) as Weapon;
      Assert.AreEqual(
        made?.DamagePerHit,
        15
      );
    }
  }
}
