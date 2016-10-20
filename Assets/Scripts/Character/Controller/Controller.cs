using UnityEngine;
using System.Collections;

public abstract class Controller : MonoBehaviour
{
    protected Rigidbody _body;
    /// <returns>True if the character is rolling False otherwise</returns>
    public abstract bool IsRolling();
    /// <summary>
    /// Handle the Top event
    /// </summary>
    public abstract void OnTop();
    /// <summary>
    /// Handle the Bot event
    /// </summary>
    public abstract void OnBot();
    /// <summary>
    /// Handle the Top event only once when pressed
    /// </summary>
    public abstract void OnTopDown();
    /// <summary>
    /// Handle the Bot event only once when pressed
    /// </summary>
    public abstract void OnBotDown();
    /// <summary>
    /// Handle the No Input event
    /// </summary>
    public abstract void OnNoInput();
    /// <summary>
    /// Initiliaze the controller
    /// </summary>
    /// <param name="IsRolling">is the Character rolling ?</param>
    public abstract void Init(bool IsRolling);
    /// <summary>
    /// Disable the controller
    /// </summary>
    public abstract void Disable();

}
