using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {


    #region Public methods
    public void StopStartAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool("isStop", true);
    }


    public void StartGameAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool("startGame", true);
    }


    public void TurnOffAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool("gameOver", false);
    }
    #endregion
}
