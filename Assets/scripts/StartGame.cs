using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {


    #region Fields
    [SerializeField] private GameObject scoreText;


    private MoveBlock[] allBlocks;
    private CharacterController character;
    private CameraScript m_camera;
    #endregion


    #region Unity lifecycle
    void Start () {
        character = GameObject.FindGameObjectWithTag("character").GetComponent<CharacterController>();
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("block");
        allBlocks = new MoveBlock[tmp.Length];
        m_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
        for(int i = 0; i < tmp.Length; i++)
        {
            allBlocks[i] = tmp[i].GetComponent<MoveBlock>();
        }
	}
    #endregion


    #region Public methods
    public void PlayGame()
    {
        character.SetNormalSpeed();
        m_camera.StartGameAnimation();
        for(int i  = 0; i < allBlocks.Length; i++)
        {
            allBlocks[i].Move();
        }
        scoreText.SetActive(true);
        transform.gameObject.SetActive(false);
    }
    #endregion
}
