using UnityEngine;
using System.Collections;
using System;
using GamepadInput;

public class scrSlave : MessageBehaviour
{
    // Actions.
    public GameObject ActionAPrefab;
    public GameObject ActionBPrefab;
    public GameObject ActionXPrefab;
    public GameObject ActionYPrefab;

    public GamePad.Index ControllerIndex;
    

    // Attributes
    public int damage;
    public int defence;
    public float speed;

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
        float axisX = GamePad.GetAxis(GamePad.Axis.LeftStick, ControllerIndex).x;
        float axisY = GamePad.GetAxis(GamePad.Axis.LeftStick, ControllerIndex).y;
        // Update animation parameters
        anim.SetFloat("axisX", axisX);
        anim.SetFloat("axisY", axisY);
        
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Uninterruptable") && !GamePad.GetButton(GamePad.Button.A, ControllerIndex) && !GamePad.GetButton(GamePad.Button.B, ControllerIndex) && !GamePad.GetButton(GamePad.Button.X, ControllerIndex) && !GamePad.GetButton(GamePad.Button.Y, ControllerIndex))
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
        
        if (CanActivateButton("A"))
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
        else
        {
            anim.SetBool("actionA", false);
        }
        if (GamePad.GetButtonUp(GamePad.Button.A, ControllerIndex))
        {
            anim.SetBool("actionA", false);
        }

    }

    void ActionB()
    {
        if(CanActivateButton("B"))
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
        if (GamePad.GetButtonUp(GamePad.Button.B, ControllerIndex))
        {
            anim.SetBool("actionB", false);
            Messenger.SendToListeners(new Message(gameObject, "ActionBUp", ""));
        }
        
    }

    void ActionX()
    {
        if (CanActivateButton("X"))
        {
            anim.SetBool("actionX", true);
            Debug.Log("ActionX taking place");
        }
        if (GamePad.GetButtonUp(GamePad.Button.X, ControllerIndex))
        {
            anim.SetBool("actionX", false);
        }

    }

    void ActionY()
    {
        if (CanActivateButton("Y"))
        {
            anim.SetBool("actionY", true);
            Debug.Log("ActionY taking place");
        }
        if (GamePad.GetButtonUp(GamePad.Button.Y, ControllerIndex))
        {
            anim.SetBool("actionY", false);
        }

    }

    private bool CanActivateButton(string button)
    {
        switch (button)
        {
            case "A":
                return (GamePad.GetButtonDown(GamePad.Button.A, ControllerIndex)
                    && !anim.GetCurrentAnimatorStateInfo(0).IsName("Action" + button)
                    && !anim.GetBool("action" + button)
                    && !GamePad.GetButton(GamePad.Button.B, ControllerIndex)
                    && !GamePad.GetButtonDown(GamePad.Button.X, ControllerIndex)
                    && !GamePad.GetButtonDown(GamePad.Button.Y, ControllerIndex));
            case "B":
                return (GamePad.GetButtonDown(GamePad.Button.B, ControllerIndex)
                    && !anim.GetCurrentAnimatorStateInfo(0).IsName("Action" + button)
                    && !anim.GetBool("action" + button)
                    && !GamePad.GetButton(GamePad.Button.A, ControllerIndex)
                    && !GamePad.GetButtonDown(GamePad.Button.X, ControllerIndex)
                    && !GamePad.GetButtonDown(GamePad.Button.Y, ControllerIndex));
            case "X":
                return (GamePad.GetButtonDown(GamePad.Button.X, ControllerIndex)
                    && !anim.GetCurrentAnimatorStateInfo(0).IsName("Action" + button)
                    && !anim.GetBool("action" + button)
                    && !GamePad.GetButton(GamePad.Button.A, ControllerIndex)
                    && !GamePad.GetButtonDown(GamePad.Button.B, ControllerIndex)
                    && !GamePad.GetButtonDown(GamePad.Button.Y, ControllerIndex));
            case "Y":
                return (GamePad.GetButtonDown(GamePad.Button.Y, ControllerIndex)
                    && !anim.GetCurrentAnimatorStateInfo(0).IsName("Action" + button)
                    && !anim.GetBool("action" + button)
                    && !GamePad.GetButton(GamePad.Button.A, ControllerIndex)
                    && !GamePad.GetButtonDown(GamePad.Button.B, ControllerIndex)
                    && !GamePad.GetButtonDown(GamePad.Button.X, ControllerIndex));
        }
        return false;
    }
}
