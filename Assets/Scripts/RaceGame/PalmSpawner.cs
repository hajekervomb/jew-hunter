using UnityEngine;

public class PalmSpawner : MonoBehaviour
{
    [SerializeField] private GameObject palmPrefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private Transform spawnPoint;

    void Update()
    {
        if (spawnInterval <= 0f)
        {
            SpawnPalm();
            spawnInterval = 3f; // Reset the interval
        }
        else
        {
            spawnInterval -= Time.deltaTime;
        }
    }

    private void SpawnPalm()
    {
        Instantiate(palmPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
    } 
        
}
