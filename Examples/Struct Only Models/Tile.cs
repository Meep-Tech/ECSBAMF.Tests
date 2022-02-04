using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meep.Tech.Data.Tests.Examples.Struct_Only_Models {

  /// <summary>
  /// IDK why, but if you want everything to be a struct, that works too
  /// </summary>
  /// TODO: EF doesn't support structs fek... maybe find a different way to serialize these into a dictionary conainer class?
  /*[Table("Tile")]
  public struct Tile : IModel<Tile, Tile.Type>,
    // If you want IReadableComponentStorage though it requires a dictionary,
    // ... but you can start with it uninitialized if you want
    IReadableComponentStorage<Tile> {

    /// <summary>
    /// All tile types
    /// </summary>
    public static Type.ArchetypeCollection Types {
      get;
    } = new Type.ArchetypeCollection();

    /// <summary>
    /// A type of tile
    /// </summary>
    public abstract class Type : Archetype<Tile, Type> {

      protected Type(
        Data.Archetype.Identity id
      ) : base(id, Types) {}
    }

    public Universe Universe {
      get;
    }

    #region Component Storage

    Dictionary<string, IComponent> _componentsByBuilderKey {
      get => __componentsByBuilderKey ??= new Dictionary<string, IComponent>();
      set => __componentsByBuilderKey = value;
    }
    Dictionary<string, IComponent>? __componentsByBuilderKey;
    Dictionary<string, IComponent> IReadableComponentStorage<Tile>._componentsByBuilderKey
      => _componentsByBuilderKey;

    public override bool Equals(object obj) {
      return IReadableComponentStorage.Equals(this, obj as IReadableComponentStorage)
        && base.Equals(obj);
    }

    #endregion

    Tile(IBuilder builder) {
      Universe = builder.Type.Id.Universe;
      __componentsByBuilderKey = null;
    }
  }*/
}
