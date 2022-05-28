namespace Meep.Tech.Data.Examples {

  /// <summary>
  /// The Base Model for all CapacitorDetectors
  /// </summary>
  public class CapacitorDetector
    : IModel.IComponent<CapacitorDetector>,
      IComponent.IUseDefaultUniverse,
      IModel.IComponent<CapacitorDetector>.IHaveContractWith<CapacitorData> 
  {

    public bool CapacitorWasDetected {
      get; private set;
    } = false;

    (CapacitorDetector @this, CapacitorData other) IComponent<CapacitorDetector>.IHaveContractWith<CapacitorData>.ExecuteContractWith(CapacitorData otherComponent) { 
      CapacitorWasDetected = true;
      return (this, otherComponent);
    }
  }
}
