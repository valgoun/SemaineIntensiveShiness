using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour {

	public static int collected = 0;
	public Text display;

    void Start() {
        display.transform.parent.gameObject.SetActive(true);
    }

	void OnTriggerEnter() {
		collected++;
		display.text = ""+collected;
		display.GetComponent<ShakeText> ().Shake ();
		Destroy (gameObject);
	}

}
