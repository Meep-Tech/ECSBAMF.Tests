namespace Meep.Tech.Data.Examples {
  public class Apple : Item.Type {

    public new static Item.Type.Identity Id {
      get;
    } = new Archetype<Item, Item.Type>.Identity("Apple");

    Apple() : base(Id) {}
  }
}
