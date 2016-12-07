using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CPlayer : MonoBehaviour
{
    enum PLAYER_DIRECTION
    {
        UP     = 0,
        DOWN   = 1,
        RIGHT  = 2,
        LEFT   = 3,
        NULL   = 4
    };


    public GameObject water_ball;
    private float speed = 0.1f;
    public static   int wball_cnt = 0;
    public static GameObject static_wball;
    public Texture2D []texture = new Texture2D[9];
    private const int reset_factor = 6;
    private PLAYER_DIRECTION direct = 0;
    private int pic = 0;
    private int cnt = 0;
    private static int cpos_x = 0;
    private static int cpos_y = 0;
    public float MoveForce = 356.0f;
    public static GameObject player;

    private const float start_point_y = 3.6f; 
	void Start ()
    {
        direct = PLAYER_DIRECTION.NULL;
        player = gameObject;
        static_wball = water_ball;
    }

    void set_player_pic(PLAYER_DIRECTION dir)
    {
        if (direct == PLAYER_DIRECTION.NULL)
            return;
        if (direct != dir)
        {
            pic = 0;
            cnt = 0;
        }
        direct = dir;
        bool changePic = false;
        if ((cnt / reset_factor) >= 1)
        {
            if ((cnt % reset_factor) == 0)
                changePic = true;
        }
        int pic_num = pic;
        switch (direct)
        {
            case PLAYER_DIRECTION.UP:
                pic_num += 6;
                break;
            case PLAYER_DIRECTION.RIGHT:
                pic_num += 3;
                break;
            case PLAYER_DIRECTION.LEFT:
                pic_num += 3;
                break;
            default:
                break;
        }
        if (changePic)
        {
            SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
            Sprite s = Sprite.Create(texture[pic_num], new Rect(0, 0, texture[pic_num].width, texture[pic_num].height), new Vector3(0.5f, 0.5f, 0));
            spr.sprite = s;
            pic++;
            if (pic >= 3)
                pic = 0;
        }
        cnt++;
    }
    // 3.4 
/*
    void reset_y(Vector3 pos)
    {
        for (int i=0; i< CBackground.map_height; i++)
        {
            if (pos.y <= 3.6f - i*0.632f
                && pos.y > 3.6f - (i+1) * 0.632f)
            {
                Debug.Log("@@@@@@@ reset y ");
                if (3.6f - i * 0.632f - pos.y > pos.y - 3.6f + (i + 1) * 0.632f)
                {
                    pos = new Vector3(pos.x, 3.6f - (i + 1) * 0.632f, -1);
                    cpos_y = i - 1;
                }
                else
                {
                    cpos_y = i;
                    pos = new Vector3(pos.x, 3.6f - i * 0.632f, -1);
                }
                return;
            }
        }
    }

    void reset_x(Vector2 pos)
    {
        for (int i = 0; i < CBackground.map_width; i++)
        {
            if (pos.x >= -4.8f + i * 0.632f
                && pos.x < -4.8f + (i + 1) * 0.632f)
            {
                if (pos.x + 4.8f - i * 0.632f > 4.8f + (i + 1) * 0.632f - pos.x)
                {
                    Debug.Log("@@@@@@@@ 111 4.8f + (i + 1) * 0.632f = " + 4.8f + (i + 1) * 0.632f);
                    cpos_x = i - 1;
                    pos = new Vector3(-4.8f + (i + 1) * 0.632f, pos.y, -1);
                }
                else
                {
                    cpos_x = i;
                    Debug.Log("@@@@@@@@ 222 4.8f + i * 0.632f = " + 4.8f + i * 0.632f);
                    pos = new Vector3(-4.8f + i * 0.632f, pos.y, -1);
                }
                return;
            }
        }
    }
*/
    public static void create_wball()
    {
        Vector3 pos = player.transform.position;
        for (int i = 0; i < CBackground.map_height; i++)
        {
            if (pos.y <= 4.5f - i * 1.0f
                && pos.y > 4.5f - (i + 1) * 1.0f)
            {
                if (4.5f - i * 1.0f - pos.y > pos.y - 4.5f + (i + 1) * 1.0f)
                {
                    pos = new Vector3(pos.x, 4.5f - (i + 1) * 1.0f, -1);
                    cpos_y = i - 1;
                }
                else
                {
                    cpos_y = i;
                    pos = new Vector3(pos.x, 4.5f - i * 1.0f, -1);
                }
                break;
            }
        }

        for (int i = 0; i < CBackground.map_width; i++)
        {
            if (pos.x >= -7.5f + i * 1.0f
                && pos.x < -7.5f + (i + 1) * 1.0f)
            {
                if (pos.x + 7.5f - i * 1.0f > 7.5f + (i + 1) * 1.0f - pos.x)
                {
                    cpos_x = i;
                    pos = new Vector3(-7.5f + (i + 1) * 1.0f, pos.y, -1);
                }
                else
                {
                    cpos_x = i+1;
                    pos = new Vector3(-7.5f + i * 1.0f , pos.y, -1);
                }
                break;
            }
        }
        Debug.Log("CreateBall ");
        Instantiate(static_wball, new Vector3(pos.x, pos.y), player.transform.rotation);
    }

    public void BeginMove()
    {
    }

    public void EndMove()
    {
        direct = PLAYER_DIRECTION.NULL;
        var r = this.GetComponent<Rigidbody2D>();
        r.velocity = new Vector2(0, 0);
    }

    public void UpdateDirection(Vector3 direction)
    {
        if (Math.Abs(direction.x) == Math.Abs(direction.y))
        {
            /* don't change */
        }
        else if (Math.Abs(direction.x) > Math.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                direct = PLAYER_DIRECTION.RIGHT;
            }
            else
            {
                direct = PLAYER_DIRECTION.LEFT;
            }
        }
        else
        {
            if (direction.y > 0)
                direct = PLAYER_DIRECTION.UP;
            else
                direct = PLAYER_DIRECTION.DOWN;
        }
    }

    void player_move(PLAYER_DIRECTION dir)
    {
        
        var r = this.GetComponent<Rigidbody2D>();
        if (dir == PLAYER_DIRECTION.RIGHT)
        {
            set_player_pic(PLAYER_DIRECTION.RIGHT);
            if (restricted_area(gameObject.transform.position.x, gameObject.transform.position.y, PLAYER_DIRECTION.RIGHT) == false)
            {
                if (gameObject.transform.position.x <= 7.5f)
                {
                    gameObject.transform.position += new Vector3(speed, 0, 0);
                }
            }
        }
        else if (dir == PLAYER_DIRECTION.LEFT)
        {
            set_player_pic(PLAYER_DIRECTION.LEFT);
            if (restricted_area(gameObject.transform.position.x, gameObject.transform.position.y, PLAYER_DIRECTION.LEFT) == false)
            {
                if (gameObject.transform.position.x >= -7.5)
                {
                    gameObject.transform.position += new Vector3(-speed, 0, 0);
                }
            }
        }
        else if (dir == PLAYER_DIRECTION.UP)
        {
            set_player_pic(PLAYER_DIRECTION.UP);
            if (restricted_area(gameObject.transform.position.x, gameObject.transform.position.y, PLAYER_DIRECTION.UP) == false)
            {
                if (gameObject.transform.position.y <= 4.5f)
                {
                    var sr = this.GetComponent<SpriteRenderer>();
                    gameObject.transform.position += new Vector3(0, speed, 0);
                    sr.sortingOrder = (int)((4.5f - gameObject.transform.position.y)) + 7;
                    if (4.5f - gameObject.transform.position.y != 0.0f)
                    {
                        sr.sortingOrder++;
                    }
                    Debug.Log("@@@@@@@@layer = " + gameObject.layer + ", y = " + gameObject.transform.position.y);
                }
            }
        }
        else if (dir == PLAYER_DIRECTION.DOWN)
        {
            set_player_pic(PLAYER_DIRECTION.DOWN);
            if (restricted_area(gameObject.transform.position.x, gameObject.transform.position.y, PLAYER_DIRECTION.DOWN) == false)
            {
                if (gameObject.transform.position.y >= -4.5f)
                {
                    var sr = this.GetComponent<SpriteRenderer>();
                    gameObject.transform.position += new Vector3(0, -speed, 0);
                    sr.sortingOrder = (int)((4.5f - gameObject.transform.position.y)) + 7;
                    if (4.5f - gameObject.transform.position.y != 0.0f)
                        sr.sortingOrder++;
                    Debug.Log("@@@@@@@@layer = " + gameObject.layer + ", y = " + gameObject.transform.position.y);
                }
            }
        }
        else
        {
            r.velocity = new Vector2(0.0f, 0.0f);
        }
        
        direct = dir;
    }

    void Update ()
    {
        // var r = this.GetComponent<Rigidbody2D>();
        set_player_pic(direct);
        player_move(direct);
        /*
        if (Input.GetKey(KeyCode.RightArrow))
        {
            set_player_pic(direct);
            player_move(direct);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            set_player_pic(PLAYER_DIRECTION.LEFT);
            player_move(PLAYER_DIRECTION.LEFT);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            set_player_pic(PLAYER_DIRECTION.UP);
            player_move(PLAYER_DIRECTION.UP);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            set_player_pic(PLAYER_DIRECTION.DOWN);
            player_move(PLAYER_DIRECTION.DOWN);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (wball_cnt < 1)
            {
                create_wball();
                wball_cnt++;
            }
        }*/

    }
    public void OnClick()
    {
        if (wball_cnt < 3)
        {
            Debug.Log("@@@@@@ws");
            create_wball();
            wball_cnt++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
    /*
        Debug.Log("@@@@@ OnCollisionEnter2D");
        switch (direct)
        {
            case PLAYER_DIRECTION.UP:
                gameObject.transform.position += new Vector3(0, -0.01f, 0);
                break;
            case PLAYER_DIRECTION.DOWN:
                gameObject.transform.position += new Vector3(0, 0.01f, 0);
                break;
            case PLAYER_DIRECTION.RIGHT:
                gameObject.transform.position += new Vector3(-0.01f, 0, 0);
                break;
            case PLAYER_DIRECTION.LEFT:
                gameObject.transform.position += new Vector3(0.01f, 0, 0);
                break;
            default:
                break;
        }
    */
    }

    void OnCollisionStay(Collision collision)
    {
    }
    public static void create_wball_from()
    {

    }

    public static void wball_destroy()
    {
        wball_cnt--;
        if (wball_cnt < 0)
            wball_cnt = 0;
    }

    bool restricted_area(float x, float y, PLAYER_DIRECTION dir)
    {
        return false;
        /*
        switch (direct)
        {
            case PLAYER_DIRECTION.UP:
                y+=speed; 
                break;
            case PLAYER_DIRECTION.DOWN:
                y-=speed;
                break;
            case PLAYER_DIRECTION.RIGHT:
                x+=speed;
                break;
            case PLAYER_DIRECTION.LEFT:
                x-=speed;
                break;
            default:
                break;
        }
        float array_x = (float)((x+7.5f)/1.0f);
        float array_y = (float)((4.5f-y)/1.0f);
        Debug.Log("@@@@@@@ x = " + x + ", y = " + y);
        Debug.Log("@@@@@@@ array_x = " + array_x + ", array_y = " + array_y);
        switch (direct)
        {
            case PLAYER_DIRECTION.UP:
            case PLAYER_DIRECTION.DOWN:
                if ((int)((x + 4.8f) % 0.632f) != 0)
                {
                    if (CBackground.map[(int)array_y, (int)array_x] == 1 || CBackground.map[(int)array_y, (int)array_x+1] == 1)
                    {
                        Debug.Log("@@@@@@@ don't go");
                        return true;
                    }
                }
                else
                {
                    if (CBackground.map[(int)array_y, (int)array_x] == 1)
                    {
                        Debug.Log("@@@@@@@ don't go");
                        return true;
                    }
                }
                break;
            case PLAYER_DIRECTION.RIGHT:
            case PLAYER_DIRECTION.LEFT:
                if ((int)((3.4f - y) % 0.632f) != 0)
                {
                    Debug.Log("@@@@@@@ hahaha");
                    if (CBackground.map[(int)array_y, (int)array_x] == 1 || CBackground.map[(int)array_y+1, (int)array_x] == 1)
                    {
                        Debug.Log("@@@@@@@ right diff don't go");
                        return true;
                    }
                }
                else
                {
                    Debug.Log("@@@@@@@ hehehe(int)array_y = " + (int)array_y);
                    if (CBackground.map[(int)array_y+1, (int)array_x] == 1)
                    {
                        Debug.Log("@@@@@@@ right don't go");
                        return true;
                    }
                }
                break;
            default:
                break;
        }
        Debug.Log("@@@@@@@ x = " + x + ", y = " + y);
        Debug.Log("@@@@@@@ array_x = " + array_x + ", array_y = " + array_y);
        
        return false;*/
    }

    public class PlayerData
    {
        public static string user_account;
        public static string user_password;
        public static string user_mail;
        public static string user_name;
        public static bool fb_login;
    }
}
