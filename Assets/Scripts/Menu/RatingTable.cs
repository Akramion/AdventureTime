using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.UI;
using UnityEngine;

public class RatingTable : MonoBehaviour
{
    private Transform container;
    private RatingController ratingController;

    [SerializeField]
    private GameObject ratingRecordPrefab;

    private DifficultyButton easyDifficultButton;

    private bool isLoadedHard = false;
    
    void Awake()
    {
        ratingController = GameObject.Find("SceneController").GetComponent<RatingController>();
        easyDifficultButton = transform.Find("DifficultyPanel/EasyRatingButton").GetComponent<DifficultyButton>();
        container = transform.Find("Scroll View/Viewport/Content");
    }

    void OnEnable()
    {
        easyDifficultButton.SetSelected();
        FillTable(false);
    }

    public void FillTable(bool fillWithHard)
    {
        // чистим таблицу
        ClearTable();

        OrderedDictionary curRating = fillWithHard ? ratingController.hardRating : ratingController.easyRating;

        float[] levelScores = ((RatingController.LevelProgress)
            curRating[ratingController.curPlayerName]).levelScores;

        // заполняем таблицу записями из рейтинга
        int i = 0;
        foreach (string playerName in curRating.Keys)
        {
            RatingController.LevelProgress levelProgress = curRating[i] as RatingController.LevelProgress;
            GameObject tableRecord = Instantiate(ratingRecordPrefab, container);

            Text place = tableRecord.transform.Find("Place").GetComponent<Text>();
            place.text = (i + 1).ToString();

            Text name = tableRecord.transform.Find("Name").GetComponent<Text>();
            name.text = playerName;

            Text time= tableRecord.transform.Find("Time").GetComponent<Text>();
            
            if (levelProgress.totalScore == 0)
            {
                time.text = "?";
            }
            else
            {
                time.text = levelProgress.totalScore.ToString();
            }

            Text levels = tableRecord.transform.Find("Levels").GetComponent<Text>();
            levels.text = levelProgress.levelsPassed.ToString();

            i++;
        }
    }

    public void ClearTable()
    {
        // очищаем таблицу
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
