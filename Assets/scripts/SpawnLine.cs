using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLine : MonoBehaviour {


    #region FieldS
    private const float SPEED_OF_LINE = 200.0F;
    private const float ROTATE_ANGLE = -90.0F;


    [SerializeField] private Transform linePrefab;


    private CharacterController character;
    private MoveBlock parent;
    private GameObject line;
    private AudioSource m_audio;
    private int state;
    #endregion


    #region Unity lifecycle
    void Start () {
        m_audio = GetComponent<AudioSource>();
        state = 0;
        parent = transform.parent.GetComponent<MoveBlock>();
        character = GameObject.FindGameObjectWithTag("character").GetComponent<CharacterController>();
	}
	

	void Update () {
        if(parent.GetState() == 1 && parent.GetisStop() == true && character.IsGameStarted())
        {
            if (Input.GetKey(KeyCode.Space) || 
                (Input.touchCount > 0 && (Input.GetTouch(0).phase != TouchPhase.Ended)))
            {
                if (state == 0)
                {
                    if (PlayerPrefs.GetInt("sound", 0) == 1)
                    {
                        m_audio.Play();
                    }
                    line = Instantiate(linePrefab, transform.position, transform.rotation).gameObject;
                    state = 1;
                }
                else if(state == 1 && line.transform.localScale.y < 400)
                {  
                    line.transform.localScale += new Vector3(0, SPEED_OF_LINE, 0) * Time.deltaTime;
                }
            }
            else if(state == 1)
            {
                if (PlayerPrefs.GetInt("sound", 0) == 1)
                {
                    m_audio.Pause();
                }
                state = 2;
            }
            if (state == 2)
            {
                RotateAnimationStart();
                if (line.transform.rotation == Quaternion.Euler(0, 0, ROTATE_ANGLE))
                {
                    character.SetNormalSpeed();
                    state = 3;
                }
            }
        }
        if (!line)
        {
            state = 0;
        }
    }
    #endregion


    #region Public methods
    public void RotateAnimationStart()
    {
        line.GetComponent<Animator>().SetBool("shouldFall", true);
    }
    #endregion
}
