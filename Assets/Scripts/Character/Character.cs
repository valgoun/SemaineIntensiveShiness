using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class Character : MonoBehaviour
{
    public float TotalLifePoint;
    public static Character MainCharacter
    {
        get
        {
            return _mainCharacter;
        }
    }

    public float Coins
    {
        get
        {
            return _coins;
        }
        set
        {
            _coins = value;
        }
    }
    public float deathLevel = -20f;

    private Controller _activeController, _topController, _sideController;
    private Action _onBot, _onTop, _onBotDown, _onTopDown, _onNoInput;
    private Rigidbody _body;
    private CameraSetUp _cam;
    private float _lifePoints;
    private float _coins;
    private static Character _mainCharacter;


    // Use this for initialization
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _mainCharacter = this;
    }
    void Start()
    {
        _topController = GetComponent<TopController>();
        _topController.enabled = false;
        _sideController = GetComponent<SideController>();
        _sideController.enabled = false;
        _body = GetComponent<Rigidbody>();
        _cam = Camera.main.GetComponent<CameraSetUp>();
        _lifePoints = TotalLifePoint;


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

        if (transform.position.y < deathLevel)
        {
            Debug.Log("DEATH!!!");
        }
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

    public void goSideView(Transform runTarget, Transform rollTarget, float runSpeed, float rollSpeed, float RunJumpForce, float RollJumpForce)
    {
        if (!_activeController.IsRolling())
        {
            disableController();
            _body.velocity = Vector3.zero;
            _body.DOJump(runTarget.position, RunJumpForce, 1, (transform.position - runTarget.position).magnitude / runSpeed, false).SetUpdate(UpdateType.Fixed).OnComplete(() =>
            {
                setActiveController(_sideController, false);
            });
            _cam.GoToSide((transform.position - runTarget.position).magnitude / runSpeed - 0.2f);
        }
        else
        {
            disableController();
            _body.velocity = Vector3.zero;
            _body.DOJump(rollTarget.position, RollJumpForce, 1, (transform.position - rollTarget.position).magnitude / rollSpeed, false).SetUpdate(UpdateType.Fixed).SetEase(Ease.Linear).OnComplete(() =>
            {
                setActiveController(_sideController, true);
            });
            _cam.GoToSide((transform.position - rollTarget.position).magnitude / rollSpeed - 0.2f);
        }
    }

    public void goTopView(Transform target, float speed, float JumpForce)
    {
        disableController();
        _body.velocity = Vector3.zero;
        float time = (transform.position - target.position).magnitude / speed;
        _body.DOJump(target.position, JumpForce, 1, time).SetUpdate(UpdateType.Fixed).OnComplete(() =>
        {
            setActiveController(_topController, true);
        }).SetEase(Ease.Linear);
        _cam.GoToPerspective(time * 2);
    }

    /// <summary>
    /// Deal Damages to the character
    /// </summary>
    /// <param name="amount">how many life point will the character loose</param>
    public void DealDamages(float amount)
    {
        _lifePoints -= amount;
        if (_lifePoints <= 0.0f)
        {
            _lifePoints = 0.0f;
            //TODO : Death
        }
    }

}
