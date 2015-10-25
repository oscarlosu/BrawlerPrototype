using UnityEngine;
using System.Collections;
using System;

public class scrSlave : scrObserver
{
    // Attributes
    public int damage;
    public int defence;
    public float speed = 3;

    Animator anim;
    bool facingLeft = false;
    new Rigidbody2D rigidbody;

    // Use this for initialization
    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        ActionA();
        ActionB();
        ActionX();
        ActionY();

    }

    void HandleMovement()
    {
        // Get left joystick input
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");
        // Update animation parameters
        anim.SetFloat("axisX", axisX);
        anim.SetFloat("axisY", axisY);

        if (!Input.GetButton("Fire1") && !Input.GetButton("Fire2") && !Input.GetButton("Fire3") && !Input.GetButton("Fire4"))
        {
            
            // Handle facing directino changes
            if (!facingLeft && axisX < 0)
            {
                ChangeFacingDir();
            }
            else if (facingLeft && axisX > 0)
            {
                ChangeFacingDir();
            }
            // Move character
            rigidbody.velocity = new Vector3(axisX * speed, axisY * speed);
        }
        else
        {
            rigidbody.velocity = new Vector3(0, 0);
        }
    
    }

    void ChangeFacingDir()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void ActionA()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("actionA", true);
            Debug.Log("ActionA taking place");
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("actionA", false);
        }

    }

    void ActionB()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("actionB", true);
            Debug.Log("ActionB taking place");
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            anim.SetBool("actionB", false);
        }
        
    }

    void ActionX()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            anim.SetBool("actionX", true);
            Debug.Log("ActionX taking place");
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            anim.SetBool("actionX", false);
        }

    }

    void ActionY()
    {
        if (Input.GetButtonDown("Fire4"))
        {
            anim.SetBool("actionY", true);
            Debug.Log("ActionY taking place");
        }
        else if (Input.GetButtonUp("Fire4"))
        {
            anim.SetBool("actionY", false);
        }

    }



    public override void Notify(scrObservable mine)
    {
        Debug.Log("scrObservable mine");
    }

    public override void Notify(scrObservable mine, scrObservable other)
    {
        Debug.Log("scrObservable mine, scrObservable other");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the slave collides with an observable that is not his
        scrObservable obs = other.gameObject.GetComponent<scrObservable>();
        if (obs != null && obs.Obs != this)
        {
            // Handle effect
            Debug.Log("Touché!");
        }
        

    }
}
