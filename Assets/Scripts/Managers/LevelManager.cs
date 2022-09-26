using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Singleton
        
    public static LevelManager Instance;

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

        LoadOtherScenes();
    }
        
    #endregion

    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        levelText.text = (DataManager.Level + 1).ToString();
    }

    public GameObject[] levels;
    private void LoadOtherScenes()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        LoadLevelPrefab();
    }

    private void LoadLevelPrefab()
    {
        if (levels.Length == 0)
        {
            Debug.LogWarning("Not Enough Level. Check LevelManager");
            return;
        }
        Instantiate(levels[DataManager.Level % levels.Length]);
    }
    
    
}
