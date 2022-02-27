using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [SerializeField] int hitPoints;
    [SerializeField] int pointValue;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        /*If script added at the parent level:
        Rigidbody rigidbody gameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
        This allows to handle collisions at the parent level,
        even if collider is at the child level.*/
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        
        if(hitPoints < 1)
        {
            DestroyEnemy();
        }
    }

    void ProcessHit()
    {
        scoreBoard.IncreaseScore(pointValue);
        ReduceHealth();
    }

    void ReduceHealth()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;

        hitPoints--;
        Debug.Log(hitPoints);
    }

    void DestroyEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;

        Destroy(gameObject);
    }
}
