using Meep.Tech.Collections.Generic;
using System;
using System.Collections.Generic;

namespace Meep.Tech.Data.Examples {

  public partial class FluxCapacitor {

    /// <summary>
    /// Device base archetype
    /// </summary>
    public new class Type : Device.Type {

      protected override Dictionary<string, object> DefaultTestParams 
        => new Dictionary<string, object> {
          {nameof(FluxCapacitor.FluxLevel), 0 }
        };

      public override HashSet<IComponent> InitialComponents
        => base.InitialComponents.Append(
          Components<Capacitor>.BuilderFactory.Make()
        );

      public override Func<IBuilder<Device>, Device> ModelConstructor
        => builder => new FluxCapacitor();

      protected override Device ConfigureModel(IBuilder<Device> builder, Device model) {
        model = base.ConfigureModel(builder, model);

        (model as FluxCapacitor).FluxLevel 
          = builder.GetAndValidateParamAs<int>(nameof(FluxCapacitor.FluxLevel));

        return model;
      }

      protected Type(Identity id = null)
        : base(id ?? new FluxCapacitor.Type.Identity("Flux Capacitor")) { }
    }
  }
}
