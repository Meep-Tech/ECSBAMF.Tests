namespace Meep.Tech.Data.Examples {
  public partial struct Capacitor {
    public class BuilderFactory : IComponent<Capacitor>.BuilderFactory {

      internal BuilderFactory() 
        : base(new Identity("Capacitor Builder")) { }

      protected override Capacitor ConfigureModel(IBuilder<Capacitor> builder, Capacitor model) {
        model = base.ConfigureModel(builder, model);
        model.DefaultCapacityValue
          = builder.GetParam<int>(nameof(DefaultCapacityValue));

        return model;
      }
    }
  }
}
