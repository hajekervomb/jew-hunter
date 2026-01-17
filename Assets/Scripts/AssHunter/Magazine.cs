using UnityEngine;

namespace AssHunter
{
    public class Magazine : MonoBehaviour
    {
        // Magazine implementation goes here
        [SerializeField] private int itemCount = 1;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                SoundManager.Instance.PlayPickupSound();
                WeaponController.Instance.AddMagazine();
                Destroy(gameObject);
            }
        }
    }
}

