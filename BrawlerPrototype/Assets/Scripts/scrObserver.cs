using UnityEngine;
using System.Collections;

public abstract class scrObserver : MonoBehaviour
{

    public abstract void Notify(scrObservable mine, scrObservable other);
    public abstract void Notify(scrObservable mine);

}
