using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private Transform finishMovePos;
    [SerializeField] private Transform jumpMovePos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameUIEvents.Instance.GameEnded(true);
            PlayerAnimationController.Instance.SetPlayerAnimation("Climb",true);
            transform.rotation = Quaternion.identity;
            transform.DOJump(jumpMovePos.position,.5f,1,0.5f).Append(transform.DOMove(finishMovePos.position, 5f));
        }
    }
}
