namespace Meep.Tech.Data.Examples {
  public partial class ModularFluxCapacitor : FluxCapacitor {
    protected ModularFluxCapacitor() {}

    /// <summary>
    /// Allow this device to add any type of component at all
    /// </summary>
    public new void AddComponent(IModel.IComponent component) {
      base.AddComponent(component);
    }

    /// <summary>
    /// Allow this device to add any type of component at all
    /// </summary>
    public new void AddNewComponent<TComponent>(params (string, object)[] @params)
      where TComponent : IModel.IComponent<TComponent> 
        => base.AddNewComponent<TComponent>(@params);
  }
}
