using UnityEngine;

namespace Managers
{
    public class DataManager : MonoBehaviour
    {
        public static int Level
        {
            get => PlayerPrefs.GetInt("levelKey",0);
            set => PlayerPrefs.SetInt("levelKey",value);
        }
        public static float Money
        {
            get => PlayerPrefs.GetFloat("money",MoneyScoreManager.Instance.MoneyCount);
            set => PlayerPrefs.SetFloat("money",value);
        }
        public static float Stamina
        {
            get => PlayerPrefs.GetFloat("stamina",StaminaManager.Instance.maxStamina);
            set => PlayerPrefs.SetFloat("stamina",value);
        }
        public static float StaminaCost
        {
            get => PlayerPrefs.GetFloat("staminaCost",IncrementalManager.Instance.StaminaCost);
            set => PlayerPrefs.SetFloat("staminaCost",value);
        }
        public static float Income
        {
            get => PlayerPrefs.GetFloat("income",MoneyScoreManager.Instance.Income);
            set => PlayerPrefs.SetFloat("income",value);
        }
        public static float IncomeCost
        {
            get => PlayerPrefs.GetFloat("incomeCost",IncrementalManager.Instance.IncomeCost);
            set => PlayerPrefs.SetFloat("incomeCost",value);
        }
        public static float Speed
        {
            get => PlayerPrefs.GetFloat("speed",StairGenerator.Instance.climbRate);
            set => PlayerPrefs.SetFloat("speed",value);
        }
        public static float SpeedCost
        {
            get => PlayerPrefs.GetFloat("speedCost",IncrementalManager.Instance.SpeedCost);
            set => PlayerPrefs.SetFloat("speedCost",value);
        }
    }
}
