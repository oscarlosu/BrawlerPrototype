using UnityEngine;
using System.Collections;

public class scrSwordAttack1 : scrAction
{
    Animator anim;
    // Use this for initialization
    protected override void OnStart()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Destroys itself after animation is done
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Done"))
        {
            GameObject.Destroy(this.gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && Creator != other.gameObject)
        {
            Debug.Log("Player was hit.");
            GameObject.Destroy(this.gameObject);
        }
    }
}
