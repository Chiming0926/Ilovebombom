using UnityEngine;
using System.Collections;

public class CStattGameDilalogOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnMouseDown()
    {
        if (gameObject.GetComponent<Renderer>().enabled == false)
            return;
        if (gameObject.tag == "start_game_dialog_close")
        {
            if (CStartGameDialog.show_new_game_dialog == 1)
                CStartGameDialog.show_new_game_dialog = 0;
            else
                CLobby_OnClick.show_start_game_dialog = 0;
        }
        else if (gameObject.tag == "start_new_game_player_fight")
        {
            CStartGameDialog.show_new_game_dialog = 1;
        }
        else if (gameObject.tag == "start_new_game_challeage")
        {

        }
    }

}
