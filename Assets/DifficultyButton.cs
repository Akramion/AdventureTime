using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private static Color unselectedColor = new Color(1, 1, 1, 0.6f);
    private static Color selectedColor = new Color(1, 1, 1, 0.8f);

    // Start is called before the first frame update
    void Start()
    {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(() => SetSelected());
    }

    public void SetSelected()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.gameObject != gameObject)
            {
                child.GetComponent<Image>().color = unselectedColor;
                break;
            }
        }

        GetComponent<Image>().color = selectedColor;
        
    }
}
