using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Camera cam;

    void Start()
    {
        if (!rigidbody)
            rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                var objectHit = hit.transform;
                var button = objectHit.GetComponent<ElevatorButton>();
                if (button)
                {
                    button.Click();
                }
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 direction = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            direction += transform.forward;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            direction -= transform.forward;
        }
        if(Input.GetKey(KeyCode.D))
        {
            direction += transform.right;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            direction -= transform.right;
        }
        Vector3 final = new Vector3(direction.normalized.x * speed, rigidbody.velocity.y, direction.normalized.z * speed);
        rigidbody.velocity = final;
    }
}
