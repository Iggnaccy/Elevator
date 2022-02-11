using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUniformElevatorData : IElevatorData
{
    public float FloorSpacing { get; }
}
