using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeCamera : MonoBehaviour {

    #region Fields
    [SerializeField] private GameObject gameOverPanel;
    #endregion


    #region Unity lifecycle
    private void Start()
    {
        gameOverPanel.SetActive(false);
    }
    #endregion


    #region Event handlers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "character")
        {
            transform.parent.gameObject.GetComponent<Animator>().SetBool("gameOver", true);
            if (PlayerPrefs.GetInt("sound", 0) == 1)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        
    }
    #endregion


    #region Public Methods
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    #endregion
}
