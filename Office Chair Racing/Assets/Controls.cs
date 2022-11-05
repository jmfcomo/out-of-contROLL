using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;


    public float kickSpeed, scootSpeed, lookSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        lookSpeed = 8f;
        kickSpeed = 8f;
        scootSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // Look
        rb.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        // Kick
        if (Input.GetMouseButtonDown(0)) {
            rb.AddRelativeForce(new Vector3(0, 0, kickSpeed), ForceMode.Impulse);
        }

        // Scoot
        if (Input.GetMouseButtonDown(1))
        {
            rb.AddRelativeForce(new Vector3(0, 0, -1 * scootSpeed), ForceMode.Impulse);
        }

    }
}
