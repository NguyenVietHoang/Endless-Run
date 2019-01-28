using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class MainView : MonoBehaviour
{
    [Header("Manager")]
    public GameplayController manager;

    [Header ("UI Tab")]
    public GameObject startGameTab;
    public GameObject pauseTab;
    public GameObject gameOverTab;
    public GameObject leaderBoardTab;
    public GameObject mainUITab;

    [Header("Start Game UI")]
    public Button srtTabStartBtn;
    public Button srtTabQuitBtn;

    [Header("Pause Tab UI")]
    public Button pauseTabResumeBtn;
    public Button pauseTabQuitBtn;

    [Header("Game over UI")]
    public PlayableDirector goAnim;
    public Text goScoreTxt;
    public Button goReplayBtn;
    public Button goQuitBtn;

    [Header("Main UI Tab")]
    public PlayableDirector mainTutorial;
    public GameObject mainTutorialTab;
    public Text mainScoreValueTxt;

    //Container all the tab, easy "foreach" all the elements
    List<GameObject> tabs;

    private void Awake()
    {
        tabs = new List<GameObject>
        {
            startGameTab,
            pauseTab,
            gameOverTab,
            leaderBoardTab,
            mainUITab
        };
    }

    //Initialize the view, called by the manager
    public void Init()
    {
        TurnOffAllTabs();
        startGameTab.SetActive(true);
        mainTutorialTab.SetActive(false);
        UpdateMainUIScore(0);

        //Assign all Button event to the UI
        srtTabStartBtn.onClick.RemoveAllListeners();
        srtTabStartBtn.onClick.AddListener(manager.StartGame);

        srtTabQuitBtn.onClick.RemoveAllListeners();
        srtTabQuitBtn.onClick.AddListener(manager.QuitGame);

        pauseTabResumeBtn.onClick.RemoveAllListeners();
        pauseTabResumeBtn.onClick.AddListener(manager.UnpauseGame);

        pauseTabQuitBtn.onClick.RemoveAllListeners();
        pauseTabQuitBtn.onClick.AddListener(manager.QuitGame);

        goReplayBtn.onClick.RemoveAllListeners();
        goReplayBtn.onClick.AddListener(manager.ResetGame);

        goQuitBtn.onClick.RemoveAllListeners();
        goQuitBtn.onClick.AddListener(manager.QuitGame);
    }
	
    //Turn off all UI tab, used for better control the current active tab
    void TurnOffAllTabs()
    {
        foreach(GameObject tab in tabs)
        {
            tab.SetActive(false);
        }
    }

    ///This region is for Activate single Tab
    #region Active UI function
    public void ToPauseTab()
    {
        TurnOffAllTabs();
        pauseTab.SetActive(true);
    }

    public void ToMainUITab()
    {
        TurnOffAllTabs();
        mainUITab.SetActive(true);
    }

    public void ToGameOverTab()
    {
        TurnOffAllTabs();
        gameOverTab.SetActive(true);

        goAnim.Play();
    }

    public void ToLeaderBoardTab()
    {
        TurnOffAllTabs();
        leaderBoardTab.SetActive(true);
    }
    #endregion

    public void UpdateMainUIScore(int _score)
    {
        mainScoreValueTxt.text = _score.ToString();
    }

    public void UpdateGameOverUIScore(int _score)
    {
        goScoreTxt.text = _score.ToString();
    }

    public void StartTutorial()
    {
        ToMainUITab();
        mainTutorialTab.SetActive(true);
        mainTutorial.Play();

        //Turn off tutorial when the animation was finished
        Invoke("TurnOffTutorial", (float)mainTutorial.duration);
    }

    void TurnOffTutorial()
    {
        mainTutorialTab.SetActive(false);
    }
}
