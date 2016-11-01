using UnityEngine;
using System.Collections;

public class CBackground : MonoBehaviour
{
    public GameObject box;
    public static int map_width  = 16;
    public static int map_height = 10;

    private float start_x = -7.5f;
    private float start_y = 4.5f;

    public static int[,] map = {
                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                       };

    // Use this for initialization
    void Start ()
    {
        /* map array */

        for (int i=0; i< map_height; i++)
        {
            for (int j=0; j<map_width; j++)
            {
                if (map[i, j] == 1)
                {
                    box.layer = i + 8;
                    Instantiate(box, new Vector3(start_x + 1.0f * j, 4.5f - 1.0f * i, 0), gameObject.transform.rotation);
                }
            }
        }
    }

    

	

	// Update is called once per frame
	void Update ()
    {
	
	}
}
