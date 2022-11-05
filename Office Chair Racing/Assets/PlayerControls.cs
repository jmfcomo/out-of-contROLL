using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;

    private float cameraDif;
    private Vector3 mousePos;
    public float kickSpeed, scootSpeed, lookSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        cameraDif = cam.transform.position.y - rb.transform.position.y;

        lookSpeed = 8f;
        kickSpeed = 8f;
        scootSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // Look
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        

        // Kick
        if (Input.GetMouseButtonDown(0)) {
            //rb.AddRelativeForce(new Vector3(0, 0, kickSpeed), ForceMode.Impulse);
            Movement.Kick(kickSpeed, rb);
        }

        // Scoot
        if (Input.GetMouseButtonDown(1))
        {
            //rb.AddRelativeForce(new Vector3(0, 0, -1 * scootSpeed), ForceMode.Impulse);
            Movement.Scoot(scootSpeed, rb);
        }

    }

    private void FixedUpdate()
    {
        /*Debug.Log(Input.mousePosition);
        Debug.Log(mousePos);

        Vector3 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation *= Quaternion.Euler(0, angle * lookSpeed, 0);*/


      float  mouseX = Input.mousePosition.x;

      float  mouseY = Input.mousePosition.y;

        Vector3 worldpos = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cameraDif));

        Vector3 lookDirection = new Vector3(worldpos.x, rb.transform.position.y, worldpos.z);

        rb.transform.LookAt(lookDirection);
    }
}
