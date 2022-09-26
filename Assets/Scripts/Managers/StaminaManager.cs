using Managers;
using UnityEngine;

public class StaminaManager : Singleton<StaminaManager>
{
    [Header("Stamina Settings")] 
    public float maxStamina;
    public float currentStamina;
    [SerializeField] private float staminaDrainRate;
    [SerializeField] private float staminaRefillRate;
    [SerializeField] private float exhaustedRatePercent;
    private void Start()
    {
        GameUIEvents.Instance.OnGameStarted += OnGameStarted;
        maxStamina = DataManager.Stamina;
    }

    private void OnGameStarted()
    {
        currentStamina = maxStamina;
    }

    public void DrainStamina()
    {
        if (currentStamina >= 0)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            if (currentStamina < maxStamina * exhaustedRatePercent)
            {
                PlayerController.Instance.PlayerExhausted();
            }
        }
        else
        {
            PlayerController.Instance.PlayerDeathAction();
        }
    }
    public void RefillStamina()
    {
        if (currentStamina <= maxStamina)
        {
            currentStamina += staminaRefillRate * Time.deltaTime;
            if (currentStamina > maxStamina * exhaustedRatePercent)
            {
                PlayerController.Instance.PlayerRecovered();
            }
        }
    }
}
