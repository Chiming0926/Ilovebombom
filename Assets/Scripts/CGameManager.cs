using UnityEngine;
using System.Collections;
using System;

public class CGameManager : MonoBehaviour {

	public class PlayerData
    {
		GameObject player_ins;
		string 	   player_account;	
    }


	public GameObject player_prefab;
    GameObject player_ins;

	enum PLAYER_DIRECTION
    {
        UP     = 0,
        DOWN   = 1,
        RIGHT  = 2,
        LEFT   = 3,
        NULL   = 4
    };
	PLAYER_DIRECTION cur_direct;
	
    AGCC m_agcc = null;
	// Use this for initialization
	void Start ()
    {
		m_agcc = FindObjectOfType(typeof(AGCC)) as AGCC;
		if(m_agcc == null) return;
		player_ins = Instantiate(player_prefab, new Vector3(7.5f, 4.5f, -1), gameObject.transform.rotation) as GameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

	public void BeginMove()
    {
    }

    public void EndMove()
    {
    	if (cur_direct == PLAYER_DIRECTION.NULL)
			return;
		string msg = "bb_stop:"+m_agcc.ag.gameUserid;
		cur_direct = PLAYER_DIRECTION.NULL;
		m_agcc.sn.Send(msg);
    }

    public void UpdateDirection(Vector3 direction)
    {
    	PLAYER_DIRECTION direct;
        if (Math.Abs(direction.x) == Math.Abs(direction.y))
        {
            /* don't change */
			return;
        }
        else if (Math.Abs(direction.x) > Math.Abs(direction.y))
        {
            if (direction.x > 0)
                direct = PLAYER_DIRECTION.RIGHT;
            else
                direct = PLAYER_DIRECTION.LEFT;
        }
        else
        {
            if (direction.y > 0)
				direct = PLAYER_DIRECTION.UP;
            else
                direct = PLAYER_DIRECTION.DOWN;
        }
		if (direct != cur_direct)
		{
			cur_direct = direct;
			string msg;
			msg = "bb_move:" + m_agcc.ag.gameUserid + "/" + (int)direct;
			Debug.Log("@@@@@@ bb_move msg = " + msg);
			m_agcc.sn.Send(msg);
		}
    }


	internal void player_move(string msg)
	{
		Debug.Log("player_move");
		string[] m = msg.Split('/');	
		player_ins.GetComponent<CPlayer>().UpdateDirection(int.Parse(m[1]));
	}

	internal void player_stop(string msg)
	{
		Debug.Log("player_stop");
		player_ins.GetComponent<CPlayer>().EndMove();
	}
}
