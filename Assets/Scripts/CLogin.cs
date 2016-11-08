﻿using UnityEngine;
using System.Collections;

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
    ArcaletGame ag = null;
    // Use this for initialization
    void Start ()
    {
        ArcaletSystem.UnityEnvironment();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void CB_ArcaletLaunch(int code, ArcaletGame game)
    {
        if (code == 0)
        {
            Debug.Log("Login Successed");
        }
        else
        {
            Debug.LogWarning("Login Failed - Code:" + code);
        }
    }

    void ArcaletLaunch(string username, string password)
    {
        ag = new ArcaletGame(username, password, gguid, sguid, certificate);
        ag.onCompletion += CB_ArcaletLaunch;
        ag.Launch();
    }

    string str_acc = "";
    string str_pw = "";

    void OnGUI()
    {
        GUI.Box(new Rect(100, 100, 300, 150), "");
        GUI.Label(new Rect(150, 120, 100, 30), "Account");
        GUI.Label(new Rect(150, 170, 100, 30), "Password");

        str_acc = GUI.TextField(new Rect(250, 120, 100, 30), str_acc, 10);
        str_pw = GUI.PasswordField(new Rect(250, 170, 100, 30), str_pw, "*"[0], 20);

        if (GUI.Button(new Rect(250, 220, 100, 30), "Login"))
        {
            ArcaletLaunch(str_acc, str_pw);
        }

        if (GUI.Button(new Rect(120, 220, 100, 30), "Register"))
        {
            ArcaletLaunch(str_acc, str_pw);
        }
    }
}