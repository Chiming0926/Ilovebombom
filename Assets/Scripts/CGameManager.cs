using UnityEngine;
using System.Collections;
using System;

public class CGameManager : MonoBehaviour {


	public GameObject player_prefab;
    public GameObject player_ins;

    AGCC m_agcc = null;
	// Use this for initialization
	void Start ()
    {
		m_agcc = FindObjectOfType(typeof(AGCC)) as AGCC;
		if(m_agcc == null) return;
		player_ins = (GameObject)Instantiate(player_prefab, new Vector3(7.5f, 4.5f, -1), gameObject.transform.rotation);
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
		string msg = "bb_stop:"+m_agcc.ag.gameUserid;
		m_agcc.sn.Send(msg);
    }

    public void UpdateDirection(Vector3 direction)
    {
		Debug.Log("@@@@ UpdateDirection");
		string msg;
        if (Math.Abs(direction.x) == Math.Abs(direction.y))
        {
            /* don't change */
        }
        else if (Math.Abs(direction.x) > Math.Abs(direction.y))
        {
            if (direction.x > 0)
                msg = "bb_move:" + m_agcc.ag.gameUserid + "/RIGHT";
            else
                msg = "bb_move:" + m_agcc.ag.gameUserid+ "/LEFT";
			m_agcc.sn.Send(msg);
        }
        else
        {
            if (direction.y > 0)
				msg = "bb_move:" + m_agcc.ag.gameUserid + "/UP";
            else
                msg = "bb_move:" + m_agcc.ag.gameUserid + "/DOWN";
            m_agcc.sn.Send(msg);
        }	
    }


	internal void player_move(string msg)
	{
		Debug.Log("player_move");
		player_ins.GetComponent<CPlayer>().EndMove();
	}

	internal void player_stop(string msg)
	{
		Debug.Log("player_stop");
		player_ins.GetComponent<CPlayer>().EndMove();
	}
}
