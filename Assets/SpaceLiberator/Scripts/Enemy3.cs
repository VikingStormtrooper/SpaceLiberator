using UnityEngine;

/// <summary>
/// Child class for enemy 3 type.
/// </summary>
public class Enemy3 : Enemy
{
    // INHERITANCE

    float rotationSpeed = 15.0f;
    Vector3 initialPosition;

    // POLYMORPHISM
    protected override void Start()
    {
        base.Start();
        initialPosition = transform.position;
    }

    private void LateUpdate()
    {
        RotateAroundAxis();
    }

    // ABSTRACTION
    // Adds a rotational motion
    void RotateAroundAxis()
    {
        float rotationIncrement = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right, rotationIncrement);
        transform.position = new Vector3(transform.position.x, initialPosition.y, initialPosition.z);
    }

    // POLYMORPHISM
    // Sets a customized firing rate for enemy type 3
    protected override void SetFireRate()
    {
        fireRate = Random.Range(2.5f, 4.5f);
    }
}