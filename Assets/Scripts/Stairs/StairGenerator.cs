using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;

public class StairGenerator : Singleton<StairGenerator>
{
    [SerializeField] private List<GameObject> stairsList;
    [SerializeField] private GameObject stairPrefab;
    [SerializeField] private int numberOfStairs;
    [SerializeField] private float yOffset;
    [SerializeField] private float radianMultiplier;
    public float climbRate;
    private int moveIndex;
    private float timeToClimb;
    private bool isGameStarted;
    private bool isGameEnded;
    [Header("HeightMeter Wood Settings")]
    [SerializeField] private Transform heightWood;
    [SerializeField] private Ease woodMoveEase;
    [SerializeField] private float woodMoveDuration;
    [SerializeField] private TextMeshPro heightWoodText;
    [SerializeField] private float heightValue;
    [SerializeField] private GameObject moneyIncomeTextOBJ;
    [SerializeField] private TextMeshPro moneyIncomeText;

    private void Start()
    {
        GenerateStairs();
        heightWoodText.text = heightValue.ToString("F1");
        moneyIncomeTextOBJ.SetActive(false);
        GameUIEvents.Instance.OnGameStarted += OnGameStarted;
        GameUIEvents.Instance.OnGameEnded += OnGameEnded;
    }
    public float HeightCount
    {
        get => heightValue;
        set
        {
            heightValue = value;
            heightWoodText.text = heightValue.ToString("F1");
        }
    }
    private void Update()
    {
        if (!isGameStarted) return;
        if (isGameEnded) return;
        if (Input.GetMouseButton(0) && Time.time>timeToClimb && moveIndex < numberOfStairs)
        {
            timeToClimb = Time.time + climbRate;
            transform.GetChild(moveIndex).gameObject.SetActive(true);
            var playerTransform = PlayerController.Instance.transform;
            playerTransform.DOJump(transform.GetChild(moveIndex).localPosition, .1f, 1,climbRate);
            playerTransform.rotation = Quaternion.Euler(stairsList[moveIndex].transform.eulerAngles.x,stairsList[moveIndex].transform.eulerAngles.y +60,stairsList[moveIndex].transform.rotation.z);
            moveIndex++;
            MoneyScoreManager.Instance.IncreaseMoney();
            IncreaseHeightWood();
            HeightCount += 0.4f;
            StartCoroutine(IncomeTextAction());
            HapticManager.Instance.PlayHaptic();
        }
    }
    void GenerateStairs()
    {
        for (int i = 0; i < numberOfStairs; i++)
        {
            var radians = 1.5f * Mathf.PI / radianMultiplier * i;
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);
            var spawnDir = new Vector3(horizontal, 0, vertical);
            var spawnPos =spawnDir * 0.7f + Vector3.up*i*yOffset;
            Transform stair = Instantiate(stairPrefab, spawnPos, Quaternion.identity,transform).transform;
            stairsList.Add(stair.gameObject);
            stair.LookAt(Vector3.up*spawnPos.y);
        }

        foreach (var stair in stairsList)
        {
            stair.SetActive(false);
        }
    }

    void IncreaseHeightWood()
    {
        heightWood.DOMoveY(PlayerController.Instance.transform.position.y - .1f, woodMoveDuration).SetEase(woodMoveEase);
    }

    void WoodFalling()
    {
        foreach (var stair in stairsList)
        {
          var rb =  stair.GetComponent<Rigidbody>();
          rb.isKinematic = false;
          rb.AddForceAtPosition(Vector3.up, stair.transform.position,ForceMode.Impulse);
          rb.AddRelativeTorque(Vector3.right * 5);

        }
    }

    IEnumerator IncomeTextAction()
    {
        moneyIncomeTextOBJ.SetActive(true);
        yield return new WaitForSeconds(.1f);
        moneyIncomeTextOBJ.SetActive(false);
    }

    #region Game Events

    private void OnGameStarted()
    {
        moneyIncomeText.text = DataManager.Income.ToString();
        isGameStarted = true;
    }
    private void OnGameEnded(bool result)
    {
        if(!result) WoodFalling();
        isGameEnded = true;
    }
    private void OnDisable()
    {
        GameUIEvents.Instance.OnGameStarted -= OnGameStarted;
        GameUIEvents.Instance.OnGameEnded -= OnGameEnded;
    }

    #endregion

}
