using UnityEngine;
using System.Collections;

public abstract class scrObservable : MonoBehaviour
{
    public scrObserver Obs { get; set; }

    public void NotifyObs(scrObservable other)
    {
        Obs.Notify(this, other);
    }
    public void NotifyObs()
    {
        Obs.Notify(this);
    }
}
