using System;

namespace Meep.Tech.Data.Examples {

  /// <summary>
  /// This example provides an extension to Item and Item.Type with a new model and new branching base archetype.
  /// This example shows how to potentially provide an updateable value with a default for each child archetype.
  /// </summary>
  public class Weapon : Item {
    protected Weapon() {}

    public int DamagePerHit {
      get;
      private set;
    }

    /// This can extend any param class really. 
    /// ... It can be as percise as you want. 
    /// ... Using the specific generic Model<>.Builder gives it an .All static collection by default.
    public class Param : IModel<Item>.Builder.Param {

      public static Param DamagerPerHit {
        get;
      } = new Param("Damage Per Hit", typeof(int));

      /// You can make the constructor public if you want anyone to be able to add params,
      /// or you can restrict it to protected, to force someone to have to extend Weapon.Params as well.

      protected Param(string name, System.Type valueType) 
        : base(name, valueType) {}
    }

    public new abstract class Type : Item.Type {
      public new class Id : Item.Type.Identity {

        public Id(string name)
          : base(name, $"Weapon") { }
      }

      public int DefaultDamagePerHit {
        get;
      }

      protected Type(Weapon.Type.Id id, int defaultDamagePerHit) 
        : base(id) {
        DefaultDamagePerHit = defaultDamagePerHit;
      }

      public override Func<IBuilder<Item>, Item> ModelConstructor
        => builder => new Weapon();

      protected override Item ConfigureModel(IBuilder<Item> builder, Item model) {
        model = base.ConfigureModel(builder, model);
        (model as Weapon)!.DamagePerHit = builder.GetViaParam(Param.DamagerPerHit, DefaultDamagePerHit);
        return model;
      }
    }
  }
}
