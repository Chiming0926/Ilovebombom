using UnityEngine;
using System.Collections;

public class CWBall : MonoBehaviour
{
    public GameObject ws_up;
    public GameObject ws_down;
    public GameObject ws_left;
    public GameObject ws_right;
    public Texture2D[] texture = new Texture2D[2];
    public Texture2D textureboom;
    public static Object[] obj = new GameObject[20];
    private const int reset_factor = 5;
    private int pic = 0;
    private int cnt = 0;
    public int power = 1;
    // Use this for initialization
    void Start ()
    {
	
	}
	
    void create_ws()
    {
        int i = 0;
        for (i = 0; i < power; i++)
        {
            Instantiate(ws_up, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f * (i + 1), 0), gameObject.transform.rotation);
            Instantiate(ws_down, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f * (i - 1), 0), gameObject.transform.rotation);
            Instantiate(ws_right, new Vector3(gameObject.transform.position.x + 1.0f * (i + 1), gameObject.transform.position.y, 0), gameObject.transform.rotation);
            Instantiate(ws_left, new Vector3(gameObject.transform.position.x + 1.0f * (i - 1), gameObject.transform.position.y, 0), gameObject.transform.rotation);
        }
    }

    void destroy_ws()
    {
        destroy_obstacle(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f));
        destroy_obstacle(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y));

        for (int i=0; i<power; i++)
        {
            destroy_obstacle(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f * (i + 1)));
            destroy_obstacle(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f * (i - 1)));
            destroy_obstacle(new Vector2(gameObject.transform.position.x + 1.0f * (i + 1), gameObject.transform.position.y));
            destroy_obstacle(new Vector2(gameObject.transform.position.x + 1.0f * (i - 1), gameObject.transform.position.y));
        }
    }

    void destroy_obstacle(Vector2 pos)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos, new Vector2(0.1f, 0.1f), 0.0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
            {
				/* Send death message */
				continue;
            }
            Destroy(collider.gameObject);   
        }
    }

    // Update is called once per frame
    void Update ()
    {
        bool changePic = false;
        if (cnt < 120)
        {
            if ((cnt / reset_factor) >= 1)
            {
                if ((cnt % reset_factor) == 0)
                    changePic = true;
            }

            if (changePic)
            {
                pic++;
                SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
                Sprite s = Sprite.Create(texture[pic % 2], new Rect(0, 0, texture[pic % 2].width, texture[pic % 2].height), new Vector3(0.5f, 0.5f, 0));
                spr.sprite = s;
            }
        }
        else if (cnt >= 120 && cnt <130)
        {
            create_ws();
            SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
            Sprite s = Sprite.Create(textureboom, new Rect(0, 0, textureboom.width, textureboom.height), new Vector3(0.5f, 0.5f, 0));
            spr.sprite = s;
        }
        else
        {
            destroy_ws();
            Destroy(gameObject);
            CPlayer.wball_destroy();
        }

        cnt++;
    }
}
