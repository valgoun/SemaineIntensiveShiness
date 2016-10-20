using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class Character : MonoBehaviour
{
    private Controller _activeController, _topController, _sideController;
    private Action _onBot, _onTop, _onBotDown, _onTopDown, _onNoInput;
    private Rigidbody _body;
    private CameraSetUp _cam;
    // Use this for initialization
    void Start()
    {
        _topController = GetComponent<TopController>();
        _topController.enabled = false;
        _sideController = GetComponent<SideController>();
        _sideController.enabled = false;
        _body = GetComponent<Rigidbody>();
        _cam = Camera.main.GetComponent<CameraSetUp>();

        setActiveController(_topController, false);
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

    void setActiveController(Controller controller, bool IsRolling)
    {
        _activeController = controller;
        _activeController.enabled = true;

        //Assign delegates
        _onTop = controller.OnTop;
        _onBot = controller.OnBot;
        _onTopDown = controller.OnTopDown;
        _onBotDown = controller.OnBotDown;
        _onNoInput = controller.OnNoInput;

        controller.Init(IsRolling);
    }

    void disableController()
    {
        _activeController.enabled = false;
        _activeController.Disable();
    }

    public void goSideView(Transform runTarget, Transform rollTarget, float runSpeed, float rollSpeed)
    {
        if (!_activeController.IsRolling())
        {
            disableController();
            _body.velocity = Vector3.zero;
            _body.DOJump(runTarget.position, 5f, 1, (transform.position - runTarget.position).magnitude / runSpeed, false).SetUpdate(UpdateType.Fixed).OnComplete(() =>
            {
                setActiveController(_sideController, false);
            });
            _cam.GoToSide((transform.position - runTarget.position).magnitude / runSpeed - 0.2f);
        }
        else
        {
            disableController();
            _body.velocity = Vector3.zero;
            _body.DOJump(rollTarget.position, 5f, 1, (transform.position - rollTarget.position).magnitude / rollSpeed, false).SetUpdate(UpdateType.Fixed).SetEase(Ease.Linear).OnComplete(() =>
            {
                setActiveController(_sideController, true);
            });
            _cam.GoToSide((transform.position - rollTarget.position).magnitude / rollSpeed - 0.2f);
        }
    }

    public void goTopView(Transform target, float speed)
    {
        disableController();
        _body.velocity = Vector3.zero;
        float time = (transform.position - target.position).magnitude / speed;
        _body.DOJump(target.position, 5f, 1, time).SetUpdate(UpdateType.Fixed).OnComplete(() =>
        {
            setActiveController(_topController, true);
        }).SetEase(Ease.Linear);
        _cam.GoToPerspective(time);
    }

}
