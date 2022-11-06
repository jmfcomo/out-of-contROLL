using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public static void Kick(float kickSpeed, Rigidbody rb, Animator? a = null)
    {
        PersonalAudio.KickAudio();

        rb.AddRelativeForce(new Vector3(0, 0, -1* kickSpeed), ForceMode.Impulse);
        if (a != null)
        {
            a.Play("kick");
        }
   
    }

    public static void Scoot(float scootSpeed, Rigidbody rb,  Animator? a = null)
    {
        PersonalAudio.ScootAudio();

        rb.AddRelativeForce(new Vector3(0, 0, scootSpeed), ForceMode.Impulse);
        if (a != null)
        {
            Debug.Log("WE KICKIN");
            a.Play("scoot");
        }

    }
}

