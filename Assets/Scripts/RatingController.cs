using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Collections.Specialized;
using UnityEngine;

public class RatingController : MonoBehaviour
{
    [SerializeField]
    private string fileName;

    public string curPlayerName = "Аноним";
    public bool recalculateTable = true;

    // отсортированные записи таблицы рейтинга
    public OrderedDictionary rating = new OrderedDictionary();

    public class RecordData
    {
        public float[] levelScores;
        public float totalScore;

        public RecordData()
        {
            levelScores = new float[SceneController.totalLevelsCount];

            for (int i = 0; i < SceneController.totalLevelsCount; ++i)
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
        SceneController sceneController = GetComponent<SceneController>();
        LoadFromFile();
        // добавляем анонимного игрока
        AddPlayer("Аноним");
    }

    public bool AddPlayer(string playerName)
    {
        if (rating.Contains(playerName))
            return false;

        rating.Add(playerName, new RecordData());
        recalculateTable = true;
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
                recalculateTable = true;
            }
        }
    }

    public void Clear()
    {
        rating.Clear();
    }

    private void LoadFromFile()
    {
        Clear();

        string filePath = Application.persistentDataPath + "/" + fileName;
        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            foreach (string recordLine in File.ReadLines(filePath))
            {
                string[] data = recordLine.Split('|');

                string playerName = data[0];

                string[] unparsedLevelScores = data[1].Split(' ');
                float[] levelScores = new float[SceneController.totalLevelsCount];

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
            // исключаем анонимного игрока из процесса сохранения
            if (playerName == "Аноним")
                continue;

            RecordData recordData = rating[playerName] as RecordData;
            saveText += playerName + "|";
            saveText += string.Join(" ", recordData.levelScores);
            saveText += "\n";
        }

        File.WriteAllText(filePath, saveText);
    }

    void OnApplicationQuit()
    {
        SaveToFile();
    }
}
