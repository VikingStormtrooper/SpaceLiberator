using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

/// <summary>
/// GameManager used to manage score, spawning and game events.
/// </summary>
public class GameManager : MonoBehaviour
{
    public bool isGameOver { get; private set; }                    // ENCAPSULATION
    public int enemiesToSpawn = 10;
    public EnemyType enemiesType = EnemyType.AllTypes;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject successUI;
    AudioSource victorySound;
    [SerializeField] AudioClip victorySoundFx;
    GameObject player;
    [SerializeField] GameObject[] enemies;
    float lowXBoundary = -7.9f;
    float highXBoundary = 8.6f;
    float lowYBoundary = 1.3f;
    float highYBoundary = 3.0f;

    // Types of enemies to spawn
    public enum EnemyType : int
    {
        AllTypes = 0,
        Type1 = 1,
        Type2 = 2,
        Type3 = 3,
        Type1And2 = 4,
        Type2And3 = 5,
        Type1And3 = 6,
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        victorySound = GetComponent<AudioSource>();
        SpawnEnemies(enemiesType, enemiesToSpawn);
    }

    void Update()
    {
        CheckGameOver();
        BackToMenu();
    }

    // ABSTRACTION
    // POLYMORPHISM
    // Spawns the desired amount of enemies. If the enemyType parameter is omitted, all types of enemies are spawned
    void SpawnEnemies(EnemyType enemyType, int amount)
    {
        int enemiesAmount;
        Vector3[] spawnSlots;

        enemiesAmount = LimitEnemiesToSpawn(amount);
        spawnSlots = AllocateSpawnPositions();
        SpawnInRandomPositions(enemiesAmount, spawnSlots);
    }
    void SpawnEnemies(int amount)
    {
        SpawnEnemies(EnemyType.AllTypes, amount);
    }

    // ABSTRACTION
    // Checks the given amount of enemies and possibly corrects it. Available number is fixed btw 1 <= n <= 20.
    int LimitEnemiesToSpawn(int amount)
    {
        int enemiesAmount = amount;

        if (enemiesAmount > 20)
        {
            enemiesAmount = 20;
        }
        else if (enemiesAmount < 1)
        {
            enemiesAmount = 1;
        }
        return enemiesAmount;
    }

    // ABSTRACTION
    // Allocates the array of available spawn positions
    Vector3[] AllocateSpawnPositions()
    {
        float zPos = player.transform.position.z;
        float xPos;
        Vector3[] spawnSlots = new Vector3[20];

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                xPos = lowXBoundary + i * (highXBoundary - lowXBoundary) / 10;
                if (j == 0)
                {
                    spawnSlots[i + 10 * j] = new Vector3(xPos, highYBoundary, zPos);
                }
                else
                {
                    spawnSlots[i + 10 * j] = new Vector3(xPos, lowYBoundary, zPos);
                }
            }
        }
        return spawnSlots;
    }

    // ABSTRACTION
    // Generates enemies in random positions in the 20 spawn slots
    void SpawnInRandomPositions(int amount, Vector3[] spawnSlots)
    {
        int randSlot;
        bool[] isSlotOccupied = new bool[20];
        GameObject enToSpawn;
        Vector3 placeToSpawn;
        for (int k = 0; k < amount; k++)
        {
            enToSpawn = SpawnRandomEnemyType(enemiesType);
            do
            {
                randSlot = Random.Range(0, 20);
            } while (isSlotOccupied[randSlot] == true);
            placeToSpawn = new Vector3(spawnSlots[randSlot].x, spawnSlots[randSlot].y, player.transform.position.z);
            Instantiate(enToSpawn, placeToSpawn, enToSpawn.transform.rotation);
            isSlotOccupied[randSlot] = true;
        }
    }

    // ABSTRACTION
    // Returns a random enemy type among the desired ones
    GameObject SpawnRandomEnemyType(EnemyType enemyType)
    {
        GameObject res;
        int typeNum;

        // Prevents the selection of a number of enemy GameObjects != 3
        if (enemies.Length != 3)
        {
            Debug.Log("Invalid number of enemy GameObjects. Valid number of enemy GameObjects = 3.");
            return null;
        }

        // Generates a random enemy type ID among the valid ones
        switch (enemyType)
        {
            case EnemyType.Type1:                           // 1
                typeNum = 1;
                break;
            case EnemyType.Type2:                           // 2
                typeNum = 2;
                break;
            case EnemyType.Type3:                           // 3
                typeNum = 3;
                break;
            case EnemyType.Type1And2:                       // 1 or 2
                typeNum = Random.Range(1, 3);               
                break;
            case EnemyType.Type2And3:                       // 2 or 3
                typeNum = Random.Range(2, 4);               
                break;
            case EnemyType.Type1And3:                       // 1 or 3
                typeNum = Random.Range(1, 3);
                if (typeNum == 2)
                {
                    typeNum = 3;
                }
                break;
            default:                                        // 1, 2 or 3
                typeNum = Random.Range(1, 4);               
                break;
        }

        // Returns the correct GameObject for the given enemy type ID
        res = enemies[typeNum - 1];
        return res;
    }

    // ABSTRACTION
    // Checks if the game is still running, opens UI if it is not
    void CheckGameOver()
    {
        if (player == null)                                                             // Player dead
        {
            isGameOver = true;
            gameOverUI.SetActive(true);
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)                     // Enemies dead
        {
            successUI.SetActive(true);
            victorySound.PlayOneShot(victorySoundFx, 0.5f);
            player.transform.Translate(10.0f * Time.deltaTime, 0, 0);
        }
    }
    
    // ABSTRACTION
    // Reloads the initial menu after pressing the Escape key
    void BackToMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    // ABSTRACTION
    // Restarts game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}