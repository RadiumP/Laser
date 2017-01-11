using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos()//show in the things u want in the editor
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

}
