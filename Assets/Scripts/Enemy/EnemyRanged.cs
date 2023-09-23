using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;
    public GameObject bulletPrefab;

    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;

    public float fireRate;
    public float timeToFire;

    public Transform firingPoint;

    [SerializeField] private AudioSource arrowShoot;
    [SerializeField] private AudioSource enemyDeath;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeToFire = fireRate;
    }

    private void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        else
        {
            RotateTowardsTarget();
        }
        
        if(target != null && Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            arrowShoot.Play();
            Shoot();
        }
    }

    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (target != null) 
        { 
            if (Vector2.Distance(target.position, transform.position) >= distanceToStop)
            {
                rb.velocity = transform.up * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player")) 
        { 
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            LevelManager.manager.IncreaseScore(1);
            Destroy(other.gameObject);
            Destroy(gameObject);
            enemyDeath.Play();
        }
    }
}
