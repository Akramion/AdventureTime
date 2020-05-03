using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PopulateLevels : MonoBehaviour
{
    [SerializeField]
    private bool isHard;
    private string levelType;

    [SerializeField]
    private GameObject levelInfoPrefab;

    [SerializeField]
    private GameObject starPrefab;

    private RatingController ratingController;

    private void Awake()
    {
        ratingController = GameObject.Find("SceneController").GetComponent<RatingController>();
        levelType = isHard ? "Hard" : "Easy";
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        // удаляем предыдущие уровни
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        OrderedDictionary curRating = isHard ? ratingController.hardRating : ratingController.easyRating;

        RatingController.LevelProgress levelProgress = (RatingController.LevelProgress)
            curRating[ratingController.curPlayerName];
        
        for (int i = 0; i < levelProgress.levelScores.Length; i++)
        {
            // создаем префаб информации уровня
            GameObject levelInfo = Instantiate(levelInfoPrefab, transform);

            // номер уровня
            Text levelText = levelInfo.transform.Find("LevelButton/Text").GetComponent<Text>();
            levelText.text = (i + 1).ToString();

            // пройденные уровни - показываем счет, активируем кнопку
            // промежуточные уровни (доступен, но еще не был пройден) - активируем кнопку
            // не пройденные - деактивируем кнопку

            // мы должны проходить уровни по порядку, поэтому сделаем неактивными кнопки закрытых уровней
            if (i > levelProgress.levelsPassed)
            {
                Button b = levelInfo.transform.Find("LevelButton").GetComponent<Button>();
                b.interactable = false;
            }
            // для пройденных покажем полученные очки.
            if (i < levelProgress.levelsPassed)
            {
                // очки
                float levelScore = levelProgress.levelScores[i];

                // время прохождения
                Text timeText = levelInfo.transform.Find("InfoContainer/TimeText").GetComponent<Text>();
                timeText.text = levelScore.ToString() + " сек.";

                //Transform starsContainer = levelInfo.transform.Find("StarsContainer");
                //for (int starIndex = 0; starIndex < Math.Ceiling(levelScore / 25.0); starIndex++)
                //{
                //Instantiate(starPrefab, starsContainer);
                //}
            }
        }
    }
}
