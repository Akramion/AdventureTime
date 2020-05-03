using UnityEngine;
using UnityEngine.UI;

public class NameList : MonoBehaviour
{
    private Transform container;
    private RatingController ratingController;
    public Color selectedColor;
    public Color unselectedColor;
    private GameObject selectedNameHolder;

    [SerializeField]
    private GameObject nameHolderPrefab;

    void Start()
    {
        ratingController = GameObject.Find("SceneController").GetComponent<RatingController>();
        container = transform.Find("Scroll View/Viewport/Content");

        FillNameList();
    }

    public void FillNameList()
    {
        // можно брать имена из любого рейтинга, они оба содеражат одинаковые имена
        foreach (string playerName in ratingController.easyRating.Keys)
        {
            GameObject inst = AppendPlayer(playerName);

            if (ratingController.curPlayerName == playerName)
            {
                SetColor(inst, selectedColor);
                selectedNameHolder = inst;
            }
        }
    }

    public void ClearNameList()
    {
        // очищаем лист
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }

    public void Select(GameObject nameHolder)
    {
        if (nameHolder != selectedNameHolder)
        {
            // выделяем зеленым цветом
            SetColor(nameHolder, selectedColor);

            // убираем выделение у предыдущего выделенного блока
            SetColor(selectedNameHolder, unselectedColor);

            selectedNameHolder = nameHolder;
            ratingController.curPlayerName = nameHolder.transform.Find("Text").GetComponent<Text>().text;
        }
    }

    public void SetColor(GameObject nameHolder, Color color)
    {
        nameHolder.GetComponent<Image>().color = color;
    }

    public GameObject AppendPlayer(string playerName)
    {
        GameObject inst = Instantiate(nameHolderPrefab, container);

        NameHolder nameHolder = inst.GetComponent<NameHolder>();
        Text text = inst.transform.Find("Text").GetComponent<Text>();

        text.text = playerName;

        return inst;
    }
}
