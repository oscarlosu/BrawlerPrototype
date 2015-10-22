using UnityEngine;
using System.Collections;
using System;

public class scrSlave : scrObserver
{
    // Attributes
    public int damage;
    public int defence;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
