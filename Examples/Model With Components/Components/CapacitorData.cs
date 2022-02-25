namespace Meep.Tech.Data.Examples {
  public struct CapacitorData 
    : IModel.IComponent<CapacitorData>,
      IComponent.IUseDefaultUniverse
  {

    public int Value;

    internal CapacitorData(int value) : this() {
      Value = value;
    }
  }
}
