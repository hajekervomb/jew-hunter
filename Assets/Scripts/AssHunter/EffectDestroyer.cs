using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{
    void OnEnable()
    {
        Destroy(gameObject, 5f);
    }
}
