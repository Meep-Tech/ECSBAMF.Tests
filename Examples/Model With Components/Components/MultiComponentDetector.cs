namespace Meep.Tech.Data.Examples {

  /// <summary>
  /// The Base Model for all CapacitorDetectors
  /// </summary>
  public class MultiComponentDetector
    : IModel.IComponent<MultiComponentDetector>,
      IComponent.IUseDefaultUniverse,
      IModel.IComponent<MultiComponentDetector>.IHaveContractWith<CapacitorData>,
      IModel.IComponent<MultiComponentDetector>.IHaveContractWith<DisplayComponent> 
  {

    public bool CapacitorWasDetected {
      get; private set;
    } = false;

    (MultiComponentDetector @this, CapacitorData other) IComponent<MultiComponentDetector>.IHaveContractWith<CapacitorData>.ExecuteContractWith(CapacitorData otherComponent) { 
      CapacitorWasDetected = true;
      return (this, otherComponent);
    }

    public bool DisplayComponentWasDetected {
      get; private set;
    } = false;

    (MultiComponentDetector @this, DisplayComponent other) IComponent<MultiComponentDetector>.IHaveContractWith<DisplayComponent>.ExecuteContractWith(DisplayComponent otherComponent) { 
      DisplayComponentWasDetected = true;
      return (this, otherComponent);
    }
  }
}
