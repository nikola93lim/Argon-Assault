using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionSoundParticle;
    [SerializeField] GameObject damageParticle;
    GameObject parentGameObject;

    [SerializeField] int points;
    [SerializeField] int healthPoints;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        AddRigidbody();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if(healthPoints < 1)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        GameObject damageVfx = Instantiate(damageParticle, transform.position, Quaternion.identity);
        damageVfx.transform.parent = parentGameObject.transform;
        healthPoints--;
        scoreBoard.UpdateScore(points);
    }

    void KillEnemy()
    {
        GameObject fx = Instantiate(explosionSoundParticle, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        Destroy(this.gameObject);
    }
}
