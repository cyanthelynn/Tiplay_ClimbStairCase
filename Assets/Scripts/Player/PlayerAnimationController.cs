using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : Singleton<PlayerAnimationController>
{
    [SerializeField] private Animator playerAnimator;
    public void SetPlayerAnimation(string name, bool value)
    {
      playerAnimator.SetBool(name,value);
    }
}
