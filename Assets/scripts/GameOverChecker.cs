using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour {


    #region Fields
    private GlobalScript global;
    private bool flag;
    #endregion


    #region Unity lifecycle
    private void Start()
    {
        flag = false;
        global = GameObject.FindGameObjectWithTag("global").GetComponent<GlobalScript>();
        global.LineNotOver();
    }
    #endregion


    #region Event handlers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "block" && flag == false)
        {
            transform.gameObject.SetActive(false);
            global.LineOver();
        }
        if (collision.tag == "GameOverBlock")
        {
            flag = true;
            transform.gameObject.SetActive(true);
            global.LineNotOver();
        }
    }
    #endregion
}
