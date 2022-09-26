using System;
using System.Collections;
//using Happy.Analytics;
//using Happy.Analytics;
using Managers;
using UIScripts;
using UnityEngine;

public class GameUIEvents : MonoBehaviour
{
    #region Singleton
        
    public static GameUIEvents Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
        
    #endregion

    public void GameStarted()
    {
       // HappyAnalytics.LevelStartEvent(DataManager.Level);
        OnGameStarted?.Invoke();
    }

    
    public event Action OnGameStarted;
    public event Action<bool> OnGameEnded;

    public void GameEnded(bool success)
    {
        Debug.Log("Oyun bitti" + success);

        StartCoroutine(GameEndRoutine(success));
    }

    private IEnumerator GameEndRoutine(bool success)
    {
        OnGameEnded?.Invoke(success);
        if (success)
        {
            //HappyAnalytics.LevelCompleteEvent(DataManager.Level);
        }
        else
        {
           // HappyAnalytics.LevelFailEvent(DataManager.Level);
        }
        yield return new WaitForSeconds(0.1f);
        MainPanelController.Instance.OpenGameEndPanel(success);
    }

   
}
