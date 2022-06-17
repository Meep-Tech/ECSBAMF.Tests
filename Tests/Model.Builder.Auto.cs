using Meep.Tech.Data.Examples.AutoBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Meep.Tech.Data.Examples.AutoBuilder.Animal;

namespace Meep.Tech.Data.Tests {
  public partial class Model {
    public partial class Builder {
      [TestClass]
      public class Auto {

        [TestMethod]
        public void MakeWithAutoBuilderRequiredField_Snake_Name_Success() {
          const string snakeName = "Snakey";
          var snake = Animal.Types.Get<Snake>()
            .Make<Animal>((nameof(Animal.Name), snakeName));

          Assert.AreEqual(snakeName, snake.Name);
        }

        [TestMethod]
        public void MakeWithAutoBuilderOverrideDefaultField_Snake_NumberOfLegs_Success() {
          const string snakeName = "Snakey";
          var snake = Animal.Types.Get<Snake>()
            .Make((nameof(Animal.Name), snakeName), (nameof(Animal.NumberOfLegs), 1));

          Assert.AreEqual(1, snake.NumberOfLegs);
        }

        [TestMethod]
        public void MakeWithAutoBuilderOverrideDefaultField_Snake_CanClimb_Success() {
          const string snakeName = "Snakey";
          var snake = Animal.Types.Get<Snake>()
            .Make((nameof(Animal.Name), snakeName), (nameof(Animal.IsAClimber), false));

          Assert.AreEqual(false, snake.IsAClimber);
        }

        [TestMethod]
        public void MakeWithAutoBuilderDefaultField_Snake_CanClimb_Success() {
          const string snakeName = "Snakey";
          var snake = Animal.Types.Get<Snake>()
            .Make((nameof(Animal.Name), snakeName));

          Assert.AreEqual(snake.Archetype.CanClimb, snake.IsAClimber);
        }

        [TestMethod]
        public void MakeWithAutoBuilderOverrideDefaultField_Snake_NumberOfLegs_Failure() {
          const string snakeName = "Snakey";
          Assert.ThrowsException<AutoBuildAttribute.Exception>(() =>
            Animal.Types.Get<Snake>()
              .Make((nameof(Animal.Name), snakeName), (nameof(Animal.NumberOfLegs), -1)));
        }

        [TestMethod]
        public void MakeWithAutoBuilderDefaultField_Snake_NumberOfLegs_Success() {
          const string snakeName = "Snakey";
          var snake = Animal.Types.Get<Snake>()
            .Make<Animal>((nameof(Animal.Name), snakeName));

          Assert.AreEqual(snake.Archetype.DefaultNumberOfLegs, snake.NumberOfLegs);
        }

        [TestMethod]
        public void MakeWithAutoBuilderRequiredFieldMissing_Snake_Name_Failure() {
          Assert.ThrowsException<AutoBuildAttribute.Exception>(() => 
            Animal.Types.Get<Snake>()
              .Make<Animal>());
        }

        [TestMethod]
        public void MakeWithAutoBuilderVirtualDefaultField_Cat_Name_Success() {
          var cat = Animal.Types.Get<Cat.Type>()
            .Make<Animal>();

          Assert.AreEqual("Kitty", cat.Name);
        }

        [TestMethod]
        public void MakeWithAutoBuilderVirtualOverrideDefaultField_Cat_Name_Success() {
          const string catName = "Mowster";
          var cat = Animal.Types.Get<Cat.Type>()
            .Make<Animal>((nameof(Animal.Name), catName));

          Assert.AreEqual(catName, cat.Name);
        }

        [TestMethod]
        public void MakeWithAutoBuilderAttributeDefaultField_Dog_Name_Success() {
          var dog = Animal.Types.Get<Dog.Type>()
            .Make<Animal>();

          Assert.AreEqual("Friend", dog.Name);
        }

        [TestMethod]
        public void MakeWithAutoBuilderAttributeOverrideDefaultField_Dog_Name_Success() {
          const string dogName = "goofy";
          var dog = Animal.Types.Get<Dog.Type>()
            .Make<Animal>((nameof(Animal.Name), dogName));

          Assert.AreEqual(dogName, dog.Name);
        }
      }
    }
  }
}
