using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuGameover : MonoBehaviour
{

    public GameObject deathMenu, winMenu, defaultButtonSelected, defaultButtonSelectedWin;

    void Start()
    {
        SelectDefaultButton();
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (Input.GetAxis("Vertical") != 0 && !EventSystem.current.currentSelectedGameObject)
                SelectDefaultButton();
        }
    }

    void SelectDefaultButton()
    {
        EventSystem.current.SetSelectedGameObject(defaultButtonSelected);
        defaultButtonSelected.GetComponent<Button>().Select();
    }

    public void SelectDefaultButtonWin()
    {
        defaultButtonSelected = defaultButtonSelectedWin;
        EventSystem.current.SetSelectedGameObject(defaultButtonSelectedWin);
        defaultButtonSelectedWin.GetComponent<Button>().Select();
    }

    public void AppearDeath()
    {
        deathMenu.SetActive(true);
    }

    public void AppearWin()
    {
        winMenu.SetActive(true);
    }

    public void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }


}
