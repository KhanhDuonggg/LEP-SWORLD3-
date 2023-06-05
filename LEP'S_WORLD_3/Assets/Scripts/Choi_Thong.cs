using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_Thong : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private float speedUp;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(speedUp, 0);
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setspeed(float value)
    {
        speedUp = value;
    }
}
