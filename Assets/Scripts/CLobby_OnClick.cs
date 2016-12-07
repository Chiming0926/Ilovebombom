using UnityEngine;
using System.Collections;

public class CLobby_OnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        if (gameObject.tag == "role")
        {
            Debug.Log("role down");
        }
        else if (gameObject.tag == "props")
        {
            Debug.Log("props down");
        }
        else if (gameObject.tag == "potion")
        {
            Debug.Log("potion down");
        }
        else if (gameObject.tag == "auction")
        {
            Debug.Log("auction down");
        }
        else if (gameObject.tag == "setup")
        {
            Debug.Log("setup down");
        }
    }
}
