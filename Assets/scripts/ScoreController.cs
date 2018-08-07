using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    #region Fields
    [SerializeField] private Text scoreText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject scoreImage;


    private Animator m_animator;
    private int score;
    #endregion


    #region Unity lifecycle
    private void Start () {
        m_animator = scoreText.gameObject.GetComponent<Animator>();
        score = 0;
    }


    private void Update()
    {
        if (gameOverPanel.activeInHierarchy == true)
        {
            CheckForHighScore();
            PlayerPrefs.SetInt("score", score);
            HideScoreImage();
        }
    }
    #endregion


    #region Public methods
    public void HideScoreText()
    {
        scoreText.gameObject.SetActive(false);
    }


    public void HideScoreImage()
    {
        scoreImage.SetActive(false);
    }


    public void ShowScoreText()
    {
        scoreText.gameObject.SetActive(true);
    }


    public void ShowScoreImage()
    {
        scoreImage.SetActive(true);
    }


    public void IncreaseScore()
    {
        score++;
    }


    public int GetScore()
    {
        return score;
    }


    static public int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore", 0);
    }


    public void CheckForHighScore()
    {
        if(score > GetHighScore())
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }


    public void SetScoreToZero()
    {
        score = 0;
    }


    public void PlayAnimationOfUpdating()
    {
        m_animator.SetTrigger("updateScore");
    }


    public void UpdateScoreText()
    {
        scoreText.text = (score).ToString();
    }
    #endregion
}
