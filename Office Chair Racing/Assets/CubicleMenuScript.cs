using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class CubicleMenuScript : MonoBehaviour
{
    public float minAngle, maxAngle, fadeSpeed;

    public UnityEvent e;

    private float alpha;

    private TextMeshPro t;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 0;
        t = gameObject.GetComponentInChildren<TextMeshPro>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (CubicleController.rotationY > minAngle & CubicleController.rotationY < maxAngle)
        {
            alpha += fadeSpeed;
            if (Input.GetMouseButtonDown(0))
            { 
                Debug.Log("Click");
                e.Invoke();
            }
        }
        else
        {
            alpha -= fadeSpeed;
        }

        alpha = Mathf.Clamp(alpha, 0, 1);

        t.color = new Color(0, 0, 0, alpha);

    }
    /*
    void OnMouseDown()
    {
        Debug.Log("A click somewhere");

        if (CubicleController.rotationY > minAngle & CubicleController.rotationY < maxAngle)
        {
            Debug.Log("Click in my bounds");
            e.Invoke();
        }
    }
    */

    public void Race()
    {
        SceneManager.LoadSceneAsync("Stage 1");
    }

    public void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}