using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Kick
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(new Vector3(0, 0, speed));
            Debug.Log("boop");
        }        

    }
}
