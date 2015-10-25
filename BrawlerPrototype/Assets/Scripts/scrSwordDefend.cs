using UnityEngine;
using System.Collections;

public class scrSwordDefend : scrAction
{
    public float ActionOffset;

    private Listener actionBUpListener;

    // Use this for initialization
    protected override void OnStart()
    {
        actionBUpListener = new Listener("ActionBUp", gameObject, "ActionOver");
        Messenger.RegisterListener(actionBUpListener);
        transform.localPosition = new Vector3(ActionOffset, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ActionOver(Message m)
    {
        if (m.MessageSource == Creator)
        {
            Messenger.UnregisterListener(actionBUpListener);
            GameObject.Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Blockable" && Creator != other.gameObject)
        {
            Debug.Log("Player blocked attack.");
            // Destroy attack
            GameObject.Destroy(other.gameObject);
        }
    }
}
