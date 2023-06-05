using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera8 : MonoBehaviour
{
    public GameObject player;
    public float start, end, top, bottom;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // lấy vị trí x player
        var playX = player.transform.position.x;
        var playY = player.transform.position.y;

        // x cam
        var camX = transform.position.x;
        var camY = transform.position.y;
        

        if (playX > start && playX < end)
        {
            camX = playX;

        }
        else
        {
            if (playX < start)
            {
                camX = start;
            }

            if (playX > end)
            {
                camX = end;
            }
        }

        if (playY > bottom && playY < top)
        {
            camY = playY;

        }
        else
        {
            if (playY < bottom)
            {
                camY = bottom;
            }

            if (playY > top)
            {
                camY = top;
            }
        }

        transform.position = new Vector3(camX, camY, -10);
    }
}
