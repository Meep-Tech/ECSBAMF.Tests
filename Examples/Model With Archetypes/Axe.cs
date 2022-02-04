namespace Meep.Tech.Data.Examples {
  public class Axe : Weapon.Type {

    public new static Weapon.Type.Id Id {
      get;
    } = new Id("Axe");

    Axe() 
      : base(Id, 5) 
        {}
  }
}
