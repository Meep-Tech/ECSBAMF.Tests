using static Meep.Tech.Data.Configuration.Loader.Settings;

namespace Meep.Tech.Data.Examples {

  /// <summary>
  /// The Base Model for all LoadableItems
  /// </summary>
  public class LoadableItem : Item {

    /// <summary>
    /// The Base Archetype for LoadableItems
    /// </summary>
    [Branch]
    public new abstract class Type : Item.Type {

      protected override bool AllowInitializationsAfterLoaderFinalization 
        => true;

      /// <summary>
      /// Used to make new Child Archetypes for LoadableItem.Type 
      /// </summary>
      /// <param name="id">The unique identity of the Child Archetype</param>
      protected Type(Identity id)
        : base(id) { }

      internal void Unload()
        => TryToUnload();
    }
  }
}
