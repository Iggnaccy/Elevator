using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElevatorControlBase<T> : MonoBehaviour, IElevatorControl<T> where T : ScriptableObject, IElevatorData
{
    [SerializeField] protected T data;
    [SerializeField] protected DoorAnimator doorAnimationControl;
    [SerializeField] protected int currentFloor;
    [SerializeField] protected AudioSource onArrival, speaker;
    [SerializeField] protected ElevatorUnits unitsInside;

    [Space(1)]
    [Header("Initialization")]
    [SerializeField] protected GameObject elevatorButtonPrefab;
    [SerializeField] protected Transform buttonsParent;
    [SerializeField] protected Transform callButtonRelativePosition;
    [SerializeField] protected int columnCount;
    [SerializeField] protected float spacing;
    [SerializeField] protected float controlButtonsY;

    public T Data => data;
    public ElevatorUnits UnitsInside => unitsInside;

    protected bool isRiding;
    protected int targetFloor;

    protected virtual void Start()
    {
        SpawnButtons();
    }

    public void CloseTheDoor()
    {
        doorAnimationControl.Close();
    }

    public void OpenTheDoor()
    {
        doorAnimationControl.Open();
    }

    private void StartAnimation()
    {
        doorAnimationControl.onClosed.RemoveListener(StartAnimation);
        StartCoroutine(RideAnimation(targetFloor));
    }

    public void Ride(int floor)
    {
        if (isRiding || doorAnimationControl.IsLocked) return;
        if (floor == currentFloor)
        {
            OpenTheDoor();
            return;
        }
        targetFloor = floor;
        if (!doorAnimationControl.IsClosed)
        {
            doorAnimationControl.onClosed.AddListener(StartAnimation);
            doorAnimationControl.Close();
        }
        else
        {
            StartAnimation();
        }
    }

    protected virtual IEnumerator RideAnimation(int floor)
    {
        if (isRiding) yield break;
        isRiding = true;
        yield return new WaitForFixedUpdate();
        speaker.Play();
        doorAnimationControl.SetLock(true);
        var targetY = GetFloorHeight(floor);
        bool ascending = currentFloor < floor;
        while (Mathf.Abs(transform.position.y - targetY) > 0.05f)
        {
            var positionDiff = data.TravelSpeed * Time.deltaTime * (ascending ? 1 : -1);
            transform.position = new Vector3(transform.position.x, transform.position.y + positionDiff, transform.position.z);
            if(!ascending)
            {
                foreach(var unit in unitsInside)
                {
                    unit.position = new Vector3(unit.position.x, unit.position.y + positionDiff, unit.position.z);
                }
            }
            yield return new WaitForFixedUpdate();
        }
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        doorAnimationControl.SetLock(false);
        doorAnimationControl.Open();
        speaker.Stop();
        onArrival.Play();
        currentFloor = floor;
        isRiding = false;
    }

    protected virtual void SpawnButtons()
    {
        int total = data.MaxFloor - data.MinFloor + 1;
        for (int i = 0; i < total; i++)
        {
            var newButtonTransform = Instantiate(elevatorButtonPrefab, buttonsParent, false).GetComponent<Transform>();
            newButtonTransform.localPosition = new Vector3(i % columnCount * spacing, -i / columnCount * spacing, 0);
            newButtonTransform.GetComponent<ElevatorButton>().Setup(data.MinFloor + i, Ride);
            var callButton = Instantiate(elevatorButtonPrefab, new Vector3(callButtonRelativePosition.position.x, callButtonRelativePosition.position.y + GetFloorOffset(i), callButtonRelativePosition.position.z), Quaternion.identity).GetComponent<ElevatorButton>();
            callButton.Setup(data.MinFloor + i, Ride, "Call");
        }
        var openDoorButton = Instantiate(elevatorButtonPrefab, buttonsParent, false).GetComponent<Transform>();
        openDoorButton.localPosition = new Vector3(0, controlButtonsY, 0);
        openDoorButton.GetComponent<ElevatorButton>().Setup("Open", OpenTheDoor);
        var closeDoorButton = Instantiate(elevatorButtonPrefab, buttonsParent, false).GetComponent<Transform>();
        closeDoorButton.localPosition = new Vector3(spacing, controlButtonsY, 0);
        closeDoorButton.GetComponent<ElevatorButton>().Setup("Close", CloseTheDoor);
    }

    protected abstract float GetFloorOffset(int floor);

    protected abstract float GetFloorHeight(int floor);
}
