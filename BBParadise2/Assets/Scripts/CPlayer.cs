using UnityEngine;
using System.Collections;

public class CPlayer : MonoBehaviour {

	void Start ()
    {
	
	}
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (gameObject.transform.position.x <= 5.97f)
                gameObject.transform.position += new Vector3(0.01f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (gameObject.transform.position.x >= -5.97f)
                gameObject.transform.position += new Vector3(-0.01f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (gameObject.transform.position.y <= 4.64f)
                gameObject.transform.position += new Vector3(0, 0.01f, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (gameObject.transform.position.y >= -4.64f)
                gameObject.transform.position += new Vector3(0, -0.01f, 0);
        }
    }
}
