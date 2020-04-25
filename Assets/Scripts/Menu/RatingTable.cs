using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RatingTable : MonoBehaviour
{
    private Transform container;
    private RatingController ratingController;

    [SerializeField]
    private GameObject ratingRecordPrefab;
    
    void Awake()
    {
        ratingController = GameObject.Find("SceneController").GetComponent<RatingController>();
        container = transform.Find("Scroll View/Viewport/Content");
    }

    void OnEnable()
    {
        FillTable();
    }

    public void FillTable()
    {
        // обновляем таблицу, только когда в рейтинге есть изменения
        if (!ratingController.recalculateTable)
            return;

        ratingController.recalculateTable = false;

        // чистим таблицу
        ClearTable();

        // заполняем таблицу записями из рейтинга
        int i = 0;
        foreach (string playerName in ratingController.rating.Keys)
        {
            RatingController.RecordData recordData = ratingController.rating[i] as RatingController.RecordData;
            GameObject tableRecord = Instantiate(ratingRecordPrefab, container);

            Text place = tableRecord.transform.Find("Place").GetComponent<Text>();
            place.text = (i + 1).ToString();

            Text name = tableRecord.transform.Find("Name").GetComponent<Text>();
            name.text = playerName;

            Text score = tableRecord.transform.Find("Score").GetComponent<Text>();
            score.text = recordData.totalScore.ToString();

            i++;
        }
    }

    public void ClearTable()
    {
        // очищаем таблицу
        Debug.Log(container);
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
