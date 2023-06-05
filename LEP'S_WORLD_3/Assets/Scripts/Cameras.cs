using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    
    public GameObject player;
    public float start, end;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // lấy vị trí x player
        var playX = player.transform.position.x;
        // x cam
        var camX = transform.position.x;
        if (playX > start && playX < end)
        {
            camX = playX;           
        }
        else
        {
            if(playX < start)
            {
                camX = start;
            }

            if (playX > end)
            {
                camX = end;
            }
        }

        transform.position = new Vector3(camX, 0, -10);
    }
}
