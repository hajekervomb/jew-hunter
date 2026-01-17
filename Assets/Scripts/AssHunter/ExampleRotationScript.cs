using UnityEngine;

public class ExampleRotationScript : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.rotation -= 1f; // Rotate the object by 1 degree every physics frame
    }
}
