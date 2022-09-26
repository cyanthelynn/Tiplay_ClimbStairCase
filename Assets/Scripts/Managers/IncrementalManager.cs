using System;
using Managers;
using TMPro;
using UnityEngine;

public class IncrementalManager : Singleton<IncrementalManager>
{
    [SerializeField] private Canvas incrementalCanvasGroup;
    [SerializeField] private float minIncrementLimit;
    [SerializeField] private float increaseStaminaValue;
    [SerializeField] private float increaseStaminaCost;
    [SerializeField] private float increaseSpeedValue;
    [SerializeField] private float increaseSpeedCost;
    [SerializeField] private float increaseIncomeValue;
    [SerializeField] private float increaseIncomeCost;
    [SerializeField] private float increaseCostMultiplier;
    [SerializeField] private TextMeshProUGUI staminaCostText;
    [SerializeField] private TextMeshProUGUI speedCostText;
    [SerializeField] private TextMeshProUGUI incomeCostText;
    private void Start()
    {
        GameUIEvents.Instance.OnGameStarted += OnGameStarted;
        staminaCostText.text = DataManager.StaminaCost.ToString();
        speedCostText.text = DataManager.SpeedCost.ToString();
        incomeCostText.text = DataManager.IncomeCost.ToString();
        StaminaCost = DataManager.StaminaCost;
        SpeedCost = DataManager.SpeedCost;
        IncomeCost = DataManager.IncomeCost;
        if (DataManager.Money >= minIncrementLimit)
        {
            incrementalCanvasGroup.gameObject.SetActive(true);
        }
    }
    public void IncreaseStamina()
    {
        if (!(DataManager.Money >= increaseStaminaCost)) return;
        DataManager.Stamina += increaseStaminaValue;
        DataManager.Money -= increaseStaminaCost;
        StaminaManager.Instance.maxStamina += increaseStaminaValue;
        MoneyScoreManager.Instance.MoneyCount -= (int) increaseStaminaCost;
        StaminaCost *= increaseCostMultiplier;
        DataManager.StaminaCost = StaminaCost;
    }
    public void IncreaseSpeed()
    {
        if (!(DataManager.Money >= increaseSpeedCost)) return;
        DataManager.Speed -= increaseSpeedValue;
        DataManager.Money -= increaseSpeedCost;
        MoneyScoreManager.Instance.MoneyCount -= (int)increaseSpeedCost;
        StairGenerator.Instance.climbRate -= increaseSpeedValue;
        SpeedCost *= increaseCostMultiplier;
        DataManager.SpeedCost = SpeedCost;
    }

    public void IncreaseIncome()
    {
        if (!(DataManager.Money >= increaseIncomeCost)) return;
        DataManager.Income += increaseIncomeValue;
        DataManager.Money -= increaseIncomeCost;
        MoneyScoreManager.Instance.MoneyCount -= (int)increaseIncomeCost;
        IncomeCost *= increaseCostMultiplier;
        DataManager.IncomeCost = IncomeCost;
    }
    public float StaminaCost
    {
        get => increaseStaminaCost;
        set
        {
            increaseStaminaCost = value;
            staminaCostText.text = increaseStaminaCost.ToString();
        }
    }
    public float SpeedCost
    {
        get => increaseSpeedCost;
        set
        {
            increaseSpeedCost = value;
            speedCostText.text = increaseSpeedCost.ToString();
        }
    }
    public float IncomeCost
    {
        get => increaseIncomeCost;
        set
        {
            increaseIncomeCost = value;
            incomeCostText.text = increaseIncomeCost.ToString();
        }
    }

    private void OnGameStarted()
    {
        incrementalCanvasGroup.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameUIEvents.Instance.OnGameStarted -= OnGameStarted;
    }
}
