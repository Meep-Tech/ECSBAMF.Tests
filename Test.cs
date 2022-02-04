using System.Collections.Generic;

namespace Meep.Tech.Data.Examples {

  /*public class Test : Model<Test, Test.Type> {
    public class Type : Archetype<Test, Type> {

      public new static Identity Id {
        get;
      }

      public Type() : base(Id) {

      }
    }

    public void main(Test item) {
      Test.Type archetypeSingleton;

      archetypeSingleton = Test.Types.Get(Test.Type.Id);
      archetypeSingleton = Test.Types.Get("Tezt.Type");
      archetypeSingleton = Test.Types.ById[Test.Type.Id];
      archetypeSingleton = Test.Types.Get<Test.Type>();
      archetypeSingleton = Test.Types.Get(typeof(Test.Type));
      archetypeSingleton = Test.Type.Collection.Get(Test.Type.Id);
      archetypeSingleton = Archetypes<Test.Type>.Archetype;
      archetypeSingleton = Archetypes<Test.Type>.Instance;
      archetypeSingleton = Archetypes<Test.Type>._;
      archetypeSingleton = Archetypes.All.Get(Test.Type.Id) as Test.Type;
      archetypeSingleton = Archetypes.All.ById[Test.Type.Id] as Test.Type;
      archetypeSingleton = typeof(Test.Type).AsArchetype<Test.Type>();
      archetypeSingleton = typeof(Test.Type).AsArchetype() as Test.Type;
      archetypeSingleton = Test.Type.Id.Archetype as Test.Type;
      archetypeSingleton = Archetypes<Test.Type>.Collection.Get<Test.Type>();

      archetypeSingleton.Make(new IModel<Test>.Builder(archetypeSingleton) {
        {"color", "red" }
      });
      Test.Make(archetypeSingleton, (IModel.Builder builder) => {
        builder.SetParam("color", "red");
      });
      Test.Types.Get<Test.Type>().Make((IBuilder<Test> builder) => {
        builder.SetParam("color", "red");
        return builder;
      });
      Archetypes<Test.Type>._.Make((IBuilder builder) => builder);
      Archetypes<Test.Type>._.Make((IModel<Test>.Builder builder) => builder["test"] = "1212");
      Archetypes<Test.Type>._.Make(("color", "red"));
      Archetypes<Test.Type>.w.Make(new KeyValuePair<string, object>("color", "red"));
      Archetypes<Test.Type>._.Make(
        ("color", "red"),
        ("count", 3)
      );
      archetypeSingleton.Make(builder => {
        builder.SetParam("color", "red");
        builder.SetParam("count", 3);
      });

      var color = Components<Color>.BuilderFactory.Make(("color", "red"));
      var color1 = Components<Color>.BuilderFactory.Make(new KeyValuePair<string, object>("color", "red"));
      var color2 = Components<Color>.BuilderFactory.Make(new KeyValuePair<IModel.Builder.Param, object>(Color.ColorParam, "red"));
      var color3 = Components<Color>.BuilderFactory.Make((Color.ColorParam, "red"));

      var color4 = Components<Color>.BuilderFactory.Make((IBuilder<Color> builder) => {
        builder.SetParam(Color.ColorParam, "red");

        return builder;
      });

      var color5 = Components<Color>.BuilderFactory.Make((IBuilder<Color> builder) => {
        builder.SetParam("color", "red");

        return builder;
      });

      var builder = new IComponent<Color>.LiteBuilder();
      builder.SetParam(Color.ColorParam, "blue");
      var color6 = Components<Color>.BuilderFactory.Make(builder);
    }
  }

  public struct Color : IModel.IComponent<Color> {

    public static IModel.Builder.Param ColorParam {
      get;
    } = new IModel.Builder.Param("color", typeof(string));

    public string color {
      get;
      private set;
    }

    // TODO: test to see if it can get the builder param with inhertance:
    Color(IComponent<Color>.LiteBuilder builder) {
      color = builder.GetParam<string>("color");
    }

    // OR just:
    Color(IBuilder builder) {
      color = builder.GetParam<string>("color");
    }

    // You could do this instead of the default ctor if you want:
    static Color() {
      Models<Color>.BuilderFactory.BuilderConstructor =
        (type, @params) => {
          var builder = new IModel<Color>.Builder(type, @params) {
            InitializeModel = builder => new Color(),
            ConfigureModel = (builder, color) => {
              color.color = builder.GetParam<string>("color");
              return color;
            },
          };

          return builder;
        };
    }
  }*/
}
