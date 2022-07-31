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
        (made as Potion)?.RemainingUses,
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
      Item made = Archetypes<Sword>.Instance.Make((Weapon.Param.DamagerPerHit.ExternalId.ToString(), 15));
      Assert.AreEqual(
        made.Archetype.Id,
        Sword.Id
      );
    }

    [TestMethod]
    public void MakeChildWithBuilder_Success() {
      Weapon made = Archetypes<Sword>.Instance.Make<Weapon>(builder =>
        builder.Append(Weapon.Param.DamagerPerHit, 15)
      );
      Assert.AreEqual(
        made.Archetype.Id,
        Sword.Id
      );
    }

    [TestMethod]
    public void MakeChildWithBuilder_ParameterWasSetOnModel() {
      Item made = Archetypes<Sword>.Instance.Make((Weapon.Param.DamagerPerHit.Key, 14));
      Assert.AreEqual(
        (made as Weapon)?.DamagePerHit,
        14
      );
    }

    [TestMethod]
    public void MakeBuilderOfRightType_IBuilder() {
      var builder = Archetypes<Sword>
        .Instance
        .Builder();

      Assert.IsInstanceOfType(builder, typeof(IBuilder<Item>));
    }

    [TestMethod]
    public void MakeBuilderOfRightType_BaseModel() {
      var builder = Archetypes<Sword>
        .Instance
        .Builder();

      Assert.IsInstanceOfType(builder, typeof(IModel<Item>.Builder));
    }

    [TestMethod]
    public void MakeBuilderOfRightType_IModelBuilder() {
      var builder = Archetypes<Sword>
        .Instance
        .Builder();

      Assert.IsInstanceOfType(builder, typeof(IModel.IBuilder));
    }

    [TestMethod]
    public void MakeWithBuildSyntax_ParameterWasSetOnModel() {
      var made = Archetypes<Sword>
        .Instance
        .Builder()
        .Append(Weapon.Param.DamagerPerHit, 30)
        .Make();

      Assert.AreEqual(
        (made as Weapon)?.DamagePerHit,
        30
      );
    }

    [TestMethod]
    public void MakeChildWithFullBuilder_ParameterWasSetOnModel() {
      IBuilder builder = Archetypes<Sword>.Instance.Builder()
        .Append(Weapon.Param.DamagerPerHit, 30);

      Item made = Archetypes<Sword>.Instance.Make(builder);
      Assert.AreEqual(
        (made as Weapon)?.DamagePerHit,
        30
      );
    }

    [TestMethod]
    public void MakeChildWithStringParam_ParameterWasSetOnModel() {
      Item made = Archetypes<HealingPotion>.Instance.Make((nameof(Potion.RemainingUses), 14));
      Assert.AreEqual(
        (made as Potion)?.RemainingUses,
        14
      );
    }

    [TestMethod]
    public void MakeChildWithBuilderWithStringParam_ParameterWasSetOnModel() {
      Item made = Archetypes<HealingPotion>.Instance.Make<Potion>(builder => 
        builder.Append(nameof(Potion.RemainingUses), 5)
      );

      Assert.AreEqual(
        (made as Potion)?.RemainingUses,
        5
      );
    }

    [TestMethod]
    public void MakeChildWithParameter_ParameterWasSetOnModel() {
      Weapon? made = Archetypes<Sword>.Instance.Make(builder =>
        builder.Append(Weapon.Param.DamagerPerHit, 15)
      ) as Weapon;
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
