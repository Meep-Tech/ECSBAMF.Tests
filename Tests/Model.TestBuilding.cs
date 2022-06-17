using Meep.Tech.Data.Examples.Examples.AutoTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Meep.Tech.Data.Tests {
	public partial class Model {

		[TestClass]
		public class TestBuilding {
			[TestMethod]
			public void SetTestValue_Int_Success() {
        Candy candy =
          Meep.Tech.Data.Configuration.Loader.GetOrBuildTestModel(
              Candy.Types.Get<ChocolateBar>(),
              typeof(Candy)
          ) as Candy;
        Assert.AreEqual(4, candy.QuantityPerPackage);
      }

      [TestMethod]
      public void SetTestValue_Int_WithName_Success() {
        Gumdrops candy =
          Meep.Tech.Data.Configuration.Loader.GetOrBuildTestModel(
              Candy.Types.Get<Gumdrops.Type>(),
              typeof(Candy)
          ) as Gumdrops;
        Assert.AreEqual(true, candy.IsSugarCoated);
      }

      [TestMethod]
      public void SetTestValue_EmptyEnumeration_Success() {
        Candy candy =
          Meep.Tech.Data.Configuration.Loader.GetOrBuildTestModel(
              Candy.Types.Get<ChocolateBar>(),
              typeof(Candy)
          ) as Candy;
        Assert.AreEqual(Enumerable.Empty<string>(), candy.Flavors);
      }

      [TestMethod]
      public void SetTestValue_NewDictionary_Success() {
        Candy candy =
          Meep.Tech.Data.Configuration.Loader.GetOrBuildTestModel(
              Candy.Types.Get<ChocolateBar>(),
              typeof(Candy)
          ) as Candy;
        Assert.AreEqual(expected: new Dictionary<string, object>().GetType(), candy.Qualities.GetType());
        Assert.AreEqual(expected: new Dictionary<string, object>().Count, candy.Qualities.Count);
      }

      [TestMethod]
      public void SetTestValue_FromMemberField_Success() {
        Candy candy =
          Meep.Tech.Data.Configuration.Loader.GetOrBuildTestModel(
              Candy.Types.Get<ChocolateBar>(),
              typeof(Candy)
          ) as Candy;
        Assert.AreEqual(4, candy.Cost);
      }

      [TestMethod]
      public void SetTestValue_FromMemberProperty_Success() {
        Candy candy =
          Meep.Tech.Data.Configuration.Loader.GetOrBuildTestModel(
              Candy.Types.Get<ChocolateBar>(),
              typeof(Candy)
          ) as Candy;
        Assert.AreEqual(100, candy.InternalId);
      }

      [TestMethod]
      public void SetTestValue_FromMemberMethod_Success() {
        Candy candy =
          Meep.Tech.Data.Configuration.Loader.GetOrBuildTestModel(
              Candy.Types.Get<ChocolateBar>(),
              typeof(Candy)
          ) as Candy;
        Assert.AreEqual(new System.DateTime(1, 1, 1), candy.DateManufactured);
      }

      [TestMethod]
      public void SetTestValue_FromMemberMethodWithArgs_Success() {
        Candy candy =
          Meep.Tech.Data.Configuration.Loader.GetOrBuildTestModel(
              Candy.Types.Get<ChocolateBar>(),
              typeof(Candy)
          ) as Candy;
        Assert.AreEqual("m-ChocolateBar", candy.ManufacturerName);
      }

      [TestMethod]
      public void SetTestValue_string_FromBranchedModelType_Base_Success() {
        Candy candy =
          Meep.Tech.Data.Configuration.Loader.GetOrBuildTestModel(
              Candy.Types.Get<ChocolateBar>(),
              typeof(Candy)
          ) as Candy;
        Assert.AreEqual("Test", candy.Name);
      }

      [TestMethod]
      public void SetTestValue_string_FromBranchedModelTypeWithBaseModelType_Success() {
        Candy candy =
          Meep.Tech.Data.Configuration.Loader.GetOrBuildTestModel(
              Candy.Types.Get<Gumdrops.Type>(),
              typeof(Candy)
          ) as Candy;
        Assert.AreEqual("Gum-Test", candy.Name);
      }

      [TestMethod]
      public void SetTestValue_string_FromUnBranchedNewModelTypeWithBaseModelType_Success() {
        Jellybean candy =
          Meep.Tech.Data.Configuration.Loader.GetOrBuildTestModel(
              Candy.Types.Get<JellybeanType>(),
              typeof(Candy)
          ) as Jellybean;
        Assert.AreEqual(true, candy.IsShiny);
      }

      [TestMethod]
      public void SetTestValue_string_FromBranchedModelType_Success() {
        Candy candy =
          Meep.Tech.Data.Configuration.Loader.GetOrBuildTestModel(
              Candy.Types.Get<Gumdrops.Type>(),
              typeof(Gumdrops)
          ) as Candy;
        Assert.AreEqual("Gum-Test", candy.Name);
      }
    }
  }
}
