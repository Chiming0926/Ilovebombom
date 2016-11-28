using UnityEngine;
using System.Collections;

public class CRegister : MonoBehaviour
{
    public GUISkin skin = null;
    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    string str_acc = "";
    string str_pw = "";
    string str_pw2 = "";
    string str_mail = "";
    string str_mail2 = "";

    void OnGUI()
    {
        GUI.skin = skin;

        int box_width = 500;
        int box_height = 360;

        int start_y = (int)(Screen.height - box_height - 5);
        int start_x = (int)((Screen.width - box_width) / 2);
        GUI.Box(new Rect(start_x, start_y, box_width, box_height), "");

        GUI.Label(new Rect(start_x + 40, start_y + 50, 200, 30), "Account");
        GUI.Label(new Rect(start_x + 40, start_y + 100, 200, 30), "Password");
        GUI.Label(new Rect(start_x + 40, start_y + 150, 200, 30), "Comfirm Password");
        GUI.Label(new Rect(start_x + 40, start_y + 200, 200, 30), "E-Mail");
        GUI.Label(new Rect(start_x + 40, start_y + 250, 200, 30), "Comfirm E-Mail");

        str_acc = GUI.TextField(new Rect(start_x + 200, start_y + 45, 260, 30), str_acc, 10);
        str_pw = GUI.PasswordField(new Rect(start_x + 200, start_y + 95, 260, 30), str_pw, "*"[0], 18);
        str_pw2 = GUI.PasswordField(new Rect(start_x + 200, start_y + 145, 260, 30), str_pw2, "*"[0], 18);
        str_mail = GUI.TextField(new Rect(start_x + 200, start_y + 195, 260, 30), str_mail, 50);
        str_mail2 = GUI.TextField(new Rect(start_x + 200, start_y + 245, 260, 30), str_mail2, 50);

        if (GUI.Button(new Rect(start_x + 20, start_y + 350 - 45, 460, 30), "Register"))
        {
            
        }
    }

}
