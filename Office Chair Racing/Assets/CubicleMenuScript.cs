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

        GameObject.Find("UpgradeMenu").GetComponent<Canvas>().enabled = false;
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

    public void Race()
    {
        if (GameManager.levelsUnlocked <= GameManager.TOTAL_LEVELS)
            SceneManager.LoadSceneAsync("Stage " + GameManager.levelsUnlocked);
        else
            SceneManager.LoadSceneAsync("Stage " + new System.Random().Next(1, GameManager.TOTAL_LEVELS));
    }

    public void Upgrade()
    {
        Cursor.lockState = CursorLockMode.None;
        //GameObject g = GameObject.Find("GameManager").GetComponent<GameObject>();
        GameManager.StartUpgradeMenu();
    }

    public void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void ExitGame()
    {
        Debug.Log("Byeeee");
        Application.Quit();
    }
}