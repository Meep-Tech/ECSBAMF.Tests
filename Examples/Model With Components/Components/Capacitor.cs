namespace Meep.Tech.Data.Examples {
  public partial struct Capacitor 
    : Archetype.IComponent<Capacitor>, 
      Archetype.ILinkedComponent<CapacitorData>, 
      IComponent.IUseDefaultUniverse
  {

    /// <summary>
    /// You can use this syntax to modify the builder info for a component, while using a new builder factory if you want.
    /// </summary>
    static Capacitor() {
      Components<Capacitor>.BuilderFactory = new BuilderFactory {
        // builder constructor is directly get/settable for factories as long as it's before archetype loading is done for this universe
        BuilderConstructor
          // here we use a light builder instead of a full one:
          = (type, @params, universe) => new IComponent<Capacitor>.LiteBuilder(type, @params, universe) {
            // configure the model
            ConfigureModel = (builder, model) => {
              model.DefaultCapacityValue
                = builder.GetParam<int>(nameof(DefaultCapacityValue));

              return model;
            }
          }
      };
    }

    public int DefaultCapacityValue {
      get;
      private set;
    }

    /// <summary>
    /// We set this the simplest way we can;
    /// </summary>
    CapacitorData Archetype.ILinkedComponent<CapacitorData>.BuildDefaultModelComponent(
      IModel.Builder parentModelBuilder,
      Universe universe
    ) => new CapacitorData (
      parentModelBuilder.GetParam<int>("capacitorValue")
    );
  }
}
