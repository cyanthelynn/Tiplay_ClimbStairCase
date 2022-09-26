using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
  #region SerializeField Variables
  [Header("Player State Settings")]
  [Space]
  [SerializeField] private PlayerState State;

  [Header("VFX Particle Settings")] 
  [Space] 
  [SerializeField]
  private ParticleSystem sweatParticle;
  [SerializeField]
  private ParticleSystem deathParticle;
  public List<ParticleSystem> confettiList;
  [SerializeField]
  private GameObject playerMesh;
  [SerializeField]private Color maxHealthColor;
  [SerializeField]private Color lowHealthColor;
  [SerializeField] private Renderer skinRenderer;
  [SerializeField] private GameObject finishPoint;
  #endregion

  #region Private Variables

  private bool isGameStarted;
  private bool isGameEnded;
  private Sequence seq;
  private float finishPos;
  
  #endregion
  
  #region Unity Event Methods

  private void Start()
  {
    GameUIEvents.Instance.OnGameStarted += OnGameStarted;
    GameUIEvents.Instance.OnGameEnded += OnGameEnded;
    skinRenderer.material.SetColor("_Color", maxHealthColor);
    finishPos = finishPoint.transform.position.y;
  }

  private void Update()
  {
    if (!isGameStarted) return;
    if (isGameEnded) return;
    UpdatePlayerState(Input.GetMouseButton(0) ? PlayerState.Climbing : PlayerState.Resting);
  }
  private void OnDisable()
  {
    GameUIEvents.Instance.OnGameStarted -= OnGameStarted;
    GameUIEvents.Instance.OnGameEnded -= OnGameEnded;
  }

  #endregion

  #region Game Event Methods

  void OnGameStarted()
  {
    isGameStarted = true;
  }
  private void OnGameEnded(bool result)
  {
    if (result)
    {
      foreach (var confetti in confettiList)
      {
        confetti.Play();
      }
    }
    isGameEnded = true;
  }

  #endregion

    #region State Methods

   void UpdatePlayerState(PlayerState newState)
    {
      State = newState;
      switch (newState)
      {
        case PlayerState.Climbing:
          Climbing();
          break;
        case PlayerState.Resting:
          Resting();
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
      }
    }
    #endregion

    #region Methods
    void Climbing()
    {
      PlayerAnimationController.Instance.SetPlayerAnimation("Climb",true);
      StaminaManager.Instance.DrainStamina();
    }
    void Resting()
    {
      PlayerAnimationController.Instance.SetPlayerAnimation("Climb",false);
      StaminaManager.Instance.RefillStamina();
      SetPlayerSkinColor();
    }
    public void PlayerExhausted()
     {
       PlayerTiredScaleFX();
       SetPlayerSkinColor();
       if(!sweatParticle.isPlaying) sweatParticle.Play();
     }
     public void PlayerRecovered()
     {
       DOTween.KillAll();
       sweatParticle.Stop();
     }
     public void PlayerDeathAction()
     {
        deathParticle.Play();
        sweatParticle.Stop();
        GameUIEvents.Instance.GameEnded(false);
        playerMesh.SetActive(false);
     }
     void PlayerTiredScaleFX()
     {
       Sequence seq = DOTween.Sequence();
       seq.Append(transform.DOScale(0.9f, 0.2f).SetEase(Ease.InOutCirc)).Append(transform.DOScale(1, 0.2f))
           .SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);
     }
     public float GetProgressBarPercent()
     {
       return transform.position.y / finishPos;
     }

     void SetPlayerSkinColor()
     {
       Color currentColor = Color.Lerp(lowHealthColor, maxHealthColor, (float) StaminaManager.Instance.currentStamina / StaminaManager.Instance.maxStamina );
       skinRenderer.material.SetColor("_Color", currentColor);
     }
    #endregion
  #region Enums
  enum PlayerState
  {
    Climbing,Resting
  }
  #endregion
}
