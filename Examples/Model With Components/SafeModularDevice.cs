namespace Meep.Tech.Data.Examples {
  public partial class SafeModularDevice : Device {

    protected SafeModularDevice() 
      : base() {}

    /// <summary>
    /// Allow this device to add any type of component made for any device.
    /// </summary>
    public void AddComponent(IModel.IComponent.IIsRestrictedTo<Device> component) {
      base.AddComponent(component);
    }
  }
}
