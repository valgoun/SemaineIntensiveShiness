using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champignon : MonoBehaviour {

    public float JumpMultiplicator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<SideController>().Bump(JumpMultiplicator);
        }
    }
}
