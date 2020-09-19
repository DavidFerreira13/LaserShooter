using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter = 0f;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laser = null;
    [SerializeField] float laserSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots,maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer();
    }

    private void shootTimer()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0)
        {
            shoot();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
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
    private void shoot()
    {
        GameObject laserShot = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
        laserShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }
}
