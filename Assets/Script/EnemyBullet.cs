using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;

    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.singletonPlayerController.transform.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
        //rigid할 경우에 너무 많은 bullet에 대한 물리 정보를 체크해야하므로, 캐릭터와 주변 벽에 rigid가 있어서 안날라갈꺼기 때문에 괜찮다.
        //rigid로 할경우 다양한 bullet이 가능하므로 변경
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.singletonPlayerHealthController.DamagePlayer();
        }
        
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
