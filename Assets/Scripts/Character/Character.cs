using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class Character : MonoBehaviour
{
    private Controller _activeController, _topController, _sideController;
    private Action _onBot, _onTop, _onBotDown, _onTopDown, _onNoInput;
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
        bool inputEvent = false;
        //Inputs
        if (Input.GetKeyDown(KeyCode.UpArrow))
            _onTopDown();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            _onBotDown();

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _onBot();
            inputEvent = true;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _onTop();
            inputEvent = true;
        }
        if (!inputEvent)
            _onNoInput();

    }

    void setActiveController(Controller controller)
    {
        if (_activeController)
            _activeController.enabled = false;
        _activeController = controller;
        _activeController.enabled = true;

        //Assign delegates
        _onTop = controller.OnTop;
        _onBot = controller.OnBot;
        _onTopDown = controller.OnTopDown;
        _onBotDown = controller.OnBotDown;
        _onNoInput = controller.OnNoInput;

        controller.Init();
    }

}
