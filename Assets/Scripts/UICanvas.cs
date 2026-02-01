using UnityEngine;

public class UICanvas : MonoBehaviour
{
    public static UICanvas Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
