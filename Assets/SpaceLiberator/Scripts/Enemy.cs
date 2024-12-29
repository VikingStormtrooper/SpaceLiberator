using System.Collections;
using UnityEngine;

/// <summary>
/// Parent class for the enemy spacecrafts.
/// </summary>
public class Enemy : MonoBehaviour
{
    GameManager gameManager;
    PlayerController playerController;
    protected float fireRate;
    public GameObject projectile;
    AudioSource shotSound;
    protected AudioClip shotSoundFx;
    Vector3 initPos;
    float oscillationAmplitude = 0.1f;
    float oscillationRate;
    bool oscillationDirection = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual protected void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        shotSound = gameObject.AddComponent<AudioSource>();
        shotSoundFx = playerController.shotSoundFx;
        initPos = gameObject.transform.position;

        SetFireRate();
        StartCoroutine(RandomFire());
    }

    virtual protected void Update()
    {
        Oscillate();
    }

    // ABSTRACTION
    // Makes spacecrafts oscillate around the initial position
    virtual protected void Oscillate()
    {
        oscillationRate = Random.Range(0.1f, 0.3f);

        // Direction of motion
        if (oscillationDirection == false)
        {
            gameObject.transform.Translate(new Vector3(0, -oscillationRate * Time.deltaTime, 0));
        }
        else
        {
            gameObject.transform.Translate(new Vector3(0, oscillationRate * Time.deltaTime, 0));
        }

        // Left boundary check
        if (gameObject.transform.position.x < initPos.x - oscillationAmplitude / 2)
        {
            oscillationDirection = true;
        }

        // Right boundary check
        if (gameObject.transform.position.x > initPos.x + oscillationAmplitude / 2)
        {
            oscillationDirection = false;
        }
    }

    // ABSTRACTION
    // Establishes the fire rate of the enemy spaceship
    virtual protected void SetFireRate()
    {
        fireRate = Random.Range(1.5f, 3.5f);
    }

    // ABSTRACTION
    // Makes the enemy spaceships open fire
    virtual protected void OpenFire()
    {
        Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(-90, -90, 90));
        shotSound.PlayOneShot(shotSoundFx);
    }

    IEnumerator RandomFire()
    {
        while (!gameManager.isGameOver)
        {
            yield return new WaitForSeconds(fireRate);
            OpenFire();
        }
    }
}