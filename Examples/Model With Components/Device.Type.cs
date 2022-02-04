namespace Meep.Tech.Data.Examples {

  public partial class Device {
    /// <summary>
    /// Device base archetype
    /// </summary>
    public abstract class Type : Archetype<Device, Type> {

      protected Type(Identity id) 
        : base(id) {}
    }
  }
}
