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

    // отсортированный рейтинг
    // 2 вида: легий и сложный. Разделены для объективности оценки
    public OrderedDictionary easyRating = new OrderedDictionary();
    public OrderedDictionary hardRating = new OrderedDictionary();

    // класс, содержащий прогресс игрока по какому-либо рейтингу (легкому или сложному)
    public class LevelProgress
    {
        // время, за которое игрок прошел уровень. Если 0, то не проходил.
        public float[] levelScores;
        public float totalScore;

        // количество пройденных уровней
        public int levelsPassed;

        // этот конструктор нужен, чтобы инициализировать пустой прогресс
        public LevelProgress(int levelCount)
        {
            levelScores = new float[levelCount];

            for (int i = 0; i < levelCount; ++i)
            {
                levelScores[i] = 0;
            }

            totalScore = 0;
            levelsPassed = 0;
        }

        // этот конструктор нужен, чтобы инициализировать прогресс на основе массива
        public LevelProgress(float[] levelScores)
        {
            this.levelScores = levelScores;

            totalScore = 0;
            for (int i = 0; i < levelScores.Length; i++)
            {
                float levelScore = levelScores[i];
                if (levelScore > 0)
                {
                    levelsPassed++;
                    totalScore += levelScore;
                }
            }
        }

        public bool ChangeLevelScore(int levelIndex, float score)
        {
            float oldScore = levelScores[levelIndex];
            
            // если не проходили этот уровень
            if (oldScore == 0)
            {
                Debug.Log("new");
                // прибавим к общему количеству заработанные очки
                totalScore += score;

                // для конкретного уровня меняем очки
                levelScores[levelIndex] = score;

                // Добавим 1 к общему количеству пройденных уровней, чтобы в меню открылся новый уровень
                levelsPassed++;
                return true;
            }
            else
            // Если проходили, то меняем очки только если их меньше, чем старых
            if (score < oldScore)
            {
                Debug.Log("old");
                // вычитаем старые очки суммы всех очков, тем самым обнуляя долю текущего уровня
                // затем добавляем новые очки (обновляем)
                totalScore += score - oldScore;

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
        AddPlayer("Вася");
        AddPlayer("Ваня");

        ChangeLevelScore(true, 0, "Вася", 10);
        ChangeLevelScore(true, 0, "Ваня", 5);

        ChangeLevelScore(true, 1, "Вася", 10);
        ChangeLevelScore(true, 1, "Ваня", 10);

        ChangeLevelScore(false, 0, "Вася", 5);
        ChangeLevelScore(false, 0, "Ваня", 10);

        SceneController sceneController = GetComponent<SceneController>();
        // LoadFromFile();
        // добавляем анонимного игрока
        AddPlayer("Аноним");
    }

    public bool AddPlayer(string playerName)
    {
        // достаточно проверить наличия игрока только в одном из рейтингов, чтобы убедиться,
        // что он есть или его нет в другом
        if (easyRating.Contains(playerName))
            return false;

        // создаем разные прогрессы для разных типов уровней
        easyRating.Add(playerName, new LevelProgress(SceneController.easyLevelsCount));
        hardRating.Add(playerName, new LevelProgress(SceneController.hardLevelsCount));

        recalculateTable = true;
        return true;
    }

    public void ChangeLevelScore(bool isHard, int levelIndex, string playerName, float levelScore)
    {
        // выбираем нужный для изменения рейтинг
        OrderedDictionary curRating = isHard ? hardRating : easyRating;

        LevelProgress curProgress = curRating[playerName] as LevelProgress;
        bool isChanged = curProgress.ChangeLevelScore(levelIndex, levelScore);

        // если новые очки оказались больше старых, то нужно пересчитать таблицу рейтинга
        if (isChanged)
        {
            int insertIndex = -1;
            int i = 0;

            foreach (string curPlayerName in curRating.Keys)
            {
                LevelProgress otherProgress = curRating[i] as LevelProgress;

                // если игрок встречается в проверке, то это означает, что
                // после изменения очков он не улучшил положение в рейтинге
                if (playerName == curPlayerName)
                    break;

                // найдена запись выше по рейтингу, которая имеет меньше очков
                // запомним индекс, на который позже встанем
                if (curProgress.levelsPassed >= otherProgress.levelsPassed &&
                    curProgress.totalScore < otherProgress.totalScore)
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
                curRating.Remove(playerName);
                // перенесем данные на новую позицию в рейтинге
                curRating.Insert(insertIndex, playerName, curProgress);
                recalculateTable = true;
            }
        }
    }

    public void Clear()
    {
        easyRating.Clear();
        hardRating.Clear();
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

                // из общего количества уровней сначала идут легкие
                float[] levelScores = new float[SceneController.easyLevelsCount];
                for (int i = 0, j = 0; i < SceneController.easyLevelsCount; i++, j++)
                {
                    levelScores[j] = float.Parse(unparsedLevelScores[i]);
                }
                easyRating.Add(playerName, new LevelProgress(levelScores));

                // потом сложные
                levelScores = new float[SceneController.hardLevelsCount];
                for (int i = SceneController.hardLevelsCount, j = 0; i < SceneController.totalLevelsCount; i++, j++)
                {
                    levelScores[j] = float.Parse(unparsedLevelScores[i]);
                }
                hardRating.Add(playerName, new LevelProgress(levelScores));
            }
        }
    }

    public void SaveToFile()
    {
        string filePath = Application.persistentDataPath + "/" + fileName;

        string saveText = "";

        foreach (string playerName in easyRating.Keys)
        {
            // исключаем анонимного игрока из процесса сохранения
            if (playerName == "Аноним")
                continue;

            // сначала заполняем легкими уровнями, потом сложными
            LevelProgress easyProgress = easyRating[playerName] as LevelProgress;
            LevelProgress hardProgress = hardRating[playerName] as LevelProgress;

            saveText += playerName + "|";
            saveText += string.Join(" ", easyProgress.levelScores);
            saveText += string.Join(" ", hardProgress.levelScores);
            saveText += "\n";
        }

        File.WriteAllText(filePath, saveText);
    }

    void OnApplicationQuit()
    {
        SaveToFile();
    }
}
