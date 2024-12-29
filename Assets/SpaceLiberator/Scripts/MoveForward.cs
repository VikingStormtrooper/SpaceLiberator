using UnityEngine;

/// <summary>
/// Class for the forward movement of projectiles.
/// </summary>
public class MoveForward : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] float projectileSpeed = 10.0f;
    float destroyY = 5.0f;
    [SerializeField] GameObject explosion;
    ParticleSystem explosionFx;
    BoxCollider projectileBoxCollider;
    AudioSource explosionSound;
    [SerializeField] AudioClip explosionSoundFx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        explosionFx = explosion.GetComponent<ParticleSystem>();
        projectileBoxCollider = GetComponent<BoxCollider>();
        explosionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOutsideBoundaries();
        Move();
    }

    // ABSTRACTION
    // Moves the projectile in the forward direction
    void Move()
    {
        Vector3 dir = new Vector3(0, 0, projectileSpeed * Time.deltaTime);
        gameObject.transform.Translate(dir);
    }

    // ABSTRACTION
    // Destroys the projectile after reaching the upper boundary
    void DestroyOutsideBoundaries()
    {
        if (gameObject.transform.position.y > destroyY)
        {
            Destroy(gameObject);
        }
    }

    // Destroys enemies on collision, plays the particle effect, updates GameManager
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            explosionFx.Play();                             // Plays the particle effect
            explosionSound.PlayOneShot(explosionSoundFx);   // Plays the explosion sound
            Destroy(other.gameObject);                      // Destroys the enemy
            projectileBoxCollider.enabled = false;          // Prevents projectile from hitting a second target
        }
    }
}