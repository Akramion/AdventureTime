using System.Collections;
using System.Collections.Generic;
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

        Debug.Log(ratingController.curPlayerName);

        float[] levelScores = ((RatingController.RecordData)
            ratingController.rating[ratingController.curPlayerName]).levelScores;

        int start = isHard ? SceneController.easyLevelsCount : 0;
        int end = isHard ? SceneController.totalLevelsCount : SceneController.easyLevelsCount;

        for (int i = start, j=1; i < end; i++, j++)
        {
            float levelScore = levelScores[i];

            // создаем префаб информации уровня
            GameObject levelInfo = Instantiate(levelInfoPrefab, transform);

            Text levelText = levelInfo.transform.Find("LevelButton/Text").GetComponent<Text>();
            levelText.text = j.ToString();

            Transform starsContainer = levelInfo.transform.Find("StarsContainer");

            for (int starIndex = 0; starIndex < Math.Ceiling(levelScore / 25.0); starIndex++)
            {
                Instantiate(starPrefab, starsContainer);
            }
        }
    }
}
