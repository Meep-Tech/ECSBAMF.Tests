using Meep.Tech.Data.Examples.AutoBuilder;
using Meep.Tech.Data.Examples.ModelWithArchetypes;
using Meep.Tech.Data.Examples.ModelWithComponents;
using Meep.Tech.Data.Examples.UnloadableArchetypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using static Meep.Tech.Data.Examples.AutoBuilder.Animal;

namespace Meep.Tech.Data.Tests {

  [TestClass]
  public partial class Archetype {

    [TestMethod]
    public void IsBaseFalse_Success() {
      Assert.IsFalse(Apple.Id.Archetype.IsBaseArchetype);
    }

    [TestMethod]
    public void BaseTypeEqualsAbstract_Success() {
      Assert.AreEqual(
        Apple.Id.Archetype.BaseArchetype,
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

    [TestMethod]
    public void MakeModelFromWrongBranch_Failure()
      => Assert.ThrowsException<InvalidCastException>(
        () => Animal.Types.Get<Animal.Snake>().Make<Dog>((nameof(Animal.Name), "Doggo?")));
  }
}
