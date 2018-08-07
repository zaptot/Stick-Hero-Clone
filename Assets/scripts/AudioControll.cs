using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControll : MonoBehaviour {


    #region Fields
    [SerializeField] GameObject soundOn;
    [SerializeField] GameObject soundOff;
    #endregion


    #region Unity lifecycle
    void Start () {
	    if(PlayerPrefs.GetInt("sound", 1) == 0)
        {
            ToggleImgs();
        }	
	}
    #endregion


    #region Public methods
    public void ToggleAudio()
    {
        if(PlayerPrefs.GetInt("sound", 1) == 1)
        {
            PlayerPrefs.SetInt("sound", 0);
        }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
        }
    }


    public void ToggleImgs()
    {
        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
    }
    #endregion
}
