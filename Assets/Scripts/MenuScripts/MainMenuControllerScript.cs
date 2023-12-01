using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuControllerScript : MonoBehaviour
{
    [SerializeField] private Animator theAnimator;
    public UnityEngine.UI.Slider graphicsSlider;
    public UnityEngine.UI.Slider soundSlider;
    public UnityEngine.UI.Slider difficultySlider;
    public float graphicsThreshLow = 0.33f;
    public float graphicsThreshMidlow = 0.34f;
    public float graphicsThreshMidhigh = 0.65f;
    public float graphicsThreshMax = 0.66f;
    public SOGraphics graphicV;
    public TMPro.TextMeshProUGUI graphicsText;
    public TMPro.TextMeshProUGUI difficultyText;
    public AudioMixer audioMixxy1;
    public int nextSceneToLoad = 1;
    public SOSoundLevel soundLevelV;
    public SODifficulty difficultyValue;


    private void Start()
    {
        theAnimator = GetComponent<Animator>();
        //graphicsSlider = GameObject.Find("GraphicsSlider").GetComponent<UnityEngine.UI.Slider>();
        //graphicsSlider = graphicsSliderObject.GetComponent<SliderInt>();
        graphicsSlider.value = graphicV.graphicsValue;
        QualitySet();
        difficultySlider.value = difficultyValue.difficulty;
        DifficultySetting();
        soundSlider.value = soundLevelV.soundLevel;
        AudioMixerSetting();
        //graphicsText = GameObject.Find("TextToShowGLevel").GetComponent<TMPro.TextMeshProUGUI>();
    }
    public void StartTheGame()
    {
        SceneManager.LoadScene(nextSceneToLoad);

    }

    public void OptionsOpen()
    {
        theAnimator.SetBool("OptionsOpen",true);
        theAnimator.SetBool("GraphicsOpen", false);
        theAnimator.SetBool("MainMenuOpen", false);
        theAnimator.SetBool("CreditsOpen", false);
        theAnimator.SetBool("SoundOpen", false);
        theAnimator.SetBool("OpGameOpen", false);
        theAnimator.SetBool("ControlsOpen", false);

    }

    public void OptionsClose()
    {
        theAnimator.SetBool("OptionsOpen", false);
        theAnimator.SetBool("MainMenuOpen", true);

    }

    public void CreditsButton()
    {
        theAnimator.SetBool("CreditsOpen", true);
        theAnimator.SetBool("MainMenuOpen", false);
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }

    public void CrBackButton()
    {
        theAnimator.SetBool("MainMenuOpen", true);
        theAnimator.SetBool("CreditsOpen", false);
    }


    // Options Begin-------------------
    public void OpGameButton()
    {
        theAnimator.SetBool("OpGameOpen", true);
        theAnimator.SetBool("OptionsOpen", false);
        theAnimator.SetBool("MainMenuOpen", false);

    }

    public void OpGraphicsButton()
    {
        theAnimator.SetBool("GraphicsOpen", true);
        theAnimator.SetBool("OptionsOpen", false);
        theAnimator.SetBool("MainMenuOpen", false);
    }

    public void OpSoundButton()
    {
        theAnimator.SetBool("SoundOpen", true);
        theAnimator.SetBool("OptionsOpen", false);
        theAnimator.SetBool("MainMenuOpen", false);
    }

    public void OpControlsButton()
    {
        theAnimator.SetBool("ControlsOpen", true);
        theAnimator.SetBool("OptionsOpen", false);
        theAnimator.SetBool("MainMenuOpen", false);
        
    }

    public void DifficultySetting()
    {
        difficultyValue.difficulty = Mathf.Round(difficultySlider.value);
        if (difficultyValue.difficulty == 1.0f)
        {
            difficultyText.text = "Normal";
        }
        if (difficultyValue.difficulty == 2.0f)
        {
            difficultyText.text = "Hard";
        }
        if (difficultyValue.difficulty == 3.0f)
        {
            difficultyText.text = "Very Hard";
        }
    }

    //Sounds Begin--------------------

    public void AudioMixerSetting()
    {
        //float currentvolume = math.remap(1,0,0,80,Mathf.Pow(soundSlider.value,2));
        soundLevelV.soundLevel = Mathf.Log10(soundSlider.value) * 20;
        audioMixxy1.SetFloat("volumeOfMixer", soundLevelV.soundLevel);

    }

    //Sounds End----------------------

    public void QualitySet()
    {
        graphicV.graphicsValue =  Convert.ToInt32(Mathf.Round(graphicsSlider.value));
        if(graphicV.graphicsValue == 2)
        {
            //graphicV.graphicsValue = 2;
            graphicsText.text = "High";
        }
        if (graphicV.graphicsValue == 1)
        {
            //graphicV.graphicsValue = 1;
            graphicsText.text = "Medium";
        }
        if (graphicV.graphicsValue == 0)
        {
            //graphicV.graphicsValue = 0;
            graphicsText.text = "Low";
        }
        QualitySettings.SetQualityLevel(graphicV.graphicsValue);
    }
    //Options End----------------------
}