using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_upDown : MonoBehaviour
{
    private Vector2 viTriBanDau;
    public float speed, height;
    // Start is called before the first frame update
    void Start()
    {
        viTriBanDau = transform.position;
        StartCoroutine(GoUpAndDown());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GoUpAndDown()
    {
        while (true)
        {
            while (true)
            {
                transform.position = new Vector2(
                    transform.position.x, transform.position.y + speed * Time.deltaTime);
                if (transform.position.y > viTriBanDau.y + height) break;
                yield return null;
            }

            while (true)
            {
                transform.position = new Vector2(
                    transform.position.x, transform.position.y - speed * Time.deltaTime);
                if (transform.position.y < viTriBanDau.y)
                {
                    transform.position = viTriBanDau;
                    break;
                }
                yield return null;
            }
            yield return null;
        }
    }
}
