using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressBarImg;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;

    private void Start()
    {
        currentLevelText.text = (DataManager.Level + 1).ToString();
        nextLevelText.text = (DataManager.Level + 2).ToString();
    }

    private void LateUpdate()
    {
        progressBarImg.fillAmount =  PlayerController.Instance.GetProgressBarPercent();
    }
}
