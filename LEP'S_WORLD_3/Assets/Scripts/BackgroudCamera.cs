using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroudCamera : MonoBehaviour
{
    public GameObject player;
    public float top, bottom;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // lấy vị trí x player
       
        var playY = player.transform.position.y;

        // x cam
         var camY = transform.position.y;
        if (playY > bottom && playY < top)
        {
            camY = playY;
        }
        else
        {
            if (playY < bottom)
            {
                camY = -0.03f;          
            }

            if (playY > top)
            {
                camY = top;
            }
        }
        transform.position = new Vector3(0, camY, -9);
    }
}
