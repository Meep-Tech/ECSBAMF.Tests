using Meep.Tech.Data.Examples.AutoBuilder;
using Meep.Tech.Data.Examples.ModelWithArchetypes;
using Meep.Tech.Data.Examples.ModelWithComponents;
using Meep.Tech.Data.Examples.StructOnlyModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using static Meep.Tech.Data.Examples.AutoBuilder.Animal;

namespace Meep.Tech.Data.Tests {

	[TestClass]
  public partial class Model {

    [TestMethod]
    public void ConstructionWithoutNeededParameter_Failure() {
      Assert.ThrowsException<IModel.Builder.Param.MissingException>(() => {
        Device.Types.Get<ModularFluxCapacitor.Type>()
          .Make();
      });
    }
  }
}
