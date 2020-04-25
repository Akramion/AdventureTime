using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameHolder : MonoBehaviour
{
    private RatingController ratingController;
    public Color selectedColor = new Color(165 / 255, 1, 168 / 255);

    void Start()
    {
        NameList nameList = transform.parent.parent.parent.parent.GetComponent<NameList>();
        Button b = GetComponent<Button>();
        b.onClick.AddListener(() => nameList.Select(gameObject));
    }
}
