using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElevatorData
{
    public int MaxFloor { get; }
    public int MinFloor { get; }
    public float TravelSpeed { get; }
}
