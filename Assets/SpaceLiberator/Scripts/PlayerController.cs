using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    public GameObject projectile;
    [SerializeField] float playerSpeed = 8.0f;
    float lowBoundary = 624.8f;
    float highBoundary = 641.4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gets user inputs as long as game is not over
        if (!gameManager.isGameOver)
        {
            MoveSpaceship();
            ShootProjectile();
        }
    }

    // Moves the spacecraft
    void MoveSpaceship()
    {
        float horAxis = Input.GetAxis("Horizontal");

        // Limits movement to within the left and right boundaries
        if (gameObject.transform.position.x < lowBoundary)
        {
            gameObject.transform.position = new Vector3(lowBoundary, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (gameObject.transform.position.x > highBoundary)
        {
            gameObject.transform.position = new Vector3(highBoundary, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        // Moves the spacecraft depending on the horizontal axis
        gameObject.transform.Translate(0, horAxis * playerSpeed * Time.deltaTime, 0);
    }

    // Shoots a projectile
    void ShootProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(-90, -90, 90));
        }
    }
}
