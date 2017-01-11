using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float padding;
    public GameObject projetile;
    public float projetileSpeed;
    public float firingRate;
    public ParticleSystem engine;
    public AudioClip laser;

    private float hitpoint = 500f;
    private float xmin;
    private float xmax;
    

    // Use this for initialization
    void Start()
    {

        float distance = transform.position.z - Camera.main.transform.position.z;//also for 3D
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));//(0,0) (1,0) (1,1),(0,1)
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }

    // Update is called once per frame
    void Update()
    {
        //repeat fire rate
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00001f, firingRate); //not 0 to aviod multi-invoke bug
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }



        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position -=new Vector3(speed * Time.deltaTime, 0f, 0f);//deltaTime: framerate independent speed
            transform.position += Vector3.left * speed * Time.deltaTime; //deltaTime: framerate independent speed
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        //restrict the player
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        engine.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
    }

    void Fire()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        GameObject bullet = Instantiate(projetile, transform.position + offset, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projetileSpeed, 0);
        AudioSource.PlayClipAtPoint(laser, transform.position, 1.0f);
    }

    void Die()
    {
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        man.LoadNewLevel("Win");
        Destroy(gameObject);
        Destroy(engine);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {


        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)//only trigger when buller hits
        {
            hitpoint -= missile.GetDamage();
            missile.Hit();
            //Debug.Log("Hit");
            if (hitpoint <= 0)
            {
                Die();
            }
        }
    }

}
