using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

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
    public float RollingSpeed;
    public float BlendSpeed;

    private Vector3 rotation;
    private GameObject Poui;
    private GameObject Pivot;
    private Animator Anim;
    private bool FirstRoll = true;

    private Transform _lifePointsPui;
    private bool _canLoosePDV = true;
    private bool _isDead = false;
    public MenuGameover menuGameover;

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
        _lifePointsPui = transform.GetChild(1);


        setActiveController(_topController, false);

        Pivot = transform.GetChild(0).gameObject;
        Poui = Pivot.transform.GetChild(0).gameObject;
        rotation = new Vector3(Poui.transform.rotation.eulerAngles.x + RollingSpeed * Time.deltaTime, 0, 0);
        Anim = Poui.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool inputEvent = false;
        if (_isDead)
            return;
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
            Dead();
        }

        if (_activeController.IsRolling())
        {
            if (FirstRoll)
            {
                this.gameObject.transform.DORotate(new Vector3(0, 90, 0), 0.01f);
                Pivot.transform.DORotate(new Vector3(0, 90, 0), 0.01f);
                FirstRoll = false;
            }
            Poui.transform.DOScaleY(0.7f, BlendSpeed);
            Pivot.transform.Rotate(rotation);
        }
        else
        {
            Poui.transform.DOScaleY(1, BlendSpeed);
            if (!FirstRoll)
            {
                this.gameObject.transform.DORotate(new Vector3(0, 90, 0), 0.01f);
                Pivot.transform.DORotate(new Vector3(0, 90, 0), 0.1f);
                FirstRoll = true;
            }
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
        Anim.SetBool("IsSide", true);
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
        Anim.SetBool("IsGliding", false);
        Anim.SetBool("IsJumping", false);
        Anim.SetBool("IsStomping", false);
        Anim.SetBool("IsSide", false);
        Anim.SetBool("IsGrounded", true);

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
        if (!_canLoosePDV)
            return;
        _lifePoints -= amount;
        _lifePointsPui.GetChild((int)_lifePoints).gameObject.SetActive(false);
        _canLoosePDV = false;
        DOVirtual.DelayedCall(0.5f, () => _canLoosePDV = true);

        ScreenColor.Img.color = new Color(0.7f, 0.2f, 0.2f, 0.0f);
        ScreenColor.Img.DOFade(0.7f, 0.07f).SetLoops(2, LoopType.Yoyo);

        if (_lifePoints <= 0.0f)
        {
            _lifePoints = 0.0f;
            Dead();
        }
    }

    private void Dead()
    {
        _isDead = true;
        _body.velocity = Vector3.zero + Vector3.up * _body.velocity.y;
        ScreenColor.Img.color = new Color(0, 0, 0, 0);
        ScreenColor.Img.DOFade(1, 1f).OnComplete(() =>
        {
            Seed.collected = 0;
            menuGameover.AppearDeath();
        }).SetEase(Ease.InCirc);
    }

    public void Victory()
    {
        _isDead = true;
        _isDead = true;
        _body.velocity = Vector3.zero + Vector3.up * _body.velocity.y;

        ScreenColor.Img.color = new Color(1, 0.5f, 0.5f, 0);
        ScreenColor.Img.DOFade(1, 1f).SetEase(Ease.InCirc);
        menuGameover.SelectDefaultButtonWin();
        menuGameover.AppearWin();
    }

}
