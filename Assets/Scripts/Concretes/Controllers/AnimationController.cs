using System.Collections;
using System.Collections.Generic;
using Kitchen.Abstract.Controller;
using UnityEngine;

public class AnimationController : IAnimator
{
    public Animator _animator { get; set; }
    public const string IS_WALKING = "IsWalking";

    public AnimationController(IEntityController entityController)
    {
        _animator = entityController.transform.GetComponentInChildren<Animator>();
    }

    public void MoveAnimation(bool isWalking)
    {
        if (isWalking)
        {
            _animator.SetBool(IS_WALKING, true);
        }
        else
        {
            _animator.SetBool(IS_WALKING, false);
        }
    }


}
