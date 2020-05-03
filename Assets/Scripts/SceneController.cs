using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int level = 0;
    public bool isHard = false;

    [SerializeField]
    private GameObject transitionCanvas;

    [SerializeField]
    private GameObject gameCanvas;

    public const int easyLevelsCount = 10;
    public const int hardLevelsCount = 10;
    public const int totalLevelsCount = easyLevelsCount + hardLevelsCount;

    private ScoreController score;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        // чтобы предотвратить размножение ковасов при повторном переходе на меню,
        // уберем их из иерархии и создатим здесь, используя их префаб
        gameCanvas = Instantiate(gameCanvas);
        gameCanvas.name = "GameCanvas";
        transitionCanvas = Instantiate(transitionCanvas);
        transitionCanvas.name = "TransitionCanvas";

        DontDestroyOnLoad(gameCanvas);
        DontDestroyOnLoad(transitionCanvas);
        // score = GameObject.Find("Score").GetComponent<ScoreController>();
    }

    public void NextLevel()
    {
        // score.ResetScore();

        // мы переходим в меню, если все уровни в той или иной сложности (легкой или сложной) пройдены
        if ((isHard && (level == hardLevelsCount)) || (!isHard && (level == easyLevelsCount)))
        {
            level = 0;
            SceneManager.LoadScene("Menu");
        }
        else
        // если уровни остались, то переходим на следующий
        {
            level++;
            LoadCurrentLevel();
        }
    }

    public void LoadCurrentLevel()
    {
        // выключаем панели с кнопками при переходе
        transitionCanvas.SetActive(false);

        // если канвас со счетом не включен, то включаем его
        gameCanvas.SetActive(true);

        // score.ResetScore();
        
        if (isHard)
        {
            SceneManager.LoadScene("Level_Hard_" + level, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Level_Easy_" + level, LoadSceneMode.Single);
        }
    }

    public void OpenTransitionPanel()
    {
        transitionCanvas.SetActive(true);
    }

    public void SetLevel(int levelIndex)
    {
        level = levelIndex;
    }

    public void SetDifficulty(bool isHard)
    {
        this.isHard = isHard;
    }
}
