using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float padding;
    private float speed;
    public float spawnDelay = 0.5f;

    private float xmin;
    private float xmax;
    private bool moveLt = true;

    // Use this for initialization
    void Start()
    {
        speed = 5.0f;
        SpawnUntilFull();
        float distance = transform.position.z - Camera.main.transform.position.z;//also for 3D
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));//(0,0) (1,0) (1,1),(0,1)
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;


    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (transform.position.x <= xmin || transform.position.x >= xmax)//can be stuck if formation always outside edge
        //{
        //    moveLt = !moveLt;
        //}

        if(transform.position.x <= xmin)
        {
            moveLt = false;
        }

        if (transform.position.x >= xmax)
        {
            moveLt = true;
        }

        if (moveLt)
        {
            transform.position += Vector3.left * speed * Time.deltaTime; //deltaTime: framerate independent speed       
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime; //deltaTime: framerate independent speed
        }

        if (AllMembersDead())
        {
            speed = Random.Range(5.0f, 8.0f);
            SpawnUntilFull();
            
        }


    }

    //void SpawnEnemies()
    //{
    //    foreach (Transform child in transform)//positions
    //    {
    //        GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
    //        enemy.transform.parent = child;
    //    }
    //}

    //recursion
    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
            enemy.GetComponentInChildren<EnemyAction>().projetileSpeed = Random.Range(4.0f, 8.0f);
            enemy.GetComponentInChildren<EnemyAction>().shotsPS = Random.Range(0.2f, 0.5f);
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);//every delay call respawn
        }
        
    }

   bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)//formation's child enemy
        {
            if(childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)//formation's child enemy
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }
}
