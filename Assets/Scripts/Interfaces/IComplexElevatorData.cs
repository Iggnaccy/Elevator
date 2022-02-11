using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComplexElevatorData : IElevatorData
{
    public IList<float> FloorHeights { get; }
}
