using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RaceControl : MonoBehaviour
{

    private float[] parTimes = { 6, 8, 16, 16, 9 };

    private bool firstPlace = true;

    private float start;

    public float countdown;

    public static bool canMove = false;

    private TextMeshProUGUI t;

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        t = FindObjectOfType<TextMeshProUGUI>();
    }

    IEnumerator CountdownToMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Cubicle");
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            t.text = countdown.ToString("n2");
            if (countdown <= 0)
            {
                t.text = "";
                canMove = true;
                start = Time.time;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            canMove = false;

            float finalTime = Time.time - start;

            if (firstPlace)
            {
                t.text = "You win!";
                if (GameManager.levelsUnlocked <= GameManager.TOTAL_LEVELS)
                    GameManager.levelsUnlocked++;
                string sceneName = SceneManager.GetActiveScene().name;
                int currentScene = (int)char.GetNumericValue(sceneName[sceneName.Length - 1]);
                Debug.Log(currentScene);
                GameManager.money += Mathf.Clamp((parTimes[currentScene - 1] - finalTime) * 3, 0, 100);

                GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayEndSound(true);
            } else
            {
                t.text = "You lose!";
                string sceneName = SceneManager.GetActiveScene().name;
                int currentScene = (int)char.GetNumericValue(sceneName[sceneName.Length - 1]);
                Debug.Log(currentScene);

                GameManager.money += Mathf.Clamp(parTimes[ currentScene - 1 ] - finalTime, 0, 100);

                GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayEndSound(false);

            }


            t.text += "\n" + finalTime.ToString("n3");

            StartCoroutine(CountdownToMenu());

        } else
        {
            firstPlace = false;
        }
    }
}
