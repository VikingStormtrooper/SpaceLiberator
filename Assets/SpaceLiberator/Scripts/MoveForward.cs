using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 10.0f;
    float destroyY = 189.0f;

    // Update is called once per frame
    void Update()
    {
        DestroyOutsideBoundaries();
        Move();
    }

    // Moves the projectile in the forward direction
    void Move()
    {
        Vector3 dir = new Vector3(0, 0, projectileSpeed * Time.deltaTime);
        gameObject.transform.Translate(dir);
    }

    // Destroy the projectile after reaching the upper boundary
    void DestroyOutsideBoundaries()
    {
        if (gameObject.transform.position.y > destroyY)
        {
            Destroy(gameObject);
        }
    }
}
