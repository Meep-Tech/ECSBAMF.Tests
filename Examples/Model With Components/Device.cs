using System.Linq;
using System.Text;
using Meep.Tech.Data;

namespace Meep.Tech.Data.Examples {

  /// <summary>
  /// A device with lots of moving parts
  /// </summary>
  public partial class Device 
    : Model<Device, Device.Type>.WithComponents 
  {

    protected Device()
      : base() { }
  }
}
