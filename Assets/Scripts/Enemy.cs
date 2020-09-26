using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]
    [SerializeField] float shotCounter = 0f;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laser = null;
    [SerializeField] float laserSpeed = 10f;

    [Header("Death Effects")]
    [SerializeField] GameObject deathAnimation = null;
    [SerializeField] float deathTimer = 1f;
    [SerializeField] AudioClip deathSound = null;
    [Range(0f, 1f)] [SerializeField] float deathVolume = 0.2f;
    [SerializeField] AudioClip shootSound = null;
    [Range(0f, 1f)] [SerializeField] float shootVolume = 0.2f;


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
        checkDeath();
    }

    private void checkDeath()
    {
        if (health <= 0)
        {
            FindObjectOfType<GameSession>().addToScore(scoreValue);
            Destroy(gameObject);
            GameObject death = Instantiate(deathAnimation, transform.position, transform.rotation);
            Destroy(death, deathTimer);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
        }
    }

    private void shoot()
    {
        GameObject laserShot = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
        laserShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
        Destroy(laserShot, 1.5f);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);

    }
}
