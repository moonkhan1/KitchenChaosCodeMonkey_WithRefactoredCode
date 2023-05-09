using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimator
{
    Animator _animator { get; }
    void MoveAnimation(bool isWalking);
}
