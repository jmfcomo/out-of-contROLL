using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicleController : MonoBehaviour
{

    public float vertSens = 2f;
    public float horizSens = 2f;

    public static float rotationY = 0f;
    public static float rotationX = -15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationY += horizSens * Input.GetAxis("Mouse X");
        rotationX -= vertSens * Input.GetAxis("Mouse Y");

        rotationX = Mathf.Clamp(rotationX, -20f, 20f);
        rotationY = Mathf.Clamp(rotationY, -80f, 25f);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}
