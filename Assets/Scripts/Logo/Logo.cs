using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour {

	public Texture2D bg;
	
	IEnumerator Start()
	{
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("Login");
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(180,150,240,100), bg);
	}
}
