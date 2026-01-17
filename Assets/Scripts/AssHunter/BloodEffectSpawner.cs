using UnityEngine;

public class BloodEffectSpawner : MonoBehaviour
{
    [SerializeField] private Sprite[] bloodEffectSprites;
    [SerializeField] private GameObject bloodEffectPrefab;

    public void Start()
    {
        int index = Random.Range(0, bloodEffectSprites.Length);
        Sprite bloodSprite = bloodEffectSprites[index];
        GetComponent<SpriteRenderer>().sprite = bloodSprite;
        
    }
}
