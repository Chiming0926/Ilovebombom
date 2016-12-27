using UnityEngine;
using System;
using System.Collections;


public partial class AGCC {
		
	internal ArcaletScene sn = null;
	
	public MatchInfo matchInfos = new MatchInfo();
    /*
    GameManager game = null;

	internal void Match( GameManager g)
	{
		game = g;
		matchInfos.GenerateMatchCode();
		string msg = ag.gameUserid + "/" + ag.poid + "/" + OXGame.playerInfo.nickname + "/" + matchInfos.matchCode;
		ag.PrivacySend("match:" + msg , serverSettings.dpPoid);
	}
	*/
	
	void DP_Room(string msg) 
	{			
		string[] m = msg.Split('/');
		int sid = int.Parse(m[0]);
		string code = m[1];
		
		if(matchInfos.matchCode != code)
			return;
		
		if(sn != null) {			
			sn.Leave(CB_LeaveScene, sid);
			Debug.Log("Sn is not null");			
		}
		else {		
			sn = new ArcaletScene(ag, sguid_game, sid);
			sn.onMessageIn += GameMessageIn;
			sn.onCompletion += CB_EnterRoom;
			sn.Launch();
		}
	}
	
	void CB_LeaveScene(int code, object token)
	{
		if(code == 0) {			
			Debug.Log("CB_LeaveScene Successed");			
			int sid = (int)token;
			sn = new ArcaletScene(ag, sguid_game, sid);
			sn.onMessageIn += GameMessageIn;
			sn.onCompletion += CB_EnterRoom;
			sn.Launch();
		}
		else {
			Debug.Log("CB_LeaveScene Failed: " + code);
			matchInfos.matchCode = "";
			ag.PrivacySend("cancel:" + ag.poid, serverSettings.dpPoid);
			Application.LoadLevel("MainMenu");
		}	
	}	
	
	void CB_EnterRoom(int code, ArcaletScene scene)
	{
		if(code == 0) {
			Debug.Log("CB_EnterRoom Successed");
			scene.Send("ready:" + ag.poid);
		}
		else {
			Debug.Log("CB_EnterRoom Failed: " + code);
		}
	}
	
	void ReMatch(string code) 
	{
		if(matchInfos.matchCode != code) 
			return;
		
		Debug.Log("ReMatch");		
		if(sn != null) {
			sn.Leave(CB_RematchLeave ,null);
		}
		else {
			matchInfos.GenerateMatchCode();
		//	string msg = ag.gameUserid + "/" + ag.poid + "/" + OXGame.playerInfo.nickname + "/" + matchInfos.matchCode;
		//	ag.PrivacySend("match:" + msg , serverSettings.dpPoid);
		}
	}
	
	void CB_RematchLeave(int code, object token)
	{
		if(code == 0) {
			sn = null;
			matchInfos.GenerateMatchCode();
		//	string msg = ag.gameUserid + "/" + ag.poid + "/" + OXGame.playerInfo.nickname + "/" + matchInfos.matchCode;
		//	ag.PrivacySend("match:" + msg , serverSettings.dpPoid);	
		}
	}
	
	
	internal void SceneGameOver()
	{
		if(sn!=null) {
			Debug.Log("SceneGameOver");
			sn.Leave(CB_SceneGameOver, null);
		}
	}
	
	void CB_SceneGameOver(int code, object token)
	{
		if(code == 0) {
			sn = null;
			Debug.Log("CB_SceneGameOver Successed");
		}
		if(code!=0) {
			Debug.Log("CB_SceneGameOver Failed");	
		}
	}
	
}

[System.Serializable]
public class MatchInfo
{
	public string matchCode = "";
	
	internal void GenerateMatchCode()
	{
		string code = "";
		char[] ch = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'R', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
		
		do {
			for(int i = 0; i < 12; i++) { 
				code += ch[ UnityEngine.Random.Range(0, 36)].ToString(); 
			}
		}
		while(matchCode == code);	
		matchCode = code;
	}
}
