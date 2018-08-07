using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour {

    #region Public methods
    public void GameOverRotate()
    {
        gameObject.GetComponent<Animator>().SetBool("isGameOver", true);
    }
    #endregion
}
