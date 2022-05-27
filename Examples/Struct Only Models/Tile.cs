namespace Meep.Tech.Data.Tests.Examples.Struct_Only_Models {

  /// <summary>
  /// IDK why, but if you want everything to be a struct, that works too
  /// </summary>
  /// TODO: EF doesn't support structs fek... maybe find a different way to serialize these into a dictionary conainer class?
  public struct Tile : Model<Tile, Tile.Type>.IFromInterface,
    // If you want IReadableComponentStorage though it requires a dictionary,
    // ... but you can start with it uninitialized if you want
    IModel.IUseDefaultUniverse
  {

    /// <summary>
    /// All tile types
    /// </summary>
    public static Type.Collection Types {
      get;
    } = new Type.Collection();

    public Type Archetype {
      get;
      private set;
    } Type Model<Tile, Type>.IFromInterface.Archetype {
      get => Archetype;
      set => Archetype = value; 
    }

    Tile(IBuilder builder) 
      : this() {}

    /// <summary>
    /// A type of tile
    /// </summary>
    public abstract class Type : Archetype<Tile, Type>.WithAllDefaultModelBuilders {

      protected Type(
        Data.Archetype.Identity id
      ) : base(id, Types) {}
    }
  }
}
