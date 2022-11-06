using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalAudio : MonoBehaviour
{
    public static AudioSource[] sources;
    private Rigidbody rb;
    public float factorDrop;
    public float rollVol;

    // Start is called before the first frame update
    void Start()
    {
        sources = gameObject.GetComponents<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody>();

        sources[2].Play(0);
       
    }

    public void FixedUpdate()
    {
        float number = Mathf.Abs(rb.velocity.x * rb.velocity.z) / factorDrop;
        //Debug.Log(number);
        sources[2].volume = Mathf.Min(number, rollVol);
       

    }

    public static void KickAudio()
    {
        sources[0].PlayOneShot(sources[0].clip, 1);
    }

    public static void ScootAudio()
    {
        sources[1].PlayOneShot(sources[1].clip,1);
    }

    public static void Hit()
    {
        sources[3].PlayOneShot(sources[3].clip, 1);
    }

    public static void RocketAudio()
    {
        sources[4].PlayOneShot(sources[4].clip, 1);
    }
}
