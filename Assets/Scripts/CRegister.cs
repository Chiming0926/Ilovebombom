using UnityEngine;
using System.Collections;

public class CRegister : MonoBehaviour
{

    private const string gguid = "7245577f-4961-7642-a64c-ba5bb008892c";
    private const string sguid = "9c186fa5-c3b1-4f45-80ed-7ebc99aa3bc7";
    byte[] certificate = {  0x43, 0x13, 0xbd, 0x25, 0x4e, 0x7b, 0xa3, 0x4b, 0x86, 0x13, 0xa8, 0xa5, 0x49,
                            0xcf, 0xd1, 0x5e, 0x58, 0x34, 0x2c, 0xda, 0xb, 0x9f, 0xc, 0x41, 0x8a, 0x3e,
                            0xd4, 0x71, 0x5a, 0xb, 0x3d, 0x41, 0x57, 0x1c, 0x53, 0xed, 0xf, 0x52, 0x70,
                            0x47, 0x93, 0xb5, 0x9f, 0x27, 0xe3, 0xa0, 0x66, 0x2d, 0x86, 0x55, 0x5b, 0x9c,
                            0x4b, 0x33, 0xd8, 0x40, 0xbb, 0x66, 0xf1, 0x8c, 0x67, 0x19, 0x8a, 0x4, 0x1a,
                            0x14, 0x28, 0xa7, 0x67, 0x95, 0x72, 0x4c, 0x8c, 0xfb, 0xa, 0xf5, 0x4a, 0x12,
                            0x49, 0x53, 0x6d, 0xf7, 0xa0, 0xd4, 0x73, 0x96, 0x52, 0x43, 0x8e, 0x54, 0x79,
                            0xfa, 0x48, 0xeb, 0x5b, 0xaf, 0xd1, 0x64, 0x20, 0x3d, 0x49, 0xa5, 0xbc, 0x40,
                            0x89, 0xa2, 0xb6, 0xc5, 0x6f, 0xd6, 0xac, 0xfe, 0x2f, 0x92, 0x4d, 0xbc, 0x3f,
                            0xbd, 0x4b, 0x4d, 0x90, 0xf8, 0x50, 0xf2, 0x2, 0x16, 0x75, 0xd4 };

    public GUISkin skin = null;
    void Start ()
    {
        ArcaletSystem.UnityEnvironment();
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
    int system_status = 0;
    void OnGUI()
    {
        GUI.skin = skin;

        int box_width = 500;
        int box_height = 360;

        int start_y = (int)(Screen.height - box_height - 5);
        int start_x = (int)((Screen.width - box_width) / 2);
        if (system_status == 0)
            show_common_gui(start_x, start_y, box_width, box_height);
        else if (system_status == 1)
            show_error_message(start_x, start_y, box_width, box_height);
    }

    void show_common_gui(int start_x, int start_y, int box_width, int box_height)
    {
        GUI.Box(new Rect(start_x, start_y, box_width, box_height), "");

        GUI.Label(new Rect(start_x + 40, start_y + 50, 200, 30), "Account");
        GUI.Label(new Rect(start_x + 40, start_y + 100, 200, 30), "Password");
        GUI.Label(new Rect(start_x + 40, start_y + 150, 200, 30), "Comfirm Password");
        GUI.Label(new Rect(start_x + 40, start_y + 200, 200, 30), "E-Mail");
        GUI.Label(new Rect(start_x + 40, start_y + 250, 200, 30), "Comfirm E-Mail");

        str_acc = GUI.TextField(new Rect(start_x + 200, start_y + 45, 260, 30), str_acc, 12);
        str_pw = GUI.PasswordField(new Rect(start_x + 200, start_y + 95, 260, 30), str_pw, "*"[0], 18);
        str_pw2 = GUI.PasswordField(new Rect(start_x + 200, start_y + 145, 260, 30), str_pw2, "*"[0], 18);
        str_mail = GUI.TextField(new Rect(start_x + 200, start_y + 195, 260, 30), str_mail, 50);
        str_mail2 = GUI.TextField(new Rect(start_x + 200, start_y + 245, 260, 30), str_mail2, 50);

        if (GUI.Button(new Rect(start_x + 20, start_y + 350 - 45, 460, 30), "Register"))
        {
            if (str_pw != str_pw2)
            {
                Debug.LogWarning("Please Confirm Your Password Information");
                system_status = 1;
                return;
            }

            if (str_mail != str_mail2)
            {
                Debug.LogWarning("Please Confirm Your E-mail Information");
                system_status = 1;
                return;
            }

            Regist(str_acc, str_pw, str_mail);
        }
    }

    void CB_Regist(int code, object token)
    {

        if (code == 0)
        {
            /* regist sucessful */
            string[] reg = token as string[]; 
            string acc = reg[0];
            string pw = reg[1];
            string mail = reg[2];
            Debug.Log("Regist Successed - Account:" + acc + " / Password:" +
             pw + " E-Mail" + mail);

        //    if (autoLogin) ArcaletLaunch(acc, pw);
        }
        else
        {
            Debug.LogWarning("Regist Failed - Error:" + code);
        }
    }

    void Regist(string username, string password, string mail)
    {
        string[] registToken = new string[] { username, password, mail };
        ArcaletSystem.ApplyNewUser(gguid, certificate, username, password,
         mail, CB_Regist, registToken);
    }

    void show_error_message(int start_x, int start_y, int box_width, int box_height)
    {
        GUI.Box(new Rect(start_x, start_y + 180, box_width, 200), "");
        GUI.Label(new Rect(start_x + 40, start_y + 230, 400, 30), "Please comfirm your password or E-mail information");
        if (GUI.Button(new Rect(start_x + 20, start_y + 330, 460, 30), "OK"))
        {
            system_status = 0;
        }
    }

}
