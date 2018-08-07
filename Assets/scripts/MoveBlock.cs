using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour {


    #region Fields
    private const float BASE_SPEED = 130.0f;
    private const float PIVOT_1 = -90.0f;
    private const float PIVOT_2 = -45.0f;
    private const float PIVOT_4 = 90.0f;
    private const float MAX_WIDTH_OF_BLOCK = 60.0f;
    private const float MIN_WIDTH_OF_BLOCK = 15.0F;
    private const float BLOCK_Y_COORDINATE = -105.0f;
    private const float BLOCK_HEIGHT = 100.0f;

    static private float changeablePivot;


    [SerializeField] private short state;
    [SerializeField] private bool isStop;
    [SerializeField] private GameObject redDot;
    
    
    private GameObject[] allBlocks;
    private MoveBlock[] allBlocksComps;
    private GameObject parent;
    private float changedSpeed;
    #endregion


    #region Unity lifecycle
    void Start () {
        parent = transform.parent.gameObject;
        changedSpeed = 0.0f;
        if (state == 2)
        {
            changeablePivot = 0;
            parent.transform.position = new Vector3(changeablePivot, BLOCK_Y_COORDINATE, 0);
        }
        isStop = true;
        allBlocks = GameObject.FindGameObjectsWithTag("block");
        allBlocksComps = new MoveBlock[allBlocks.Length];
        for(int i = 0; i < allBlocks.Length; i++)
        {
            allBlocksComps[i] = allBlocks[i].GetComponent<MoveBlock>();
        }
    }
	

	void Update () {
        if(isStop == false)
        {
            if (state == 1)
            {
                if(transform.position.x > PIVOT_1)
                {
                    parent.transform.Translate(-transform.right * BASE_SPEED * Time.deltaTime);
                }
                else
                {
                    if (transform.childCount > 2)
                    {
                        Destroy(transform.GetChild(2).gameObject);
                    }
                    
                    parent.transform.position = new  Vector3(PIVOT_4, BLOCK_Y_COORDINATE, 0);
                    transform.localScale = new Vector3(Random.Range(MAX_WIDTH_OF_BLOCK, MIN_WIDTH_OF_BLOCK), BLOCK_HEIGHT, 0);
                    state = 3;
                    isStop = true;
                }
            }
            else if (state == 2)
            {
                if (transform.position.x > PIVOT_2)
                {
                    parent.transform.Translate(-transform.right * BASE_SPEED * Time.deltaTime);
                }
                else
                {
                    isStop = true;
                    state = 1;
                }               
            }
            else if (state == 3)
            {
                if (changedSpeed == 0.0f)
                {
                    changedSpeed = GetNewChangedSpeed();
                }
                if (transform.position.x > changeablePivot)
                {
                    parent.transform.Translate(-transform.right * changedSpeed * Time.deltaTime);
                }
                else
                {
                    isStop = true;
                    changedSpeed = 0.0f;
                    state = 2;
                }
            }
        }
        ControllRedDot();
    }
    #endregion


    #region Event handlers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "line" && state == 2)
        {
            collision.gameObject.transform.SetParent(transform);
            if (PlayerPrefs.GetInt("sound", 0) == 1)
            {
                collision.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
    #endregion


    #region public methods
    public void LocateOnRandomPoint()
    {
        MoveBlock.changeablePivot = NewChangeblePointCoordinates();
        parent.transform.position = new Vector3(changeablePivot, BLOCK_Y_COORDINATE, 0);
    }


    public void Move()
    {
        isStop = false;
    }


    public short GetState()
    {
        return state;
    }


    public bool GetisStop()
    {
        return isStop;
    }


    public void SetStartedCondition()
    {
        changedSpeed = 0.0f;
        transform.localScale = new Vector3(MAX_WIDTH_OF_BLOCK, BLOCK_HEIGHT, 1);
    }


    static public float GetChPoint()
    {
        return changeablePivot;
    }
    #endregion


    #region Private methods
    private float NewChangeblePointCoordinates()
    {
        return Random.Range(PIVOT_2 + MAX_WIDTH_OF_BLOCK / 2, Mathf.Abs(PIVOT_2));
    }


    private float GetNewChangedSpeed()
    {
        float tmp = MoveBlock.changeablePivot;
        MoveBlock.changeablePivot = NewChangeblePointCoordinates();
        return (Mathf.Abs(parent.transform.position.x - MoveBlock.changeablePivot) * BASE_SPEED / Mathf.Abs(tmp - PIVOT_2));
    }


    private void ControllRedDot()
    {
        if (state == 1)
        {
            redDot.SetActive(false);
        }
        else if (state == 3)
        {
            redDot.SetActive(true);
        }
    }
    #endregion
}
