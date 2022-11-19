using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;
using TMPro;

public class GameManager: MonoBehaviour
{
    public bool hasRockets = false;
    public bool hasGains = false;
    public bool hasBounce = true;
    public bool hasSpinnyWheels = false;
    public bool aerodynamic = false;

    public const int TOTAL_LEVELS = 5;

    public static int levelsUnlocked = 1;

    public static float money = 10;

    private static int storePage = 0;

    public static UpgradeItem[] upgradeItems =
    {
        new UpgradeItem("Gym Membership", 25),
        new UpgradeItem("Rockets", 100),
        new UpgradeItem("Bounciness", 20),
        new UpgradeItem("Spinny wheels", 20),
        new UpgradeItem("Aerodynamic chair", 50)
    };

    public void Right()
    {
        if (++storePage >= upgradeItems.Length)
        {
            storePage = 0;
        }

        TextMeshProUGUI t = GameObject.Find("Item").GetComponent<TextMeshProUGUI>();
        t.text = upgradeItems[storePage].name;
        t.text += "\n$" + upgradeItems[storePage].cost;
    }
    public void Left()
    {
        if (--storePage <= 0)
        {
            storePage = upgradeItems.Length;
        }

        TextMeshProUGUI t = GameObject.Find("Item").GetComponent<TextMeshProUGUI>();
        t.text = upgradeItems[storePage].name;
        t.text += "\n$" + upgradeItems[storePage].cost;
    }

    public static void StartUpgradeMenu()
    {
        Cursor.lockState = CursorLockMode.None;

        GameManager g = GameObject.FindObjectOfType<GameManager>();
        g.StartCoroutine("WaitForTransition");
        //GameObject.Find("UpgradeMenu").GetComponent<Canvas>().enabled = true;

        TextMeshProUGUI t = GameObject.Find("Item").GetComponent<TextMeshProUGUI>();
        t.text = upgradeItems[storePage].name;
        t.text += "\n$" + upgradeItems[storePage].cost;

        t = GameObject.Find("moneys").GetComponent<TextMeshProUGUI>();
        t.text = "$" + money.ToString("n2");
    }

    public static void ExitUpgradeMenu()
    {
        GameObject.Find("UpgradeMenu").GetComponent<Canvas>().enabled = false;
        CubicleController.ExitMenu();
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindObjectOfType<GameManager>().StopCoroutine("WaitForTransition");
    }

    IEnumerator WaitForTransition()
    {
        yield return new WaitForSeconds(CubicleController.transitionDuration);
        GameObject.Find("UpgradeMenu").GetComponent<Canvas>().enabled = true;
    }

    public void Buy()
    {
        if (upgradeItems[storePage].cost < money)
        {
            money -= upgradeItems[storePage].cost;
            if (upgradeItems[storePage].name == "Gym Membership")
            {
                hasGains = true;
                upgradeItems[storePage].cost = 0;
            } else if (upgradeItems[storePage].name == "Rockets") {
                hasRockets = true;
                upgradeItems[storePage].cost = 0;
            } else if (upgradeItems[storePage].name == "Bounciness") {
                hasBounce = true;
                upgradeItems[storePage].cost = 0;
            } else if (upgradeItems[storePage].name == "Spinny wheels") {
                hasSpinnyWheels = true;
                upgradeItems[storePage].cost = 0;
            } else if (upgradeItems[storePage].name == "Aerodynamic chair") {
                aerodynamic = true;
                upgradeItems[storePage].cost = 0;
            }

            TextMeshProUGUI t = GameObject.Find("Item").GetComponent<TextMeshProUGUI>();
            t.text = upgradeItems[storePage].name;
            t.text += "\n$" + upgradeItems[storePage].cost;

            t = GameObject.Find("moneys").GetComponent<TextMeshProUGUI>();
            t.text = "$" + money.ToString("n2");
        }
    }

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
        if (aerodynamic)
        {
            player.GetComponent<Rigidbody>().drag = 0;
            player.transform.GetChild(0).GetComponentInChildren<MeshRenderer>().enabled = true;
            player.transform.GetChild(1).GetComponentInChildren<MeshRenderer>().enabled = false;
        }

        Debug.Log("GameManager did its magic");
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            ExitUpgradeMenu();
        }        
    }
}

public class UpgradeItem
{
    public string name;
    public float cost;

    public UpgradeItem(string n, float c)
    {
        name = n;
        cost = c;
    }
};


