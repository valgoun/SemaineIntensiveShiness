using UnityEngine;
using System.Collections;

public abstract class Controller : MonoBehaviour
{
    protected Rigidbody _body;
    public abstract bool IsRolling();
    public abstract void OnTop();
    public abstract void OnBot();
    public abstract void OnTopDown();
    public abstract void OnBotDown();
    public abstract void OnNoInput();
    public abstract void Init();
    public abstract void Disable();

}
