using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleScreen : MonoBehaviour {

    [SerializeField]
    GameObject defaultButtonSelected, credits;

    [SerializeField]
    AnimationCurve animCredits;

    float creditsTime = 1f;
    bool hasCredits;

    #region PrivateMethods

    void Start() {
        SelectDefaultButton();
        credits.SetActive(false);
    }

    void Update() {
        if (hasCredits && Input.GetButtonDown("Cancel")) {
            hasCredits = false;
            credits.SetActive(false);
        }

        if (EventSystem.current.currentSelectedGameObject == null) {
            if (Input.GetAxis("Vertical") != 0 && !EventSystem.current.currentSelectedGameObject)
                SelectDefaultButton();
        }
    }

    void SelectDefaultButton() {
        EventSystem.current.SetSelectedGameObject(defaultButtonSelected);
    }

    #endregion

    public void LoadScene(int sceneToLoad) {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadScene(string sceneToLoad) {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void Credits() {
        credits.SetActive(true);
        hasCredits = true;
        StartCoroutine(_Credits());
    }

    float creditStart = -250, creditsEnd = -550;

    IEnumerator _Credits() {
        var cred = credits.GetComponent<RectTransform>();
        Vector3 pos = cred.anchoredPosition;

        for(float e = 0; e < creditsTime; e+=Time.deltaTime) {
            float t = e / creditsTime;

            pos.x = Mathf.Lerp(creditStart, creditsEnd, animCredits.Evaluate(t));
            cred.anchoredPosition = pos;

            yield return null;
        }
    }
}
