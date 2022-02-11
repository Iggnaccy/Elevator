using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElevatorControl<T> where T : IElevatorData
{
    public ElevatorUnits UnitsInside {get;}
    public T Data { get; }

    public void OpenTheDoor();
    public void CloseTheDoor();
    public void Ride(int floor);
}
