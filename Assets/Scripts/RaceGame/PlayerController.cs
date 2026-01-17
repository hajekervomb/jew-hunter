    using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject posLeft;
    [SerializeField] private GameObject posRight; 
    [SerializeField] private GameObject playerObject; 

    private void Start()
    {
        playerObject.transform.position = posLeft.transform.position;
    } 

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.PlayerLives > 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                playerObject.transform.position = posLeft.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                playerObject.transform.position = posRight.transform.position;
            }
        }
        
    }
}
