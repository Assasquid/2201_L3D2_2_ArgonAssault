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
