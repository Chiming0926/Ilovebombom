using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using Facebook.Unity;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
using System;

public class CLogin : MonoBehaviour
{
    private const string gguid = "7245577f-4961-7642-a64c-ba5bb008892c";
    private const string sguid = "346adc56-b1b1-994f-b845-54e142d3f0ff";
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
    AGCC m_agcc = null;

    public GUISkin myskin = null;
    // Use this for initialization
    void Start ()
    {
        FB.Init(init_complete);
        m_agcc = FindObjectOfType(typeof(AGCC)) as AGCC;
		if(m_agcc == null) return;
    }

    void init_complete()
    {
    }
    // Update is called once per frame
    void Update ()
    {
	
	}

    int fb_login = 0;

    string str_acc = "";
    string str_pw = "";

    void OnGUI()
    {
        int box_width = 500; 
        int box_height = 300;
        GUI.skin = myskin;
        /* text background is width = 612 and height is 312 */
        int start_y = (int)(Screen.height - box_height - 5);
        int start_x = (int)((Screen.width - box_width) / 2);
        GUI.Box(new Rect(start_x, start_y, box_width, box_height), "");
   /*     GUI.Label(new Rect(start_x + 70, start_y + 50, 100, 30), "Account");
        GUI.Label(new Rect(start_x + 70, start_y + 100, 100, 30), "Password");

        str_acc = GUI.TextField(new Rect(start_x + 150, start_y + 45, 320, 30), str_acc, 10);
        str_pw = GUI.PasswordField(new Rect(start_x + 150, start_y + 105, 320, 30), str_pw, "*"[0], 20);

        if (GUI.Button(new Rect(start_x + 70, start_y + 165, 180, 30), "Login"))
        {
            fb_login = 0;
        }

        if (GUI.Button(new Rect(start_x + 290, start_y + 165, 180, 30), "Register"))
        {
            SceneManager.LoadSceneAsync("Register");
        }*/
        if (GUI.Button(new Rect(start_x + 70, start_y + 215, 400, 30), "Facebook Login"))
        {
            fb_login = 1;
            FB.LogInWithPublishPermissions(new List<string>() { "public_profile", "email", "user_friends" }, fb_login_callback);
        }
    }

    void fb_login_callback(IResult result)
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("login result = " + result.RawResult);
            Debug.Log("FB login is successful, token = " +  AccessToken.CurrentAccessToken.TokenString);
            FB.API("/me?fields=id,name,email", HttpMethod.GET, user_callback);
        }
        else
        {
            Debug.Log("FB login is not successful");
        }
    }

    string user_account;
    string user_password;
    string user_mail;

    void user_callback(IResult result)
    {
        string md5 = getMd5Method(result.ResultDictionary["email"].ToString());
    //    string md5 = getMd5Method("Chiming0928@gmail.com");
        if (md5 != null)
        {
            user_mail = result.ResultDictionary["email"].ToString();
         //   user_mail = "Chiming0928@gmail.com";
            user_password = md5.Substring(16, 16);
            user_account = md5.Substring(16, 10);
            Debug.Log("user_account = " + user_account + ", user_password = " + user_password);
            m_agcc.ArcaletLaunch(user_account, user_password, user_mail);


        }
    }

    private string getMd5Method(string input)
    {
        try
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            byte[] myData = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < myData.Length; i++)
            {
                sBuilder.Append(myData[i].ToString("x"));
            }

            return string.Format("ComputeHash(16)：{0}", sBuilder.ToString());
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Can't get user's email");
        }
        return null;
    }
}
