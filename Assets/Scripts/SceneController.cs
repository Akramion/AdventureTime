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
    [SerializeField]
    private GameObject soundManager;


    private RatingController ratingController;

    public const int easyLevelsCount = 10;
    public const int hardLevelsCount = 10;
    public const int totalLevelsCount = easyLevelsCount + hardLevelsCount;

    private static SceneController singleton;

    private void Awake()
    {
        // предотвращаем дублирование менеджера сцен при переходе на меню
        if (singleton)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            singleton = this;
        }

        DontDestroyOnLoad(this);

        // чтобы предотвратить размножение ковасов при повторном переходе на меню,
        // уберем их из иерархии и создатим здесь, используя их префаб
        gameCanvas = Instantiate(gameCanvas);
        gameCanvas.name = "GameCanvas";
        transitionCanvas = Instantiate(transitionCanvas);
        transitionCanvas.name = "TransitionCanvas";
        soundManager = Instantiate(soundManager);
        soundManager.name = "SoundManager";

        ratingController = GetComponent<RatingController>();

        DontDestroyOnLoad(gameCanvas);
        DontDestroyOnLoad(transitionCanvas);
    }


    public void NextLevel()
    {
        // мы переходим в меню, если все уровни в той или иной сложности (легкой или сложной) пройдены
        if ((isHard && (level == hardLevelsCount)) || (!isHard && (level == easyLevelsCount)))
        {
            LoadMenu();
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

    public void LoadMenu()
    {
        // выключаем все игровые канвасы
        transitionCanvas.SetActive(false);
        gameCanvas.SetActive(false);

        level = 0;
        SceneManager.LoadScene("Menu");
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

    public void ShowScore()
    {
        ScoreController scoreController = gameCanvas.transform.Find("GamePanel/Score").GetComponent<ScoreController>();
        Text scoreOutput = transitionCanvas.transform.Find("TransitionPanel/CoinsText").GetComponent<Text>();

        // записываем очки
        string currentScore = "Собрано вишенок: " + scoreController.score;
        scoreOutput.text = currentScore;
    }

    public void ShowTime()
    {
        // получаем время похождения уровня
        float levelTime = (float) Math.Round(Time.timeSinceLevelLoad, 3);
        // меняем рейтинг для текущего уровня
        ratingController.ChangeLevelScore(isHard, level - 1, ratingController.curPlayerName, levelTime);

        // выводим время прохождения
        Text timeOutput = transitionCanvas.transform.Find("TransitionPanel/TimeText").GetComponent<Text>();
        string currentTime = "Время: " + levelTime.ToString() + " сек.";
        timeOutput.text = currentTime;
    }

    public void SetLevel(int levelIndex)
    {
        level = levelIndex;
    }

    public void SetDifficulty(bool isHard)
    {
        this.isHard = isHard;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
