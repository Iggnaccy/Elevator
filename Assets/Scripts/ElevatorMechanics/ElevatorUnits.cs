using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ElevatorUnits : MonoBehaviour, IEnumerable<Transform>
{
    private List<Transform> unitsInside;

    public List<Transform> UnitsInside => unitsInside;

    private void Start()
    {
        unitsInside = new List<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var otherTransform = other.transform;
            if (unitsInside.Contains(otherTransform))
            {
                unitsInside.Remove(otherTransform);
            }
            else
            {
                unitsInside.Add(otherTransform);
            }
        }
    }

    public IEnumerator<Transform> GetEnumerator()
    {
        return ((IEnumerable<Transform>)UnitsInside).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)UnitsInside).GetEnumerator();
    }

    public Transform this[int index]
    {
        get => UnitsInside[index];
    }
}
