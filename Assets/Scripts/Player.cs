using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config params
    [Header("Player Configurations")]
    [Range(0,20)][SerializeField] float speed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 100;

    [Header("Laser configurations")]
    [SerializeField] GameObject laserPrefab = null;
    [SerializeField] float laserSpeed = 1f;
    [SerializeField] float laserFireTime = 0.1f;

    Coroutine fireCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float enemyDistanceArea = 4f;

    // Start is called before the first frame update
    void Start()
    {
        setupMovementBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        shoot();
    }

    private void shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine( shootContinuously() );
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private IEnumerator shootContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            Destroy(laser, 1.5f);
            yield return new WaitForSeconds(laserFireTime);
        }
    }

    private void move()
    {
        moveHorizontally();
        moveVertically();
    }

    private void moveHorizontally()
    {
        //Multiplying the axis values (-1 to 1) by Time.deltaTime, makes the game framerate independent
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        transform.position = new Vector2(newXPosition, transform.position.y);
    }
    private void moveVertically()
    {
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(transform.position.x, newYPosition);
    }

    private void setupMovementBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding - enemyDistanceArea;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.gameObject.GetComponent<Damage>();
        if (!damage)
        {
            return;
        }
        getHit(damage);
    }
    private void getHit(Damage damage)
    {
        health -= damage.getDamage();
        damage.hit();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
