namespace Meep.Tech.Data.Tests.Examples.Enumerations {
  public class AnimalType : Enumeration<AnimalType> {

    public static AnimalType Fish { get; } 
      = new AnimalType(nameof(Fish));

    public static AnimalType Frog { get; } 
      = new AnimalType(nameof(Frog));

    protected AnimalType(string uniqueKey) 
      : base(uniqueKey) { } 
  }
}
