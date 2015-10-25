using UnityEngine;
using System.Collections;

public class scrSwordAttack1 : scrAction
{
    Animator anim;

    public float AnimSpeed;
    // Use this for initialization
    protected override void OnStart()
    {
        anim = GetComponent<Animator>();
        transform.localPosition = new Vector3(0, 0);
        //StartCoroutine("Animate");
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

    IEnumerator Animate()
    {
        var col = GetComponent<BoxCollider2D>();
        // Extend phase
        while (col.size.x < 1)
        {
            // Scale and offset
            col.offset += new Vector2(Time.deltaTime * AnimSpeed * transform.localScale.x / 2.0f, 0);
            col.size += new Vector2(Time.deltaTime, 0);
            yield return null;
        }
        // Shrink phase
        while (col.size.x > 0.01f)
        {
            // Scale and offset
            col.offset -= new Vector2(Time.deltaTime * AnimSpeed * transform.localScale.x / 2.0f, 0);
            col.size -= new Vector2(Time.deltaTime, 0);
            yield return null;
        }


        // End
        GameObject.Destroy(this.gameObject);
    }
}
