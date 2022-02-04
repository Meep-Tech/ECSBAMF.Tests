using System;
using System.Collections.Generic;

namespace Meep.Tech.Data.Examples {

  /// <summary>
  /// This example creates a new model, and new branching abstract archetype to be used with the model, extending item and item.type
  /// This example shoes how to override the BuilderConstructor to change a single value.
  /// Weapon is a simpler example of the same type of thing.
  /// This example uses a string-based param, as opposed to a constant based param, like in weapon.
  /// </summary>
  public class Potion : Item {

    public int remainingUses {
      get;
      internal set;
    }

    protected Potion() 
      : base() {}

    public new abstract class Type : Item.Type {

      public virtual int MaxUses {
        get;
      } = 10;

      public new class Id : Item.Type.Identity {

        public Id(string name)
          : base(name, $"Potion.") { }
      }

      protected override Func<Archetype, Dictionary<string, object>, Universe, IBuilder<Item>> BuilderConstructor
        => (type, @params, _) => {
          // Get the original builder from the base:
          IModel<Item>.Builder baseBuilder 
            = base.BuilderConstructor(type, @params, null) as IModel<Item>.Builder;

          // capture the original config logic
          Func<IModel<Item>.Builder, Item, Item> baseConfigure 
            = baseBuilder.ConfigureModel;

          // update what you want to
          baseBuilder!.InitializeModel = @builder => new Potion();
          baseBuilder.ConfigureModel = (builder, model) => {
            // you can invoke the bases in these too:
            baseConfigure?.Invoke(builder, model);

            // set the new value and return:
            (model as Potion)!.remainingUses
              = builder.GetParam<int>(nameof(remainingUses), MaxUses);

            return model;
          };

          return baseBuilder;
        };

      protected Type(Potion.Type.Id typeId)
        : base(typeId) { }
    }
  }
}