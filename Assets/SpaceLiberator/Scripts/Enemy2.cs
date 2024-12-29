using UnityEngine;

/// <summary>
/// Child class for enemy 2 type.
/// </summary>
public class Enemy2 : Enemy
{
    // INHERITANCE

    // POLYMORPHISM
    // Sets a customized firing rate for enemy type 2
    protected override void SetFireRate()
    {
        fireRate = Random.Range(1.5f, 3.5f);
    }
}