using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class ElevatorButton : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private AudioSource audio;

    private UnityAction<int> floorAction;
    private UnityAction doorAction;

    private int floor;

    public void Setup(int floor, UnityAction<int> action, string altText = "")
    {
        this.floor = floor;
        floorAction = action;
        if (altText == "")
            text.text = floor.ToString();
        else text.text = altText;
    }

    public void Setup(string text, UnityAction action)
    {
        this.text.text = text;
        doorAction = action;
    }

    public void Click()
    {
        Debug.Log("Click!");
        audio.Play();
        floorAction?.Invoke(floor);
        doorAction?.Invoke();
    }
}
