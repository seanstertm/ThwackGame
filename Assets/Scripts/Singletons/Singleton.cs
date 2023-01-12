using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    private static T mainReference;

    public static T Main
    {
        get
        {
            return mainReference;
        }
        private set { }
    }

    public void Start()
    {
        mainReference = gameObject.GetComponent<T>();
    }
}
