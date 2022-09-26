using System.Collections;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIScripts
{
    public class MainPanelController : MonoBehaviour
    {
        #region Singleton
        
        public static MainPanelController Instance;

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

        [SerializeField]private GameObject completePanel;
        [SerializeField]private GameObject failPanel;
        [SerializeField]private CanvasGroup canvasGroup;
        [SerializeField]private float fadeTime;
        [SerializeField]private CanvasGroup startPanelCanvasGroup;
        [SerializeField]private float startPanelFadeTime;
        private void Start()
        {
            canvasGroup.alpha = 1f;
            SetCanvasGroupAlpha(0f);
        }

        #region LevelEndActions

        public void OpenGameEndPanel(bool success)
        {
            StartCoroutine(OpenGameEndPanelRoutine(success));
        }

        private IEnumerator OpenGameEndPanelRoutine(bool success)
        {
            yield return new WaitForSeconds(2f);
            if (success)
            {
                completePanel.SetActive(true);
            }
            else
            {
                failPanel.SetActive(true);
            }
        }
        
        public void CompleteLevelButton()
        {
            DataManager.Level++;
            StartCoroutine(LoadRoutine());
        }
        public void FailButton()
        {
            StartCoroutine(LoadRoutine());
        }

        #endregion
        
        
        private IEnumerator LoadRoutine()
        {
            SetCanvasGroupAlpha(1f);
            yield return new WaitForSeconds(0.5f);
            //it will chage to 1 when Elephant added to buildSettings
            SceneManager.LoadScene("MainScene");
        }
        
        #region FadeInOut

        private void SetCanvasGroupAlpha(float b)
        {
            float a = canvasGroup.alpha;
            DOTween.To(SetAlphaTweenCall,a, b, fadeTime);
        }
        
        private void SetAlphaTweenCall(float x)
        {
            canvasGroup.alpha = x;
        }

        public void FadeOutStartPanel()
        {
            float a = startPanelCanvasGroup.alpha;
            DOTween.To(SetSpAlphaTweenCall,a, 0f, startPanelFadeTime).OnComplete(()=>startPanelCanvasGroup.gameObject.SetActive(false));
        }
        
        private void SetSpAlphaTweenCall(float x)
        {
            startPanelCanvasGroup.alpha = x;
        }

        #endregion
    }
}
