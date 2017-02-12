using UnityEngine;
using System;
using System.Collections;
using Facebook.Unity;

public partial class AGCC {
		
	internal ArcaletScene sn = null;
	
	public MatchInfo matchInfos = new MatchInfo();
	IEnumerator GetImageAndShow() 
	{
        Debug.Log("@@@@@@@@@@@@@@ 1");
        WWW www = new WWW("https://graph.facebook.com/" + m_PlayerInfo.fbUserId + "/picture?type=large"); 
        yield return www;
		GameObject obj = GameObject.Find ("player01");
		if (obj != null)
		{
            //TextureScale.Bilinear(www.texture, obj.GetComponent<SpriteRenderer>().sprite.texture.width, obj.GetComponent<SpriteRenderer>().sprite.texture.height);
            TextureScale.Bilinear(www.texture, 1000, 1000);
            Debug.Log("@@@@@@@@@@@@@@ 1 obj.GetComponent<SpriteRenderer>().sprite.texture.width = " + www.texture.width);
            Debug.Log("@@@@@@@@@@@@@@ 2 obj.GetComponent<SpriteRenderer>().sprite.texture.width = " + obj.GetComponent<SpriteRenderer>().sprite.texture.width);
            Sprite s = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector3(0.5f, 0.5f, 0));
            obj.GetComponent<SpriteRenderer>().sprite = s;
        }
    }

    internal void Match()
    {
		Debug.Log("@@@@@@@@@@@@@@ Match 1");
        matchInfos.GenerateMatchCode();
		StartCoroutine(GetImageAndShow());
        string msg = ag.gameUserid + "/" + ag.poid + "/" + "test520" + "/" + matchInfos.matchCode;
        ag.PrivacySend("match:" + msg, serverSettings.dpPoid);
		Debug.Log("@@@@@@@@@@@@@@ Match 2");
    }

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
            UnityEngine.SceneManagement.SceneManager.LoadScene("level_village");
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
		//	Application.LoadLevel("MainMenu");
		}	
	}	
	
	void CB_EnterRoom(int code, ArcaletScene scene)
	{
		if(code == 0) {
			Debug.Log("CB_EnterRoom Successed");
			scene.Send("bb_ready:" + ag.gameUserid + "/"  + ag.poid);
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
