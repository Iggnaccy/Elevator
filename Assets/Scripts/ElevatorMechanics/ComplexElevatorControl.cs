using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexElevatorControl : ElevatorControlBase<ComplexElevatorDataSO>
{
    protected override float GetFloorOffset(int floor)
    {
        return data.FloorHeights[floor];
    }

    protected override float GetFloorHeight(int floor)
    {
        return data.FloorHeights[floor];
    }
}
