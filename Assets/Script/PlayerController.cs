using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singletonPlayerController;

    public float moveSpeed;
    private Vector2 moveInput;
    public Transform gun;
    public Rigidbody2D rigidbody2D;

    private Camera camera;

    public Animator animator;

    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots;
    private float shotCounter;

    public Transform enemiesContainer;
    public List<GameObject> enemiesList;

    private void Awake()
    {
        singletonPlayerController = this;
    }

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //transform.position += (Vector3)new Vector2(moveInput.x, moveInput.y);
        //*Time.deltaTime : Update함수 호출 주기가 컴퓨터의 성능,게임의 최적화 정도 등등에 따라서 달라지기때문에 평균화 작업함
        //transform.position += new Vector3(moveInput.x*Time.deltaTime*moveSpeed, moveInput.y*Time.deltaTime*moveSpeed,0f);

        moveInput.Normalize(); //Normalize에 대해서 다시 찾아보기

        rigidbody2D.velocity = moveInput * moveSpeed;

        // Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePosition = GetEnemy();
        Vector3 screenPoint = camera.WorldToScreenPoint(transform.localPosition);
        // Vector3 screenPoint = GetEnemy();
        
        // if (mousePosition.x < screenPoint.x)
        // {
        //     transform.localScale = new Vector3(-1f, 1f, 1f);
        //     gun.localScale = new Vector3(-1f, -1f, 1f);
        // }
        // else
        // {
        //     transform.localScale = Vector3.one;
        //     gun.localScale = Vector3.one;
        // }

        //rotate gun
        // Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        Vector2 offset = mousePosition - transform.position;
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        Debug.Log("angle : " + angle);
        gun.rotation = Quaternion.Euler(0, 0, angle);


        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots;
        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }


        if (moveInput != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        
    }

    Vector2 GetEnemy()
    {
        //enemiesList = new List<GameObject>();
        enemiesList.Clear();
        
        for (int i = 0; i < enemiesContainer.childCount; i++)
        {
            var _enemy = enemiesContainer.GetChild(i).gameObject;
            enemiesList.Add(_enemy);
        }
        enemiesList = enemiesList.OrderByDescending(r =>
            Vector2.Distance(gameObject.transform.position, r.transform.position)).ToList();
        // for (int i = 0; i < enemiesList.Count; i++)
        // {
        //     var _enemyListElement = enemiesList[i];
        //     enemiesList.OrderByDescending(r =>
        //         Vector2.Distance(gameObject.transform.position, r.transform.position));
        // }

        var getEnemy = enemiesList[enemiesList.Count-1];
        
        // Debug.Log(getEnemy.name);

        return getEnemy.transform.position;
    }
}