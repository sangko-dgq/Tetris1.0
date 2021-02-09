using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class View : MonoBehaviour
{
    private Ctrl ctrl;

    private RectTransform logoName; //LogoName的RectTransform组件
    private RectTransform menuUI;
    private RectTransform gameUI;
    private GameObject restartButton;
    private GameObject gameOverUI,settinggUI, rankUI;

    private Text score, highScore,gameOverScore;
    private Text rankScore, rankHighScore, rankNumbersGame;

    private GameObject mute;

     [HideInInspector] public bool isRightButtonDown = false, isLeftButtonDown = false, isSpeedupButtonDown = false, isRotateButtonDown = false;

    

    // Start is called before the first frame update
    void Awake()
    {
        ctrl = GameObject.FindWithTag("Ctrl").GetComponent<Ctrl>();

        logoName = transform.Find("Canvas/LogoName") as RectTransform;
        menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
        gameUI = transform.Find("Canvas/GameUI") as RectTransform;
        restartButton = transform.Find("Canvas/MenuUI/RestartButton").gameObject;
        
        gameOverUI = transform.Find("Canvas/GameOverUI").gameObject;
        settinggUI = transform.Find("Canvas/SettingUI").gameObject;
        rankUI = transform.Find("Canvas/RankUI").gameObject;


        score = transform.Find("Canvas/GameUI/ScoreLabel/Text").GetComponent<Text>();
        highScore = transform.Find("Canvas/GameUI/HighScoreLabel/Text").GetComponent<Text>();
        gameOverScore = transform.Find("Canvas/GameOverUI/Text").GetComponent<Text>();

        mute = transform.Find("Canvas/SettingUI/AudioButton/Mute").gameObject;

        rankScore = transform.Find("Canvas/RankUI/ScoreLabel/Text").GetComponent<Text>();
        rankHighScore = transform.Find("Canvas/RankUI/HighScoreLabel/Text").GetComponent<Text>();
        rankNumbersGame = transform.Find("Canvas/RankUI/NumbersGameLabel/Text").GetComponent<Text>();
    }

    //显示
    public void ShowMenu()
    {
        logoName.gameObject.SetActive(true);
        logoName.DOAnchorPosY(-164.8f, 0.5f);  //从默认位置，运动到锚点坐标的y值为-86.636处，花费0.5s

        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(71,0.5f);
    }
    public void HideMenu()
    {
        logoName.DOAnchorPosY(160.8f, 0.5f)
            .OnComplete(delegate { logoName.gameObject.SetActive(false);});  // A.OnComplete(delegate{B}); A 完成后执行 B
        menuUI.DOAnchorPosY(-71f, 0.5f)
            .OnComplete(delegate { menuUI.gameObject.SetActive(false);});
    }

    public void UpdateGameUI(int score, int highScore)
    {
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
    }
    public void ShowGameUI(int score = 0, int highScore = 0)
    {
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();

        gameUI.gameObject.SetActive(true);
        gameUI.DOAnchorPosY(-126.99f, 0.5f);

    }

    public void HideGameUI()
    {
        gameUI.DOAnchorPosY(126.99f, 0.5f)
            .OnComplete(delegate { gameUI.gameObject.SetActive(false);});  // A.OnComplete(delegate{B}); A 完成后执行 B
    }
    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }
    public void ShowGameOverUI(int score = 0)
    {
        ctrl.audioManager.PlayGameOver();
        gameOverUI.SetActive(true);
        gameOverScore.text = score.ToString();
    }
    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }
    
    public void OnHomeButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnSettingButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        settinggUI.SetActive(true);
    }
    public void SetMuteActive(bool isActive)
    {
        mute.SetActive(isActive);
    }
    public void OnSettingUIClick()
    {
        ctrl.audioManager.PlayCursor();
        settinggUI.SetActive(false);
    }
    public void OnGitHuButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        Application.OpenURL("https://github.com/sangko-dgq");
    }
    public void OnRankUIClick()
    {
        ctrl.audioManager.PlayCursor();
        rankUI.SetActive(false);
    }
    public void ShowRankUI(int score, int highScore, int numbersGame)
    {
        this.rankScore.text = score.ToString();
        this.rankHighScore.text = highScore.ToString();
        this.rankNumbersGame.text = numbersGame.ToString();
        rankUI.SetActive(true);
    }

    public void  OnRightButtonClick()
    {
        isRightButtonDown = true;
    }
    public void  OnLeftButtonClick()
    {
        isLeftButtonDown = true;
    }
    public void  OnRotateButtonClick()
    {
        isRotateButtonDown = true;
    }
    public void OnSpeedUpbuttonDownClick()
    {
        isSpeedupButtonDown = true;
    }

}
