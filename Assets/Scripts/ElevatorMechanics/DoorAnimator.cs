using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorAnimator : MonoBehaviour
{
    [SerializeField] private Transform leftDoor, rightDoor;
    [SerializeField] private Axis animationAxis;
    [SerializeField] private float zeroValue, totalChange;
    [SerializeField] private float speed;
    [SerializeField] private float maxOpenTime;

    public UnityEvent onClosed;

    private bool isLocked;
    private bool isOpening;
    private bool isClosed;
    private bool isClosing;
    private float lastOpenTime;

    public bool IsClosed => isClosed;
    public bool IsLocked => isLocked;

    private void Start()
    {
        Open();
        lastOpenTime = Time.time;
    }

    enum Axis
    {
        X, Z
    }

    private void Update()
    {
        if(Time.time - lastOpenTime > maxOpenTime)
        {
            if(!isOpening && !isClosed && !isClosing && !isLocked)
            {
                Close();
            }
        }
    }

    public void Open()
    {
        if (isOpening || isLocked) return;
        StopAllCoroutines();
        StartCoroutine(OpenAnimation());
    }

    public void Close()
    {
        if (isOpening || isClosing || isClosed || isLocked) return;
        StopAllCoroutines();
        StartCoroutine(CloseAnimation());
    }

    public void SetLock(bool value)
    {
        isLocked = value;
    }

    private IEnumerator OpenAnimation()
    {
        if (isOpening || isLocked) yield break;
        Debug.Log("Door started opening");
        isOpening = true;
        isClosing = false;
        isClosed = false;
        yield return new WaitForFixedUpdate();
        var startValue = GetPositionValue();
        var currentValue = startValue;
        while(currentValue < totalChange)
        {
            AdjustValuesBy(speed * Time.deltaTime);
            currentValue = GetPositionValue();
            yield return new WaitForFixedUpdate();
        }
        AdjustValuesBy(totalChange - currentValue);
        isOpening = false;
        lastOpenTime = Time.time;
    }

    private IEnumerator CloseAnimation()
    {
        if (isOpening || isClosing || isLocked) yield break;
        isClosing = true;
        Debug.Log("Door started closing");
        var startValue = GetPositionValue();
        var currentValue = startValue;
        while (Mathf.Abs(currentValue) > 0.005f)
        {
            AdjustValuesBy(-speed * Time.deltaTime);
            currentValue = GetPositionValue();
            yield return null;
        }
        AdjustValuesBy(-currentValue);
        isClosing = false;
        isClosed = true;
        onClosed?.Invoke();
    }

    private float GetPositionValue()
    {
        if (animationAxis == Axis.X)
            return Mathf.Abs(leftDoor.position.x - zeroValue);
        else return Mathf.Abs(leftDoor.position.z - zeroValue);
    }

    private void AdjustValuesBy(float value)
    {
        if(animationAxis == Axis.X)
        {
            if (Mathf.Abs(leftDoor.transform.position.x - value) < Mathf.Abs(zeroValue - totalChange))
            {
                leftDoor.position = new Vector3(leftDoor.position.x - value, leftDoor.position.y, leftDoor.position.z);
                rightDoor.position = new Vector3(rightDoor.position.x + value, rightDoor.position.y, rightDoor.position.z);
            }
            else
            {
                leftDoor.position = new Vector3(zeroValue - totalChange, leftDoor.position.y, leftDoor.position.z);
                rightDoor.position = new Vector3(-zeroValue + totalChange, rightDoor.position.y, rightDoor.position.z);
            }
        }
        else
        {
            if (Mathf.Abs(leftDoor.transform.position.z - value) < Mathf.Abs(zeroValue - totalChange))
            {
                leftDoor.position = new Vector3(leftDoor.position.x, leftDoor.position.y, leftDoor.position.z - value);
                rightDoor.position = new Vector3(rightDoor.position.x, rightDoor.position.y, rightDoor.position.z + value);
            }
            else
            {
                leftDoor.position = new Vector3(leftDoor.position.x, leftDoor.position.y, zeroValue - totalChange);
                rightDoor.position = new Vector3(rightDoor.position.x, rightDoor.position.y, -zeroValue + totalChange);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isClosed && other.CompareTag("Player"))
        {
            Open();
            isLocked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isLocked = false;
        }
    }
}
