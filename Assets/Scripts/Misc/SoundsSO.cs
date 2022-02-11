using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used only for segregation
[CreateAssetMenu(fileName = "Sounds", menuName = "SO/Sounds")]
public class SoundsSO : ScriptableObject
{
    public AudioClip buttonClick, elevatorArrived, elevatorRiding;
}
