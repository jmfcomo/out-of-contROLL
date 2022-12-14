using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    private Animator animator;

    private float cameraDif;
    private Vector3 mousePos;
    public float kickSpeed, scootSpeed;
    //private AudioSource[] sources;


    public bool hasRocket = false;
    private bool rocketActive = false;
    public float rocketSpeed;
    public float rocketTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        cameraDif = cam.transform.position.y - rb.transform.position.y;

        animator = gameObject.GetComponentInChildren<Animator>();

        Debug.Log(animator);

        //sources = gameObject.GetComponents<AudioSource>();
       // Debug.Log(sources.Length);

        kickSpeed = 8f;
        scootSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        // Look
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        

        // Kick
        if (Input.GetMouseButtonDown(0)) {
            //rb.AddRelativeForce(new Vector3(0, 0, kickSpeed), ForceMode.Impulse);
            Movement.Kick(kickSpeed, rb, animator);
        }

        // Scoot
        if (Input.GetMouseButtonDown(1))
        {
            //rb.AddRelativeForce(new Vector3(0, 0, -1 * scootSpeed), ForceMode.Impulse);
            Movement.Scoot(scootSpeed, rb, animator);
        }

        if (hasRocket == true && Input.GetKeyDown(KeyCode.Space) && RaceControl.canMove)
        {
            rocketActive = true;
            StartCoroutine(Rocket());
            hasRocket = false;
            PersonalAudio.RocketAudio();
        }
    }

    private void FixedUpdate()
    {

      float  mouseX = Input.mousePosition.x;

      float  mouseY = Input.mousePosition.y;

        Vector3 worldpos = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cameraDif));

        Vector3 lookDirection = new Vector3(worldpos.x, rb.transform.position.y, worldpos.z);

        rb.transform.LookAt(lookDirection);

        if (rocketActive==true)
        {
            rb.AddRelativeForce(new Vector3(0, 0, rocketSpeed));
        }
    }

    IEnumerator Rocket()
    {
        
        yield return new WaitForSeconds(rocketTime);
        rocketActive = false; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        PersonalAudio.Hit();
    }
}
