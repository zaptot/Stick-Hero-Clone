using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    

    #region Fields
    private const float GAME_OVER_SPEED = 400.0f;
    private const float BASE_SPEED = 50.0F;


    private GlobalScript global;
    private ScoreController score;
    private float speed;
    private int direction;
    private bool isGameStarted;
    #endregion


    #region Unity lifecycle
    void Start() {
        score = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreController>();
        isGameStarted = false;
        global = GameObject.FindGameObjectWithTag("global").GetComponent<GlobalScript>();
        speed = 0.0f;
        direction = 1;
    }


    void Update() {
        transform.Translate(transform.right * speed * direction * Time.deltaTime);
        if (global.GetIsGameOver() == true)
        {
            transform.Translate(-transform.up * GAME_OVER_SPEED * direction * Time.deltaTime);
        }
    }
    #endregion


    #region Event handlers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "spawner" && (global.GetLineOver() == true || isGameStarted == false))
        {
            SetZeroSpeed();
            if (isGameStarted == true)
            {
                global.MoveAll();
                score.IncreaseScore();
                score.PlayAnimationOfUpdating();
                score.UpdateScoreText();
                if (PlayerPrefs.GetInt("sound", 0) == 1)
                {
                    GetComponent<AudioSource>().Play();
                }
            }
            isGameStarted = true;
            transform.SetParent(collision.transform);
        }
        else if (collision.tag == "gameOver")
        {
            SetZeroSpeed();
            global.GameOver();
            collision.transform.parent.GetComponent<LineController>().GameOverRotate();
        }
    }
    #endregion


    #region Public methods
    public void ChangeDirection()
    {
        direction *= -1;
    }


    public void SetZeroSpeed()
    {
        speed = 0.0f;
    }


    public void SetNormalSpeed()
    {
        speed = BASE_SPEED;
    }


    public bool IsGameStarted()
    {
        return isGameStarted;
    }


    public void RestartGame()
    {
        isGameStarted = false;
    }
    #endregion
}
