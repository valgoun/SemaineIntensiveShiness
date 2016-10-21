using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuiAnimation : MonoBehaviour
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        transform.DOLocalMoveY(0.25f + Random.value * 0.2f, 0.25f + Random.value * 0.2f).SetRelative().SetEase(Ease.OutCubic).SetLoops(-1, LoopType.Yoyo).SetDelay(Random.value);
    }
}
