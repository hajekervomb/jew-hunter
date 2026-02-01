using System;
using Unity.VisualScripting;
using UnityEngine;

namespace AssHunter
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        //префабы стен и дверей
        [SerializeField] private GameObject wallContainer;
        [SerializeField] private GameObject wallHorizontalPrefab;
        [SerializeField] private GameObject wallVerticalPrefab;
        [SerializeField] private GameObject doorHorizontalPrefab;
        [SerializeField] private GameObject doorVerticalPrefab;

        [SerializeField] private Vector3 leftWallPosition = new Vector3(-8.5f, 0f, 0f);
        [SerializeField] private Vector3 rightWallPosition = new Vector3(8.5f, 0f, 0f);
        [SerializeField] private Vector3 topWallPosition = new Vector3(0f, 5f, 0f);
        [SerializeField] private Vector3 bottomWallPosition = new Vector3(0f, -5f, 0f);
        private Vector3[] wallPositions = new Vector3[3];
        private GameObject[] walls = new GameObject[4];

        private int wallCount = 0;

        [SerializeField] private GameObject player;
        [SerializeField] private Transform playerSpawnPoint; 

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            wallPositions[0] = topWallPosition;
            wallPositions[1] = rightWallPosition;
            wallPositions[2] = bottomWallPosition;

            LoadNextLevel();            
        }

        public void LoadNextLevel()
        {
            // Логика загрузки следующего уровня
            Debug.Log("Loading next level...");
            // Например, можно использовать SceneManager.LoadScene для загрузки следующей сцены
            wallCount = 0;

            for (int i = 0; i < walls.Length; i++)
            {
                if (walls[i] != null)
                {
                    Destroy(walls[i]);
                }
            }
            Array.Clear(walls, 0, walls.Length);
            CreateLevelWalls();

            SpawnPlayer();
            SpawnLevelObject();
            SpawnEnemies();
            SpawnAmmoPickups();

            GameManager.Instance.AddPlayerHealth();
        }

        private void SpawnPlayer()
        {
            player.transform.position = playerSpawnPoint.position;
        }

        private void SpawnLevelObject()
        {
            // Логика спавна объектов уровня
        }

        private void SpawnEnemies()
        {
            // Логика спавна врагов
        }

        private void SpawnAmmoPickups()
        {
            // Логика спавна патронов
        }

        public void CreateLevelWalls()
        {
            // Логика создания стен на уровне
            Debug.Log("Creating walls...");
            // Реализуйте здесь создание стен
            //лево = всегда стена
            GameObject leftWall = Instantiate(wallVerticalPrefab, leftWallPosition, Quaternion.identity, wallContainer.transform);
            walls[0] = leftWall;
            
            IncrementWallCount();
            //вверх = стена или дверь (50% шанс)
            GameObject topWall = CreateRandomWall(wallHorizontalPrefab, doorHorizontalPrefab, topWallPosition);
            walls[1] = topWall;
            //право = стена или дверь (50% шанс)
            GameObject rightWall = CreateRandomWall(wallVerticalPrefab, doorVerticalPrefab, rightWallPosition);
            walls[2] = rightWall;
            //вниз = стена или дверь (50% шанс)
            GameObject bottomWall = CreateRandomWall(wallHorizontalPrefab, doorHorizontalPrefab, bottomWallPosition);
            walls[3] = bottomWall;

            //если стаен больше 3, то создать 1 дверь минимум
            if (wallCount > 3)
            {
                Debug.Log("More than 3 walls, ensuring at least one door.");
                //логика обеспечения наличия двери
                //например, можно заменить одну из стен на дверь
                int randIndex = UnityEngine.Random.Range(0, 3);
                Vector3 positionToReplace = wallPositions[randIndex];
                //заменяем стену на дверь
                switch (randIndex)
                {
                    case 0: //top
                        Destroy(topWall);
                        Instantiate(doorHorizontalPrefab, positionToReplace, Quaternion.identity, wallContainer.transform);
                        break;
                    case 1: //right
                        Destroy(rightWall);
                        Instantiate(doorVerticalPrefab, positionToReplace, Quaternion.identity, wallContainer.transform);
                        break;
                    case 2: //bottom
                        Destroy(bottomWall);
                        Instantiate(doorHorizontalPrefab, positionToReplace, Quaternion.identity, wallContainer.transform);
                        break;

                }
            }

        }

        private GameObject CreateRandomWall(GameObject wallPrefab, GameObject doorPrefab, Vector3 position)
        {
            float rand = UnityEngine.Random.Range(0f, 1f);

            if (rand < 0.5f)
            {   
                IncrementWallCount();
                return Instantiate(wallPrefab, position, Quaternion.identity, wallContainer.transform);
                
            }
            else
            {
                return Instantiate(doorPrefab, position, Quaternion.identity, wallContainer.transform);
            }
        }            

        private void IncrementWallCount()
        {
            wallCount++;    
        }
    }
}

