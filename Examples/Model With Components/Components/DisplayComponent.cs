namespace Meep.Tech.Data.Examples {
  public class DisplayComponent : IModel.IComponent<DisplayComponent>, IComponent.IUseDefaultUniverse, IComponent.IDoOnAdd {

    public bool WasInitialized {
      get;
      private set;
    } = false;


    void IComponent.IDoOnAdd.ExecuteWhenAdded(IReadableComponentStorage parent) 
      => WasInitialized = true;
  }
}
