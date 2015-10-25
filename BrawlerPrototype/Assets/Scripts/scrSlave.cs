using UnityEngine;
using System.Collections;
using System;

public class scrSlave : MessageBehaviour
{
    // Actions.
    public GameObject ActionAPrefab;
    public GameObject ActionBPrefab;
    public GameObject ActionXPrefab;
    public GameObject ActionYPrefab;
    

    // Attributes
    public int damage;
    public int defence;
    public float speed = 3;

    Animator anim;
    bool facingLeft = false;
    new Rigidbody2D rigidbody;

    // Use this for initialization
    protected override void OnStart()
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
        
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Uninterruptable") && !Input.GetButton("Fire1") && !Input.GetButton("Fire2") && !Input.GetButton("Fire3") && !Input.GetButton("Fire4"))
        {
            // Handle facing direction changes
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
        if (Input.GetButtonDown("Fire1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("ActionA"))
        {
            anim.SetBool("actionA", true);
            // Create the action A prefab.
            var actionAObj = GameObject.Instantiate(ActionAPrefab);
            // Set its creator to the current player.
            actionAObj.GetComponent<scrAction>().Creator = this.gameObject;
            // Set the current player as the action object's parent.
            actionAObj.transform.parent = this.transform;
            Debug.Log("ActionA taking place");
        }
        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("actionA", false);
        }

    }

    void ActionB()
    {
        if(Input.GetButtonDown("Fire2") && !anim.GetCurrentAnimatorStateInfo(0).IsName("ActionB"))
        {
            anim.SetBool("actionB", true);
            // Create the action B prefab.
            var actionBObj = GameObject.Instantiate(ActionBPrefab);
            // Set its creator to the current player.
            actionBObj.GetComponent<scrAction>().Creator = this.gameObject;
            // Set the current player as the action object's parent.
            actionBObj.transform.parent = this.transform;
            Debug.Log("ActionB taking place");
        }
        if (Input.GetButtonUp("Fire2"))
        {
            anim.SetBool("actionB", false);
            Messenger.SendToListeners(new Message(gameObject, "ActionBUp", ""));
        }
        
    }

    void ActionX()
    {
        if (Input.GetButtonDown("Fire3") && !anim.GetCurrentAnimatorStateInfo(0).IsName("ActionX"))
        {
            anim.SetBool("actionX", true);
            Debug.Log("ActionX taking place");
        }
        if (Input.GetButtonUp("Fire3"))
        {
            anim.SetBool("actionX", false);
        }

    }

    void ActionY()
    {
        if (Input.GetButtonDown("Fire4") && !anim.GetCurrentAnimatorStateInfo(0).IsName("ActionY"))
        {
            anim.SetBool("actionY", true);
            Debug.Log("ActionY taking place");
        }
        if (Input.GetButtonUp("Fire4"))
        {
            anim.SetBool("actionY", false);
        }

    }

    
}
