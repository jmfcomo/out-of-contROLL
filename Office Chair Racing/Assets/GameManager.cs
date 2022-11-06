using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    public bool hasRockets = false;
    public bool hasGains = false;
    public bool hasBouce = false;
    public bool hasSpinnyWheels = false;
    public bool areodynamic = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        GameObject player = GameObject.FindWithTag("Player");
        Collider coll = player.GetComponent<BoxCollider>();

        if (hasRockets)
        {
            player.GetComponent<PlayerControls>().hasRocket = true;
        }
        if (hasGains)
        {
            player.GetComponent<PlayerControls>().kickSpeed *= 2;
            player.GetComponent<PlayerControls>().kickSpeed *= 2;
        }
        if (hasBouce)
        {
            coll.material.bounciness = 1;
        }
        if(hasSpinnyWheels)
        {
            coll.material.dynamicFriction /= 2;

        }


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
