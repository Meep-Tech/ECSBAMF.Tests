using System.Collections.Generic;
using Meep.Tech.Data;

namespace Meep.Tech.Data.Tests.Examples.AutoBuilder {

  /// <summary>
  /// The Base Model for all Animals
  /// </summary>
  public class Animal : Model<Animal, Animal.Type>, IModel.IUseDefaultUniverse {

    [Configuration.AutoBuild(IsRequiredAsAParameter = true, NotNull = true)]
    public virtual string Name {
      get; protected set;
    }

    [Configuration.AutoBuild(ParameterName = "AgeInYears")]
    public int YearsOld {
      get; private set;
    }

    [Configuration.AutoBuild(ValueValidatorName = nameof(_numberOfLegsValidator))]
    public int NumberOfLegs {
      get; private set;
    }

    [Configuration.AutoBuild(DefaultArchetypePropertyName = nameof(Type.CanClimb))]
    public bool IsAClimber {
      get; private set;
    }

    [Configuration.AutoBuild(DefaultValueGetterDelegateName = nameof(_speedDefaultValueGetter), Order = 100)]
    public int Speed {
      get; private set;
    }

    #region XBam Configuration

    protected Animal(IBuilder<Animal> builder) {}

    #region Validation

     Configuration.AutoBuildAttribute.ValueValidator _numberOfLegsValidator
      = (object v, IBuilder b, IModel m, out string t, out System.Exception x)
        => {
          if((int)v >= 0) {
            t = null;
            x = null;

            return true;
          }
          else {
            t = "Animals can't have negative legs";
            x = null;

            return false;
          }
        };

    #endregion

    #region Defaults

    Configuration.AutoBuildAttribute.DefaultValueGetter _speedDefaultValueGetter
      = (IBuilder b, IModel m) => (m as Animal).NumberOfLegs * 4;

    #endregion

    #endregion

    /// <summary>
    /// The Base Archetype for Animals
    /// </summary>
    public abstract class Type : Archetype<Animal, Animal.Type>.WithDefaultParamBasedModelBuilders {

      /// <summary>
      /// The default number of legs.
      /// </summary>
      public abstract int DefaultNumberOfLegs {
        get;
      }

      /// <summary>
      /// The default age of this kind of animal.
      /// </summary>
      public virtual int DefaultAgeInYears {
        get;
      } = 1;

      /// <summary>
      /// If this type can climb
      /// </summary>
      public abstract bool CanClimb {
        get;
      }

      /// <summary>
      /// Used to make new Child Archetypes for Animal.Type 
      /// </summary>
      /// <param name="id">The unique identity of the Child Archetype</param>
      protected Type(Identity id)
        : base(id) { }
    }

    public class Snake : Type {
      protected override Dictionary<string, object> DefaultTestParams
        => new() {
          {nameof(Animal.Name), "Frank" }
        };

      public override int DefaultNumberOfLegs 
        => 0;
      public override bool CanClimb 
        => true;
      protected Snake() 
        : base(new(nameof(Snake))) {}
    }

    public class Dog : Animal {

      [Configuration.AutoBuild(NotNull = true)]
      public override string Name
        => base.Name ??= "Friend";

      protected Dog(IBuilder<Animal> builder) 
        : base(builder) {
        Name = "Friend";
      }

      [Configuration.Loader.Settings.Branch]
      public new class Type : Animal.Type {
        public override int DefaultNumberOfLegs
          => 4;
        public override bool CanClimb
          => false;
        protected Type()
          : base(new(nameof(Dog))) { }
      }
    }

    public class Cat : Animal {

      [Configuration.AutoBuild(NotNull = true)]
      public override string Name {
        get;
        protected set;
      } = "Kitty";

      protected Cat(IBuilder<Animal> builder) 
        : base(builder) {}

      [Configuration.Loader.Settings.Branch]
      public new class Type : Animal.Type {
        public override int DefaultNumberOfLegs
          => 4;
        public override bool CanClimb
          => true;
        protected Type(Identity id)
          : base(id ?? new(nameof(Cat))) { }
      }
    }
  }
}
