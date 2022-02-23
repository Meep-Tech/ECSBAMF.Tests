namespace Meep.Tech.Data.Tests.Examples.Enumerations {
  public class TreeType
    : Enumeration<TreeType> {

    public static TreeType Poplar { get; } 
      = new TreeType(nameof(Poplar));

    public static TreeType Birtch { get; } 
      = new TreeType(nameof(Birtch));

    protected TreeType(string uniqueNameForTreeType) 
      : base(uniqueNameForTreeType) { }

    protected override object UniqueIdCreationLogic(object uniqueIdentifier) 
      => uniqueIdentifier.ToString();
  }
}
