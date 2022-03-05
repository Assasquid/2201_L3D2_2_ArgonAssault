using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int hitPoints;
    [SerializeField] int pointValue;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        /*If script added at the parent level:
        Rigidbody rigidbody gameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
        This allows to handle collisions at the parent level,
        even if collider is at the child level.*/
        parentGameObject = GameObject.FindWithTag("EffectsParent");
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        
        if(hitPoints < 1)
        {
            scoreBoard.IncreaseScore(pointValue);
            DestroyEnemy();
        }
    }

    void ProcessHit()
    {
        ReduceHealth();
    }

    void ReduceHealth()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;

        hitPoints--;
        Debug.Log(hitPoints);
    }

    void DestroyEnemy()
    {
        GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;

        Destroy(gameObject);
    }
}
