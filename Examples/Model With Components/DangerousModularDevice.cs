namespace Meep.Tech.Data.Examples {
  /// <summary>
  /// Dangerous version, lets you delete things.
  /// </summary>
  public partial class DangerousModularDevice 
    : SafeModularDevice, 
      IWriteableComponentStorage 
  { }
}
