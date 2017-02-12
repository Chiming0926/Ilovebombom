using UnityEngine;
using System.Collections;

public class CFBLogin : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (gameObject.tag == "fb_login")
        {
            AGCC.BBDebug(AGCC.BBDEBUG_INFO, "facebook loggin");
            CLogin login;
            login = FindObjectOfType(typeof(CLogin)) as CLogin;
            if (login == null)
            {
                AGCC.BBDebug(AGCC.BBDEBUG_ERROR, "Login Object is null");
                return;
            }
            login.onFBLoingOnClick();
        }
    }
}
