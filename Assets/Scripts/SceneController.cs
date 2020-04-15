using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int level = 0;
    public bool isHard = false;

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    public void NextLevel() {
        level++;
        Debug.Log("Level_" + level);

        if (isHard)
        {
            SceneManager.LoadScene("Level_Hard_" + level, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Level_Easy_" + level, LoadSceneMode.Single);
        }
    }

    public void RestartLevel() {
        if (isHard)
        {
            SceneManager.LoadScene("Level_Hard_" + level, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Level_Easy_" + level, LoadSceneMode.Single);
        }
    }

    public void SetDificulty(bool isHard)
    {
        this.isHard = isHard;
    }
}
