using System;
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

    public static int easyLevelsCount = 10;
    public static int hardLevelsCount = 10;
    public static int totalLevelsCount = easyLevelsCount + hardLevelsCount;

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
    }

    public void NextLevel()
    {

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

        // Обнуляем очки
        GameObject.Find("Score").GetComponent<ScoreController>().ResetScore();
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

        // Отображение времени
        ShowTime();

        // Отображение очков
        ShowScore();

        gameCanvas.SetActive(false);
    }

    public void ShowScore() {
        ScoreController  scoreController = GameObject.Find("Score").GetComponent<ScoreController>();
        Text scoreOutput = GameObject.Find("CoinsText").GetComponent<Text>();
        string currentScore = "Собрано очков: " + scoreController.score;
        scoreOutput.text = currentScore;
    }

    public void ShowTime() {
        Text timeOuput = GameObject.Find("TimeText").GetComponent<Text>();
        string currentTime = "Время: " + Math.Round(Time.timeSinceLevelLoad, 3).ToString() + "c";
        timeOuput.text = currentTime;
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
