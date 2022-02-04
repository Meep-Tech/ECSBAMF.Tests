using System;

namespace Meep.Tech.Data.Examples {

  public partial class DangerousModularDevice {

    public new class Type : Device.Type {

      public override Func<IBuilder<Device>, Device> ModelConstructor
        => builder => new DangerousModularDevice();

      protected Type()
        : base(new Identity("Dangerous Modular")) { }
    }
  }
}
