using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static private T instance;
    static private object _lock = new object();
    static private bool isQuitting = false;

    public static T WithInstance()
    {
        if (isQuitting) return null;

        lock(_lock)
        {
            if (instance == null)
            {
                T[] instances = (T[])FindObjectsOfType(typeof(T));

                if (instances.Length == 0)
                {
                    GameObject singleton = new GameObject();
                    singleton.name = typeof(T).ToString();
                    DontDestroyOnLoad(singleton);

                    instance = singleton.AddComponent<T>();
                }
                else
                {
                    instance = instances[0];
                }
            }

            return instance;
        }
    }

    static public void Initialize() { WithInstance(); }

    public void OnDestroy ()
    {
        isQuitting = true;
    }
}