using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class ShakeText : MonoBehaviour {

	float shakeTime = 0.2f;
	float shakeIntensity = 20;
	int fontBig = 80;
	int fontSize;
	Text disp;

	void Start() {
		disp = GetComponent<Text> ();
		fontSize = disp.fontSize;
	}

	public void Shake() {
		StartCoroutine(_Shake());
	}

	IEnumerator _Shake() {
		for (float e = 0; e < shakeTime; e += Time.deltaTime) {
			float percent = e / shakeTime;

			Vector3 rot = Vector3.forward * Random.Range (-shakeIntensity, shakeIntensity);
			disp.rectTransform.rotation = Quaternion.Euler (rot);
			disp.fontSize = Random.Range (fontSize, fontBig);
			yield return null;
		}
		disp.fontSize = fontSize;
		disp.rectTransform.rotation = Quaternion.identity;
	}

}