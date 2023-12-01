using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    PlayerInput playerInput;
    private bool isContinuePressed = false;
    private float sceneAdvanceDebounceAmount = 1.0f;
    private float sceneAdvanceLastTime;
    private int currentCutScene;
    public int nextSceneToLoad = 0;

    //thePictures and theText MUST be the same array length
    [SerializeField] GameObject[] thePictures;
    [SerializeField] GameObject[] theText;

    private void Awake()
    {
        //QualitySettings.vSyncCount = 3;
        playerInput = new PlayerInput();
        playerInput.UI.Continue.started += OnContinue;
        playerInput.UI.Continue.canceled += OnContinue;
        sceneAdvanceLastTime = Time.time;
        for (int i = 0; i < thePictures.Length; i++)
        {
            thePictures[i].SetActive(false);
        }
        currentCutScene = -1;
    }

    private void OnContinue(InputAction.CallbackContext context)
    {
        isContinuePressed = context.ReadValueAsButton();
        if (isContinuePressed && Time.time > sceneAdvanceLastTime + sceneAdvanceDebounceAmount)
        {
            AdvanceCutscene();
            sceneAdvanceLastTime = Time.time;
        }
    }

    private void AdvanceCutscene()
    {
        currentCutScene++;
        if(currentCutScene < thePictures.Length)
        {
            for (int i = 0; i < thePictures.Length; i++)
            {
                thePictures[i].SetActive(false);
                theText[i].SetActive(false);
            }
            thePictures[currentCutScene].SetActive(true);
            theText[currentCutScene].SetActive(true);
        }
        if (currentCutScene >= thePictures.Length)
        {
            SceneManager.LoadScene(nextSceneToLoad);
        }
    }

    void OnEnable()
    {
        playerInput.UI.Enable();
    }

    void OnDisable()
    {
        playerInput.UI.Disable();
    }

}