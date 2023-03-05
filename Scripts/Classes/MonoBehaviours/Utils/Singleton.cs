using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T pInstance;
    public static T instance
    {
        get
        {
            if (pInstance == null)
            {
                pInstance = FindObjectOfType<T>();
            }
            else if (pInstance != FindObjectOfType<T>())
            {
                Destroy(FindObjectOfType<T>());
            }
            DontDestroyOnLoad(pInstance.gameObject);
            return pInstance;
        }
        
    }
}
