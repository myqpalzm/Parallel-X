using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PuzzleObject : MonoBehaviour{
    public float targetMoveY;
    public float duration;

    public Vector3 targetRotation;

    public IdleAnim idleAnim;
    private void Start(){
        switch (idleAnim){
            case IdleAnim.Floating:
                Floating();
                break;
            case IdleAnim.Rotating:
                Rotate();
                break;
        }
        
    }

    private void Floating(){
        transform.DOMoveY(targetMoveY, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void Rotate(){
        transform.DORotate(targetRotation, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    public enum IdleAnim{
        Floating,
        Rotating
    }
    
}
