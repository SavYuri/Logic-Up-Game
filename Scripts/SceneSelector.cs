using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelector : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelsMenu;
    public GameObject gameMenu;
    public GameObject helpMenu;
    public GameObject winMenu;
    public GameObject informationMenu;

    public Button[] levelButtons;
    public Text [] textButton;
    public Text levelTextFromGameMenu;
    public GameObject [] imageButton;
    public GameObject buttonsParent;

    public WinConditions winConditions;
    public GameManager gameManager;

    public GameObject nextLevelButton;

    public InterstitialAdExample interstitialAd;
    public BannerAd bannerAd;

    


    private void Awake()
    {
        InitializeLevelButton();
    }

    private void Start()
    {
        GoToMainMenu();
    }

    void InitializeLevelButton()
    {
        levelButtons = new Button[buttonsParent.transform.childCount];
        textButton = new Text[buttonsParent.transform.childCount];
        imageButton = new GameObject[buttonsParent.transform.childCount];
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i] = buttonsParent.transform.GetChild(i).GetComponent<Button>();
            textButton[i] = levelButtons[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            imageButton[i] = levelButtons[i].gameObject.transform.GetChild(1).gameObject;

            textButton[i].text = (i + 1).ToString();
        }
    }

    void LevelButtonActivator()
    {
        int openLevel = PlayerPrefs.GetInt("OpenLevel");

        SetButtonCondition(openLevel+1);
     
    }

    void SetButtonCondition(int index)
    {
        for (int i = index; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
            imageButton[i].SetActive(true);
        }
        for (int i = 0; i < index; i++)
        {
            levelButtons[i].interactable = true;
            imageButton[i].SetActive(false);
        }
    }

    public void GoToLevelsMenu()
    {
        DisableAllScenes();
        LevelButtonActivator();
        levelsMenu.SetActive(true);
    }

    public void GoToMainMenu()
    {
        DisableAllScenes();
        mainMenu.SetActive(true);
    }

    public void StartGame()
    {
        DisableAllScenes();
        gameMenu.SetActive(true);
        gameManager.CancelItemLocation();
        bannerAd.ShowBannerAd();
    }

    void InitializeGameLevel(int correctLevel)
    {
        for( int i = 0; i < winConditions.conditionPanels.Length; i++)
        {
            winConditions.conditionPanels[i].SetActive(false);
        }

        winConditions.conditionPanels[correctLevel].SetActive(true);
        gameManager.CancelItemLocation();
        gameManager.currentGameLevel = correctLevel;
        Debug.Log(gameManager.currentGameLevel);
        levelTextFromGameMenu.text = "Level " + (correctLevel+1);
    }

    public void StartLevelFromMainMenu()
    {
        int level;
        level = PlayerPrefs.GetInt("OpenLevel");

        InitializeGameLevel(level);
        StartGame();
    }

    public void StartLevelFromLevelButton(int level)
    {
        InitializeGameLevel(level);
        StartGame();
    }

    

    public void StartLevelFromWinMenu()
    {
        
        int level;
        level = gameManager.currentGameLevel + 1;

        InitializeGameLevel(level);
        StartGame();
        interstitialAd.ShowIntAd();

    }

    public void OpenWinMenu()
    {
        bannerAd.HideBannerAd();
        winMenu.SetActive(true);
        if (gameManager.currentGameLevel != (levelButtons.Length-1))
        {
            nextLevelButton.SetActive(true);
        }
        else
        {
            nextLevelButton.SetActive(false);
        }
    }

    public void OpenHelpMenu()
    {
        helpMenu.SetActive(true);
    }

    public void CloseHelpMenu()
    {
        helpMenu.SetActive(false);
    }
    public void OpenInformationMenu()
    {
        informationMenu.SetActive(true);
    }

    public void CloseInformationMenu()
    {
        informationMenu.SetActive(false);
    }

    void DisableAllScenes()
    {
        mainMenu.SetActive(false);
        levelsMenu.SetActive(false);
        gameMenu.SetActive(false);
        helpMenu.SetActive(false);
        winMenu.SetActive(false);
        informationMenu.SetActive(false);

    }

    

   
}
