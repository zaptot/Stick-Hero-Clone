using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDoteController : MonoBehaviour {


    #region Fields
    [SerializeField] private GameObject block;


    private Animator perfectAnimator;
    private MoveBlock blockState;
    private ScoreController score;
    private AudioSource m_audio;
    #endregion


    #region Unity lifecycle
    void Start () {
        perfectAnimator = GameObject.FindGameObjectWithTag("perfect").GetComponent<Animator>();
        m_audio = GetComponent<AudioSource>();
        score = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreController>();
        blockState = block.GetComponent<MoveBlock>();
    }
	

	void Update () {
		if(blockState.GetState() == 1)
        {
            gameObject.SetActive(false);
        }
        else if (blockState.GetState() == 3)
        {
            gameObject.SetActive(true);
        }
	}
    #endregion


    #region Event handlers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "gameOver")
        {
            if (PlayerPrefs.GetInt("sound", 0) == 1)
            {
                m_audio.Play();
            }
            score.IncreaseScore();
            score.PlayAnimationOfUpdating();
            score.UpdateScoreText();
            perfectAnimator.SetTrigger("perfectTrigger");
        }
    }
    #endregion
}
