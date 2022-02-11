using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Complex Elevator Data", menuName = "SO/Complex Elevator Data")]
public class ComplexElevatorDataSO : ScriptableObject, IComplexElevatorData
{
    [SerializeField] private List<float> floorHeights;
    [SerializeField] private int maxFloor, minFloor;
    [SerializeField] private float travelSpeed;

    [SerializeField] private UnityEvent<int> onFloorChanged;

    public IList<float> FloorHeights => floorHeights;

    public int MaxFloor => maxFloor;

    public int MinFloor => minFloor;

    public float TravelSpeed => travelSpeed;
}
