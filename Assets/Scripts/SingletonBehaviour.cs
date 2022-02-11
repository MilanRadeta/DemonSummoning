using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T: MonoBehaviour
{
    public static T Instance {get; private set;}

    public SingletonBehaviour()
    {
        Instance = this as T;
    }

}
