using UnityEngine;

/// <summary>
/// Manages the actions of the player spacecraft.
/// </summary>
public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject projectile;
    private float m_playerSpeed = 8.0f;
    public float playerSpeed                                // ENCAPSULATION
    {
        get { return m_playerSpeed; }
        set 
        {
            if (value < 0)
            {
                value = 0;
            }
            else
            {
                m_playerSpeed = value;
            }
        }
    }
    private float lowBoundary = -7.9f;
    private float highBoundary = 8.6f;
    private AudioSource shotSound;
    public AudioClip shotSoundFx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shotSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gets user inputs as long as the game is not over, plays victory animation with success
        if (!gameManager.isGameOver)
        {
            MoveSpaceship();
            ShootProjectile();
        }
    }

    // ABSTRACTION
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
        gameObject.transform.Translate(0, horAxis * m_playerSpeed * Time.deltaTime, 0);
    }

    // ABSTRACTION
    // Shoots a projectile
    void ShootProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(-90, -90, 90));
            shotSound.PlayOneShot(shotSoundFx);
        }
    }
}
