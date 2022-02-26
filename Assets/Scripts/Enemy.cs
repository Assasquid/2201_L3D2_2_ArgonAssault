using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [SerializeField] int pointValue;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        DestroyEnemy();
    }

    void ProcessHit()
    {
        scoreBoard.IncreaseScore(pointValue);
    }

    void DestroyEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;

        Destroy(gameObject);
    }
}
