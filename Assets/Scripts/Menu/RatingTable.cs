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
    
    void Start()
    {
        ratingController = GameObject.Find("Supervisor").GetComponent<RatingController>();
        container = transform.Find("Scroll View/Viewport/Content");

        fillTable();
    }

    public void fillTable()
    {
        // заполняем таблицу записями из рейтинга
        for (int i = 0; i < ratingController.records.Count; i++)
        {
            RatingController.RecordData recordData = ratingController.records[i];
            GameObject tableRecord = Instantiate(ratingRecordPrefab, container);

            Text place = tableRecord.transform.Find("Place").GetComponent<Text>();
            place.text = (i + 1).ToString();

            Text name = tableRecord.transform.Find("Name").GetComponent<Text>();
            name.text = recordData.name;

            Text score = tableRecord.transform.Find("Score").GetComponent<Text>();
            score.text = recordData.score.ToString();
        }
    }

    public void clearTable()
    {
        // очищаем таблицу
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
