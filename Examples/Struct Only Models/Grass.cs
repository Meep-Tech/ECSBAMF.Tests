using Meep.Tech.Data;

namespace Meep.Tech.Data.Tests.Examples.Struct_Only_Models {

  /// <summary>
  /// Grass tile
  /// </summary>
  public class Grass : Tile.Type {

    public new static Identity Id {
      get;
    } = new Identity("Grass");

    Grass() 
      : base(Id) {}
  }
}
