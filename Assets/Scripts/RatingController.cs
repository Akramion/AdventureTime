using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Collections.Specialized;
using UnityEngine;

public class RatingController : MonoBehaviour
{
    [SerializeField]
    private string fileName;

    // рейтинг учитывает topSize лучших игроков
    [SerializeField]
    private int topSize;

    private static int levels = 5;

    // отсортированные записи таблицы рейтинга
    public OrderedDictionary rating = new OrderedDictionary();

    public class RecordData
    {
        public float[] levelScores;
        public float totalScore;

        public RecordData()
        {
            levelScores = new float[levels];

            for (int i = 0; i < levels; ++i)
            {
                levelScores[i] = 0;
            }

            totalScore = 0;
        }

        public RecordData(float[] levelScores)
        {
            this.levelScores = levelScores;

            totalScore = 0;
            foreach (float levelScore in levelScores)
            {
                totalScore += levelScore;
            }
        }

        public bool ChangeLevelScore(int levelIndex, float score)
        {
            // меняем очки только если их больше, чем старых
            if (score > levelScores[levelIndex])
            {
                // вычитаем старые очки суммы всех очков, тем самым обнуляя долю текущего уровня
                // затем добавляем новые очки (обновляем)
                totalScore += score - levelScores[levelIndex];

                // для конкретного уровня меняем очки
                levelScores[levelIndex] = score;

                return true;
            }

            return false;
        }
    }

    // подгружаем рейтинг из файла перед началом игры
    public void Awake()
    {
        LoadFromFile();
        AddPlayer("CR@zy d0g");
        AddPlayer("Sobaka228");
        AddPlayer("Nosik");
        AddPlayer("Bobil");
        AddPlayer("Keny");
        AddPlayer("Shushkan");
        AddPlayer("Ruport");
        AddPlayer("Keks");
        AddPlayer("Jiga");

        RecalculateRating(1, "CR@zy d0g", 100);
        RecalculateRating(4, "CR@zy d0g", 50);
        RecalculateRating(1, "Sobaka228", 100);
        RecalculateRating(2, "Sobaka228", 40);
        RecalculateRating(1, "Nosik", 100);
        RecalculateRating(2, "Nosik", 30);
        RecalculateRating(1, "Bobil", 100);
        RecalculateRating(2, "Bobil", 20);
        RecalculateRating(1, "Keny", 100);
        RecalculateRating(2, "Keny", 10);
        RecalculateRating(1, "Shushkan", 50);
        RecalculateRating(2, "Shushkan", 20);

        SaveToFile();
    }

    public bool AddPlayer(string playerName)
    {
        if (rating.Contains(playerName))
            return false;

        rating.Add(playerName, new RecordData());
        return true;
    }

    public void RecalculateRating(int levelIndex, string playerName, float levelScore)
    {
        RecordData updatedRecord = rating[playerName] as RecordData;
        bool isChanged = updatedRecord.ChangeLevelScore(levelIndex, levelScore);

        // если новые очки оказались больше старых, то нужно пересчитать таблицу рейтинга
        if (isChanged)
        {
            int insertIndex = -1;
            int i = 0;
            foreach (string curPlayerName in rating.Keys)
            {
                RecordData curRecord = rating[i] as RecordData;

                // если игрок встречается в проверке, то это означает, что
                // после изменения очков он не улучшил положение в рейтинге
                if (playerName == curPlayerName)
                    break;

                // найдена запись выше по рейтингу, которая имеет меньше очков
                // запомним индекс, на который позже встанем
                if (curRecord.totalScore < updatedRecord.totalScore)
                {
                    insertIndex = i;
                    break;
                }

                i++;
            }

            // игрок улучшил положение в рейтинге
            if (insertIndex != -1)
            {
                // уберем его со старой позиции
                rating.Remove(playerName);
                // перенесем данные на новую позицию в рейтинге
                rating.Insert(insertIndex, playerName, updatedRecord);
            }
        }
    }

    public void ClearRecords()
    {
        rating.Clear();
    }

    private void LoadFromFile()
    {
        ClearRecords();

        string filePath = Application.persistentDataPath + "/" + fileName;
        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            foreach (string recordLine in File.ReadLines(filePath))
            {
                string[] data = recordLine.Split('|');

                string playerName = data[0];

                string[] unparsedLevelScores = data[1].Split(' ');
                float[] levelScores = new float[levels];

                for (int i = 0; i < unparsedLevelScores.Length; i++)
                {
                    levelScores[i] = float.Parse(unparsedLevelScores[i]);
                }

                rating.Add(playerName, new RecordData(levelScores));
            }
        }
    }

    public void SaveToFile()
    {
        string filePath = Application.persistentDataPath + "/" + fileName;

        string saveText = "";

        foreach (string playerName in rating.Keys)
        {
            RecordData recordData = rating[playerName] as RecordData;
            saveText += playerName + "|";
            saveText += string.Join(" ", recordData.levelScores);
            saveText += "\n";
        }

        File.WriteAllText(filePath, saveText);
    }
}
