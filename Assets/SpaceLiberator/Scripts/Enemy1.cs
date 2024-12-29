using UnityEngine;

/// <summary>
/// Child class for enemy 1 type.
/// </summary>
public class Enemy1 : Enemy
{
    // INHERITANCE

    // POLYMORPHISM
    // Sets a customized firing rate for enemy type 1
    protected override void SetFireRate()
    {
        fireRate = Random.Range(0.5f, 2.5f);
    }
}