using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meep.Tech.Data.Examples {
  public class Item : Model<Item, Item.Type>, IUnique {

    protected Item() {}

    [field: Column(nameof(IUnique.Id))]
    string IUnique.Id {
      get;
      set;
    }

    public abstract class Type : Archetype<Item, Item.Type> {

      protected Type(Archetype.Identity id) 
        : base(id) {}
    }
  }
}
