using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static void Kick(float kickSpeed, Rigidbody rb)
    {
        rb.AddRelativeForce(new Vector3(0, 0, kickSpeed), ForceMode.Impulse);
    }

    public static void Scoot(float scootSpeed, Rigidbody rb)
    {
        rb.AddRelativeForce(new Vector3(0, 0, -1 * scootSpeed), ForceMode.Impulse);
    }
}

