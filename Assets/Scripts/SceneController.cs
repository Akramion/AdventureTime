using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int level = 0;
    public bool isHard = false;

    private GameObject blackoutCanvas;

    private bool isBlackout = false;
    public GameObject alphaObjBlackout;
    private Image alphaImageBlackout;
    private float alphaABlackout;

    public GameObject alphaObjTransition;
    private Image alphaImageTransition;
    private float alphaATransition;

    public GameObject alphaObjRestart;
    private Image alphaImageRestart;
    public GameObject alphaObjNextLevel;
    private Image alphaImageNextLevel;

    private GameObject person;

    


    private void Awake() {
        DontDestroyOnLoad(this);
        blackoutCanvas = GameObject.Find("Transition");
        alphaImageBlackout = alphaObjBlackout.GetComponent<Image>();
        alphaImageTransition = alphaObjTransition.GetComponent<Image>();
        alphaImageNextLevel = alphaObjNextLevel.GetComponent<Image>();
        alphaImageRestart = alphaObjRestart.GetComponent<Image>();
    }

    private void Update() {
        if(isBlackout == true) {
            Blackout();
        }

        else {
            UnsetBlackout();
        }
    }

    public void NextLevel() {
        level++;
        blackoutCanvas.SetActive(false);
        if (isHard)
        {
            SceneManager.LoadScene("Level_Hard_" + level, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Level_Easy_" + level, LoadSceneMode.Single);
        }

        isBlackout = false;
    }

    public void RestartLevel() {
        blackoutCanvas.SetActive(false);
        if (isHard)
        {
            SceneManager.LoadScene("Level_Hard_" + level, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Level_Easy_" + level, LoadSceneMode.Single);
        }

        isBlackout = false;
    }

    public void SetDificulty(bool isHard)
    {
        this.isHard = isHard;
    }

    public void TurnOfButtons() {
        blackoutCanvas.SetActive(true);
    }

    public void SetBlackot() {
        isBlackout = !isBlackout;
        
        if(isBlackout == true){
            DestroyPerson();
        }   
     }

    private void UnsetBlackout() {
        alphaABlackout = 0f;
        alphaATransition = 0f;
        alphaImageTransition.color = new Color(alphaImageTransition.color.r, alphaImageTransition.color.g, alphaImageTransition.color.b, alphaABlackout);
        alphaImageNextLevel.color = new Color(alphaImageNextLevel.color.r, alphaImageNextLevel.color.g, alphaImageNextLevel.color.b, alphaABlackout);
        alphaImageRestart.color = new Color(alphaImageRestart.color.r, alphaImageRestart.color.g, alphaImageRestart.color.b, alphaABlackout);
        alphaImageBlackout.color = new Color(alphaImageBlackout.color.r, alphaImageBlackout.color.g, alphaImageBlackout.color.b, alphaABlackout);
    }

    private void DestroyPerson() {
        person = GameObject.Find("Character").gameObject;
        Destroy(person);
    }

    public void BlackoutSetActive() {
        alphaObjBlackout.SetActive(true);
    }

    private void Blackout() {
        
        if(alphaABlackout < 0.7f) {
            alphaABlackout += 0.7f * Time.deltaTime;
            alphaImageBlackout.color = new Color(alphaImageBlackout.color.r, alphaImageBlackout.color.g, alphaImageBlackout.color.b, alphaABlackout);
        }

        if(alphaABlackout > 0.7f) {
            ShowTransition();
        }
    }

    private void ShowTransition() {
        if(alphaATransition < 1) {
            alphaATransition += 0.7f * Time.deltaTime;


            alphaImageTransition.color = new Color(alphaImageTransition.color.r, alphaImageTransition.color.g, alphaImageTransition.color.b, alphaATransition);
            alphaImageNextLevel.color = new Color(alphaImageNextLevel.color.r, alphaImageNextLevel.color.g, alphaImageNextLevel.color.b, alphaATransition);
            alphaImageRestart.color = new Color(alphaImageRestart.color.r, alphaImageRestart.color.g, alphaImageRestart.color.b, alphaATransition);
        }        
    }
}
