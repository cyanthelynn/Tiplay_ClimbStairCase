using System;
using Managers;
using TMPro;
using UnityEngine;

public class MoneyScoreManager : Singleton<MoneyScoreManager>
{
   [SerializeField] private int totalMoney;
   [SerializeField] private float income;
   [SerializeField] private TextMeshProUGUI moneyText;

   private void Start()
   {
      GameUIEvents.Instance.OnGameEnded += OnGameEnded;
      GameUIEvents.Instance.OnGameStarted += OnGameStarted;
      MoneyCount = (int)DataManager.Money;
   }

   private void OnGameStarted()
   {
      MoneyCount = (int)DataManager.Money;
      Income = DataManager.Income;
      Stamina = DataManager.Stamina;
      Speed = DataManager.Speed;
   }

   #region Props

   public int MoneyCount
   {
      get => totalMoney;
      set
      {
         totalMoney = value;
         moneyText.text = totalMoney.ToString();
      }
   }
   public float Income
   {
      get => income;
      set => income = value;
   }
   public float Stamina
   {
      get => StaminaManager.Instance.maxStamina;
      set => StaminaManager.Instance.maxStamina = value;
   }
   public float Speed
   {
      get => StairGenerator.Instance.climbRate;
      set => StairGenerator.Instance.climbRate = value;
   }

   #endregion
   
   public void IncreaseMoney() => MoneyCount += (int)Income;

   void OnGameEnded(bool result)
   {
      DataManager.Money = MoneyCount;
   }
   private void OnDisable()
   {
      GameUIEvents.Instance.OnGameEnded -= OnGameEnded;
      GameUIEvents.Instance.OnGameStarted -= OnGameStarted;
   }
}
