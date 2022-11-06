using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    public bool hasRockets = false;
    public bool hasGains = false;
    public bool hasBounce = true;
    public bool hasSpinnyWheels = false;
    public bool areodynamic = false;

    public const int TOTAL_LEVELS = 5;

    public static int levelsUnlocked;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("GameManager doing its magic");

        GameObject player = GameObject.FindWithTag("Player");
        Collider coll = player.GetComponent<BoxCollider>();

        if (hasRockets)
        {
            player.GetComponent<PlayerControls>().hasRocket = true;
            MeshRenderer[] rocketRender = player.transform.GetChild(3).GetComponentsInChildren<MeshRenderer>();
            BoxCollider[] rocketColl = player.transform.GetChild(3).GetComponentsInChildren<BoxCollider>();

            foreach(MeshRenderer rocRend in rocketRender)
            rocRend.enabled = true;
            foreach(BoxCollider recColl in rocketColl)
            recColl.enabled = true;
        }
        if (hasGains)
        {
            player.GetComponent<PlayerControls>().kickSpeed *= 2;
            player.GetComponent<PlayerControls>().scootSpeed *= 2;
        }
        if (hasBounce)
        {
            coll.material.bounciness = 1;
        }
        if (hasSpinnyWheels)
        {
            coll.material.dynamicFriction /= 2;
        }
        if (areodynamic)
        {
            player.GetComponent<Rigidbody>().drag = 0;
            player.transform.GetChild(0).GetComponentInChildren<MeshRenderer>().enabled = true;
            player.transform.GetChild(1).GetComponentInChildren<MeshRenderer>().enabled = false;
        }

        Debug.Log("GameManager did its magic");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stage 1");
    }

    public void LevelWin()
    {
        SceneManager.LoadScene("Cubicle");
    }


}
