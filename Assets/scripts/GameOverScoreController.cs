using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScoreController : MonoBehaviour {


    #region Fields
    [SerializeField] private Text score;
    [SerializeField] private Text highScore;


    private GlobalScript global;
    #endregion


    #region Unity lifecycle
    private void Start()
    {
        global = GameObject.FindGameObjectWithTag("global").GetComponent<GlobalScript>();
    }


    private void Update () {
        if(global.GetIsGameOver() == true)
        {
            score.text = PlayerPrefs.GetInt("score", 0).ToString();
            highScore.text = PlayerPrefs.GetInt("highScore", 0).ToString();
        }     
    }
    #endregion
}
