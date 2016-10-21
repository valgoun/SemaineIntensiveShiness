using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour {

	public static int collected = 0;
	public Text display;
    public AudioSource biteSound;

    void Start() {
        display.transform.parent.gameObject.SetActive(true);
    }


	void OnTriggerEnter() {
		collected++;
		display.text = ""+collected;
		display.GetComponent<ShakeText> ().Shake ();

        biteSound.pitch = 1.2f + Random.Range(-0.2f, 0.2f);                    
        biteSound.Play();

		Destroy (gameObject);
	}

}
