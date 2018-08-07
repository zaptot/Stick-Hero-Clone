using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundScript : MonoBehaviour {


    #region Fields
    private AudioSource m_audio;
    #endregion


    #region Unity lifecycle
    private void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }
    #endregion


    #region public methods
    public void PlaySound()
    {
        if(PlayerPrefs.GetInt("sound", 0) == 1)
        {
            m_audio.Play();
        }
    }
    #endregion
}
