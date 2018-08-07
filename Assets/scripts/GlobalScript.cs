using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalScript : MonoBehaviour {


    #region Fields
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject scoreTextObject;

    private const float PIVOT_2 = -45.0f;
    private const float PIVOT_4 = 90.0f;
    private const float BLOCK_Y_COORDINATE = -105.0f;
    private const float CHARACTER_Y_COORDINATE = -55.0f;


    private MoveBlock[] allBlocks;
    private GameObject character;
    private GameObject score;
    private bool isGameOver;
    private bool lineOver;
    #endregion


    #region Unity lifecycle
    void Start () {
        lineOver = false;
        isGameOver = false;
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("block");
        score = GameObject.FindGameObjectWithTag("score");
        character = GameObject.FindGameObjectWithTag("character");
        allBlocks = new MoveBlock[tmp.Length];
        for(int i = 0; i < tmp.Length; i++)
        {
            allBlocks[i] = tmp[i].GetComponent<MoveBlock>();
        }
	}
    #endregion


    #region Public methods
    public void MoveAll()
    {
        for(int i = 0; i<allBlocks.Length; i++)
        {
            allBlocks[i].Move();
        }
    }


    public void GameOver()
    {
        isGameOver = true;
    }


    public void LineOver()
    {
        lineOver = true;
    }


     public void LineNotOver()
    {
        lineOver = false;
    }


    public bool GetLineOver()
    {
        return lineOver;
    }


    public bool GetIsGameOver()
    {
        return isGameOver;
    }


    public void RestartGame()
    {
        character.transform.SetParent(transform);
        isGameOver = false;
        float[] tmp = { PIVOT_2, MoveBlock.GetChPoint(), PIVOT_4 };
        for(int i = 0; i < allBlocks.Length; i++)
        {
            int tmpX = allBlocks[i].GetComponent<MoveBlock>().GetState();
            allBlocks[i].GetComponent<MoveBlock>().SetStartedCondition();
            if (tmpX == 2)
            {
                allBlocks[i].GetComponent<MoveBlock>().LocateOnRandomPoint();
            }
            else
            {
                allBlocks[i].transform.parent.position = new Vector3(tmp[tmpX - 1], BLOCK_Y_COORDINATE, 0.0f);
            }
        }
        GameObject[] allLines = GameObject.FindGameObjectsWithTag("line");
        for(int i = 0; i < allLines.Length; i++)
        {
            Destroy(allLines[i]);
        }
        gameOverPanel.SetActive(false);
        character.transform.position = new Vector3(PIVOT_2, CHARACTER_Y_COORDINATE, 0);
        character.GetComponent<CharacterController>().SetNormalSpeed();
        character.GetComponent<CharacterController>().RestartGame();        
        score.GetComponent<ScoreController>().SetScoreToZero();
        score.GetComponent<ScoreController>().UpdateScoreText();
        score.GetComponent<ScoreController>().CheckForHighScore();
        scoreTextObject.SetActive(true);
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    #endregion
}
