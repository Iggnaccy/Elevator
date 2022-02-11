using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniformElevatorControl : ElevatorControlBase<ElevatorDataSO>
{
    private float zeroY;

    protected override void Start()
    {
        zeroY = transform.position.y - currentFloor * data.FloorSpacing;
        SpawnButtons();
    }

    protected override float GetFloorOffset(int floor)
    {
        return floor * data.FloorSpacing;
    }

    protected override float GetFloorHeight(int floor)
    {
        return zeroY + floor * data.FloorSpacing;
    }
}
