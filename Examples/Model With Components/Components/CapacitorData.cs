namespace Meep.Tech.Data.Examples {
  public struct CapacitorData 
    : IModel.IComponent<CapacitorData>
  {

    Universe IComponent.Universe {
      get;
      set;
    }

    public int Value;

    internal CapacitorData(int value) : this() {
      Value = value;
    }
  }
}
