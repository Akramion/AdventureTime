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

    [SerializeField]
    private GameObject levelInfoPrefab;

    [SerializeField]
    private GameObject starPrefab;

    private RatingController ratingController;

    // Start is called before the first frame update
    void Start()
    {
        ratingController = GameObject.Find("SceneController").GetComponent<RatingController>();
        string levelType = isHard ? "Hard" : "Easy";

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

            // мы должны проходить уровни по порядку, поэтому сделаем неактивными кнопки закрытых уровней
            if (i > levelProgress.levelsPassed)
            {
                Button b = levelInfo.transform.Find("LevelButton").GetComponent<Button>();
                b.interactable = false;
            }
            // для пройденных же активируем кнопку и покажем полученные очки
            else
            {
                // очки
                float levelScore = levelProgress.levelScores[i];
                
                Transform starsContainer = levelInfo.transform.Find("StarsContainer");
                for (int starIndex = 0; starIndex < Math.Ceiling(levelScore / 25.0); starIndex++)
                {
                    Instantiate(starPrefab, starsContainer);
                }
            }
        }
    }
}
