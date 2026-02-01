using UnityEngine;

public class PlayerShotAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float stepDuration = 0.1f;
    [SerializeField] private Sprite[] shotSprites;

    void Start()
    {
        StartCoroutine(ShotAnimationCoroutine());
    }    

    private System.Collections.IEnumerator ShotAnimationCoroutine()
    {
        for (int i = 0; i < shotSprites.Length; i++)
        {
            spriteRenderer.sprite = shotSprites[i];
            yield return new WaitForSeconds(stepDuration);
        }

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
