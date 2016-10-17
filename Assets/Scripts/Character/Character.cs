using UnityEngine;
using System.Collections;
using System;

public class Character : MonoBehaviour
{
    private Controller _activeController, _topController, _sideController;
    private Action _onDown, _onUp;
    // Use this for initialization
    void Start()
    {
        _topController = GetComponent<TopController>();
        _topController.enabled = false;
        _sideController = GetComponent<SideController>();
        _sideController.enabled = false;


        setActiveController(_topController);
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Vertical");

        if (input < 0.0)
            _onDown();
        else if (input > 0.0)
            _onUp();
    }

    void setActiveController(Controller controller)
    {
        _activeController = controller;
        _onUp = controller.OnUp;
        _onDown = controller.OnDown;
    }

}
