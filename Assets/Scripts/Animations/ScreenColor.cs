using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenColor : MonoBehaviour
{


    public static Image Img
    {
        get
        {
            return _instance._img;
        }
    }

    private static ScreenColor _instance;

    private Image _img;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (_instance)
            return;
        _instance = this;
        _img = GetComponent<Image>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
