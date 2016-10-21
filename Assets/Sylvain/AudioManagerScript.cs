using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour {

    public Transform playerCamera;
    public float Xoffset;
    public float YoffsetRange;
    public float ZoffsetRange;

    void Start () {
        Translate();
    }
	

	void Update () {
		if (Mathf.Abs(transform.position.x - playerCamera.position.x) > Xoffset)
            Translate();
	}

    void Translate() {
        transform.position = new Vector3(playerCamera.position.x + Xoffset,
                                         playerCamera.position.y + Random.Range(-YoffsetRange, YoffsetRange),
                                         playerCamera.position.z + Random.Range(-ZoffsetRange, ZoffsetRange));
    }
}
