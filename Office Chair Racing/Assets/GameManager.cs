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
