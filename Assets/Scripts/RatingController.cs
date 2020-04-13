using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RatingController : MonoBehaviour
{
    [SerializeField]
    private string fileName;

    // рейтинг учитывает topSize лучших игроков
    [SerializeField]
    private int topSize;

    // отсортированные записи таблицы рейтинга
    public List<RecordData> records;

    public struct RecordData
    {
        public string name;
        public float[] levelScores;
        public float totalScore;

        public RecordData(string name, float[] levelScores, float totalScore)
        {
            this.name = name;
            this.levelScores = levelScores;
            this.totalScore = totalScore;
        }
    }

    // подгружаем рейтинг из файла перед началом игры
    public void Awake()
    {
        records = new List<RecordData>();
        loadFromFile();
    }

    public void addRecord(string name, float[] levelScores)
    {
        float totalScore = 0;
        foreach(float levelScore in levelScores)
        {
            totalScore += levelScore;
        }

        int insertIndex = -1;
        for (int i = 0; i < records.Count; i++)
        {
            RecordData record = records[i];
            if (record.totalScore < totalScore)
            {
                insertIndex = i;
                break;
            }
        }

        // место для новой записи где-то между старыми записями
        if (insertIndex != -1)
        {
            // вставляем запись
            records.Insert(insertIndex, new RecordData(name, levelScores, totalScore));

            // если топ переполнен, значит какая-то запись вытеснена.
            // Удалим эту запись, чтобы сохранить размер топа
            if (records.Count == topSize)
            {
                records.RemoveAt(records.Count - 1);
            }
        }
        // место для новой записи после старых записей
        // добавим, если топ не переполнен старыми записями
        else
        if (records.Count < topSize)
        {
            records.Add(new RecordData(name, levelScores, totalScore));
        }
    }

    public void clearRecords()
    {
        records.Clear();
    }

    private void loadFromFile()
    {
        clearRecords();

        string filePath = Application.persistentDataPath + "|" + fileName;
        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            foreach (string recordLine in File.ReadLines(filePath))
            {
                string[] data = recordLine.Split('/');
                string name = data[0];

                float score = float.Parse(data[1]);

                records.Add(new RecordData(name, levelScores, score));
            }
        }
    }

    public void saveToFile()
    {
        string filePath = Application.persistentDataPath + "/" + fileName;

        string[] recordLines = new string[records.Count];
        for (int i = 0; i < records.Count; i++)
        {
            RecordData record = records[i];
            recordLines[i] = record.name + "|" + record.score.ToString();
        }

        File.WriteAllLines(filePath, recordLines);
    }
}
