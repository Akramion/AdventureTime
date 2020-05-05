using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionController : MonoBehaviour
{
    private float shadowAlpha;
    private float transitionPanelAlpha;

    private Image shadowImage;
    private CanvasGroup canvasGroupTransitionPanel;

    private Coroutine blackoutAnimationCoroutine;

    private void Awake()
    {
        SceneController sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();

        Button nextButton = transform.Find("TransitionPanel/NextLevel").GetComponent<Button>();
        Button restartButton = transform.Find("TransitionPanel/RestartLevel").GetComponent<Button>();

        nextButton.onClick.AddListener(() => {
            sceneController.NextLevel();
            gameObject.SetActive(false);
        });

        restartButton.onClick.AddListener(() => {
            sceneController.LoadCurrentLevel();
            gameObject.SetActive(false);
        });

        // тень
        shadowImage = GetComponent<Image>();

        // панель с кнопками и счетом
        canvasGroupTransitionPanel = transform.Find("TransitionPanel").GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        // когда объект включается, запускаем анимацию
        blackoutAnimationCoroutine = StartCoroutine(BlackoutAnimation());
    }

    private void OnDisable()
    {
        // когда объект выключается, все альфа-каналы обнуляются и анимации останавливаются
        shadowAlpha = 0f;
        transitionPanelAlpha = 0f;

        shadowImage.color = new Color(shadowImage.color.r, shadowImage.color.g, shadowImage.color.b, 0.0f);
        canvasGroupTransitionPanel.alpha = 0.0f;

        // останавливаем анимацию
        StopCoroutine(blackoutAnimationCoroutine);
    }

    IEnumerator BlackoutAnimation()
    {
        // двухступенчатая анимация

        // продолжает работать, пока панель с кнопками не появится

        // Сначала увеличиваем альфа канал тени
        while (shadowAlpha < 0.7f)
        {
            shadowAlpha += 0.7f * Time.deltaTime;
            shadowImage.color = new Color(shadowImage.color.r, shadowImage.color.g, shadowImage.color.b, shadowAlpha);
            yield return null;
        }

        // затем повышаем альфа канал панели с кнопками
        while (transitionPanelAlpha <= 1.0f)
        {
            transitionPanelAlpha += 1.0f * Time.deltaTime;
            canvasGroupTransitionPanel.alpha = transitionPanelAlpha;

            yield return null;
        }
    }
}
