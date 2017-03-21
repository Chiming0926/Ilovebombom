using UnityEngine;
using System;
using System.Collections;

public partial class AGCC : MonoBehaviour {

    /*******************************************************************
	*	Copyright  2013 arcalet. All Rights Reserved
	*	 
	*******************************************************************/
    public static int BBDEBUG_INFO = 1;
    public static int BBDEBUG_WARNING = 2;
    public static int BBDEBUG_ERROR = 4;
    private static int bbDebug = 7; /* debug level info, warning, error */
	#region Variables	
	string gguid = "7245577f-4961-7642-a64c-ba5bb008892c";
	string sguid = "52a06444-ff13-654b-bfa1-29da9f7124dd";

	string iguid_server = "";
	string iguid_player = "";

    string sguid_game = "c4345a29-310f-d241-b95c-77928bf819c6";
    string lguid = "";

	byte[] certificate = {0x43, 0x13, 0xbd, 0x25, 0x4e, 0x7b, 0xa3, 0x4b, 0x86, 0x13, 0xa8, 0xa5, 0x49,
                            0xcf, 0xd1, 0x5e, 0x58, 0x34, 0x2c, 0xda, 0xb, 0x9f, 0xc, 0x41, 0x8a, 0x3e,
                            0xd4, 0x71, 0x5a, 0xb, 0x3d, 0x41, 0x57, 0x1c, 0x53, 0xed, 0xf, 0x52, 0x70,
                            0x47, 0x93, 0xb5, 0x9f, 0x27, 0xe3, 0xa0, 0x66, 0x2d, 0x86, 0x55, 0x5b, 0x9c,
                            0x4b, 0x33, 0xd8, 0x40, 0xbb, 0x66, 0xf1, 0x8c, 0x67, 0x19, 0x8a, 0x4, 0x1a,
                            0x14, 0x28, 0xa7, 0x67, 0x95, 0x72, 0x4c, 0x8c, 0xfb, 0xa, 0xf5, 0x4a, 0x12,
                            0x49, 0x53, 0x6d, 0xf7, 0xa0, 0xd4, 0x73, 0x96, 0x52, 0x43, 0x8e, 0x54, 0x79,
                            0xfa, 0x48, 0xeb, 0x5b, 0xaf, 0xd1, 0x64, 0x20, 0x3d, 0x49, 0xa5, 0xbc, 0x40,
                            0x89, 0xa2, 0xb6, 0xc5, 0x6f, 0xd6, 0xac, 0xfe, 0x2f, 0x92, 0x4d, 0xbc, 0x3f,
                            0xbd, 0x4b, 0x4d, 0x90, 0xf8, 0x50, 0xf2, 0x2, 0x16, 0x75, 0xd4};

    internal ArcaletGame ag = null;
	public ServerSettings serverSettings = new ServerSettings();
	
	#endregion

	internal PlayerInfo m_PlayerInfo = new PlayerInfo();
	
	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(this);
		ArcaletSystem.UnityEnvironment();
	}
	
    public static void BBDebug(int debugLevel, string msg)
    {
        int numOfDebugLevel = 3;
        for (int i=0; i< numOfDebugLevel; i++)
        {
            if ((debugLevel & (1 << i)) == 1)
            {
                Debug.Log(msg);
                break;
            }
        }
    }

	void MainMessageIn(string msg, int delay, ArcaletGame game)
	{
		try 
		{
            Debug.Log("MainMessageIn, msg = " + msg);
        }
        catch (Exception e) { Debug.LogWarning("MainMessageIn Exception:\r\n" + e.ToString()); }
	}
	
	void PrivateMessageIn(string msg, int delay, ArcaletGame game)
	{
		try {
			Debug.Log("@ PrivaMsg>> " + msg);
			string[] cmds = msg.Split(':');			
            switch (cmds[0])
            {
				case "dp_new": GetPlayerInfos(cmds[1]); break;
				case "dp_room": DP_Room(cmds[1]); break;
				case "dp_rematch": ReMatch(cmds[1]); break;
				case "bb_match_data": UpdateMatchData(cmds[1]); break;
			//	case "dp_update": OXGame.UpdatePlayerInfos(cmds[1]); break;
            }
        }
        catch (Exception e) { Debug.LogWarning("PrivateMessageIn Exception:\r\n" + e.ToString()); }
	}
	
	void GameMessageIn(string msg, int delay, ArcaletScene scene)
	{
		try {
			Debug.Log("@ GameMsg>> " + msg);
			string[] cmds = msg.Split(':');
			CGameManager game = FindObjectOfType(typeof(CGameManager)) as CGameManager;
			if (game == null) 
				return ;
			
            switch (cmds[0])
            {
				case "bb_move":
					game.player_move(cmds[1]); 
					break;
				case "bb_stop": 
					game.player_stop(cmds[1]); 
					break;
				case "bb_player":
					game.add_player(cmds[1]);
					break;
				case "bb_wball":
					game.bb_wball(cmds[1]);
					break;
                case "bb_death":
                    game.handle_death_message(cmds[1]);
                    break;
				case "bb_over":
					//game.
					break;
				default:
					break;
			//	case "dp_start": game.GameStart(cmds[1]); break;
			//	case "dp_player": game.SetRevalInfos(cmds[1]); break;
			//	case "dp_slot": game.FillSlot(cmds[1]); break;
			//	case "dp_gameover": game.DP_GameOver(cmds[1]); break;
			//	case "dp_draw": game.DP_Draw(cmds[1]); break;
			//	case "dp_timeup": game.DP_TiemUP(cmds[1]); break;
			//	case "dp_sync" : game.TimerSynchronization(cmds[1], delay); break;
            }
        }
        catch (Exception e) { Debug.LogWarning("GameMessageIn Exception:\r\n" + e.ToString()); }
	}
	
	void OnApplicationQuit()
	{
		if(ag==null) return;
		ag.Dispose();
	}

	internal void setFBUserId(string id)
	{
		m_PlayerInfo.fbUserId = id;
	}
}

[System.Serializable]
public class ServerSettings 
{
	public bool passageGate = false;
	public string announcement = "";
	public int dpPoid = 0;
}

[System.Serializable]
public class PlayerInfo
{
    public string nickname = "NickName";
    public string account = "Account";
    public int win = 0;
    public int lose = 0;
    public int draw = 0;
    public string winRate = "0%";
	public string fbUserId = "";
    internal void SetWinRate()
    {
        if (win == 0) winRate = "0%";
        else
        {
            float rate_f = (float)win / (win + lose);
            int rate_100 = Mathf.CeilToInt(rate_f * 100);
            winRate = rate_100 + "%";
        }
    }
}


