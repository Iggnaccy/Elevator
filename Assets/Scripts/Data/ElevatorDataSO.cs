using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Uniform Elevator Data", menuName = "SO/Uniform Elevator")]
public class ElevatorDataSO : ScriptableObject, IUniformElevatorData
{
    [SerializeField] private int maxFloor;
    [SerializeField] private int minFloor;
    [SerializeField] private float floorSpacing;
    [SerializeField] private float travelSpeed;

    [SerializeField] private UnityEvent<int> onFloorChanged;

    public int MaxFloor => maxFloor;

    public int MinFloor => minFloor;

    public float FloorSpacing => floorSpacing;

    public float TravelSpeed => travelSpeed;
}
