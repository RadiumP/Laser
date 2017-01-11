using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public float hitpoint = 300f;
    public GameObject projetile;
    public float projetileSpeed;
    
    public int scoreValue = 150;
    public AudioClip laser;
    public AudioClip die;

    //private float firingRate;
    public float shotsPS;

    private ScoreKeeper scoreKeeper;
    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();

    }

    void Update()
    {

        float probability = Time.deltaTime * shotsPS;
        if(Random.value < probability)
        {
            Fire();
        }
        //InvokeRepeating("Fire", 0.00001f, firingRate);
        Debug.Log(shotsPS);
        Debug.Log(projetileSpeed);
    }

    void Fire()
    {
        //enemy fire
        Vector3 starPosition = transform.position + new Vector3(0, -0.2f, 0);
        GameObject bullet = Instantiate(projetile, starPosition, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projetileSpeed, 0);
        AudioSource.PlayClipAtPoint(laser, transform.position, 1.0f);
    }
    //edit--projsetting--phys 2d--collison matrix
    void OnTriggerEnter2D(Collider2D collider)
    {

      
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if(missile)//only trigger when buller hits
        {
            hitpoint -= missile.GetDamage();
            missile.Hit();
            //Debug.Log("Hit");
            if(hitpoint <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
        AudioSource.PlayClipAtPoint(die, transform.position, 1.0f);
    }
}
