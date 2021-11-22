using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float moveSpeed;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    public int health = 150;

    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;

    public float shootRange;

    public SpriteRenderer spriteRendererBody;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRendererBody.isVisible)
        {
            if (Vector3.Distance(transform.position, PlayerController.singletonPlayerController.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.singletonPlayerController.transform.position - transform.position;
            }
            else
            {
                moveDirection = Vector3.zero;
            }
        
            moveDirection.Normalize();
            rigidbody2D.velocity = moveDirection * moveSpeed;

            if (shouldShoot && Vector3.Distance(transform.position,PlayerController.singletonPlayerController.transform.position)<shootRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                }
            }
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}