namespace Meep.Tech.Data.Examples {
  public class FishTaco : LoadableItem.Type {

    public new static Identity Id {
      get;
    } = new Identity("Fish Taco");

    internal FishTaco() 
      : base(Id) {}
  }
}
