using System;
using Meep.Tech.Data.Configuration;

namespace Meep.Tech.Data.Examples {

  public partial class SafeModularDevice : Device {

    /// <summary>
    /// Branch by default activates the containing type's parameterless ctor instead of the one for the current base model type.
    /// </summary>
    [Branch]
    public new class Type : Device.Type {

      protected Type()
        : base(new Identity("Safe Modular")) { }
    }
  }
}
