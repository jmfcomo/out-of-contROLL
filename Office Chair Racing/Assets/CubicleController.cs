using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicleController : MonoBehaviour
{

    public float vertSens = 2f;
    public float horizSens = 2f;

    public static float rotationY = 0f;
    public static float rotationX = -15f;
    public static float transitionDuration = 1f;
    private static float interpolationPosition = 1f;

    private static bool cameraUnlocked = true;

    private static Vector3 freeCamPos = new Vector3(18.29f, 2.83f, 3.73f);
    private static Vector3 upgradeMenuPos = new Vector3(19.37f, 2.45f, 6.45f);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = freeCamPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraUnlocked)
        {
            rotationY += horizSens * Input.GetAxis("Mouse X");
            rotationX -= vertSens * Input.GetAxis("Mouse Y");

            rotationX = Mathf.Clamp(rotationX, -20f, 20f);
            rotationY = Mathf.Clamp(rotationY, -80f, 55f);

            transform.eulerAngles = new Vector3(rotationX, rotationY, 0);

            if (transform.position != freeCamPos)
            {
                interpolationPosition = Mathf.Clamp(interpolationPosition + (Time.deltaTime / transitionDuration), 0f, 1f);
                transform.position = Vector3.Lerp(upgradeMenuPos, freeCamPos, interpolationPosition);
            }
        } else
        {
            if (transform.position != upgradeMenuPos)
            {
                interpolationPosition = Mathf.Clamp(interpolationPosition + (Time.deltaTime / transitionDuration), 0f, 1f);
                transform.position = Vector3.Lerp(freeCamPos, upgradeMenuPos, interpolationPosition);
                transform.eulerAngles = new Vector3(Mathf.Lerp(rotationX, 0f, interpolationPosition), Mathf.Lerp(rotationY, 0f, interpolationPosition), 0);
            }
        }
    }

    public static void EnterMenu()
    {
        if (cameraUnlocked)
        {
            cameraUnlocked = false;
            interpolationPosition = 1 - interpolationPosition;
        }
    }

    public static void ExitMenu()
    {
        if (!cameraUnlocked)
        {
            Debug.Log("Exiting menu: " + cameraUnlocked);
            cameraUnlocked = true;
            interpolationPosition = 1 - interpolationPosition;
            //Debug.Log("Exiting menu: " + interpolationPosition);
        }
    }
}
