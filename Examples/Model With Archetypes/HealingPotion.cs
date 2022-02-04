namespace Meep.Tech.Data.Examples {
  public class HealingPotion : Potion.Type {

    public new static Potion.Type.Id Id {
      get;
    } = new Id("Healing");

    public override int MaxUses 
      => 5;

    HealingPotion() 
      : base(Id) {}
  }
}