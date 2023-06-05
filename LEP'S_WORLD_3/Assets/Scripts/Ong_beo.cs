using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ong_beo : MonoBehaviour
{
    public float start, end;
    private bool isRight;
    private Vector2 viTriBanDau;
    public float speed, height, speedUp;
    public Sprite newSprite;

    void Start()
    {
        viTriBanDau = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var positionE = transform.position.y;
     

        if (positionE < start)
        {
            isRight = true;
        }
        if (positionE > end)
        {
            isRight = false;
        }

        Vector2 scale = transform.localScale;
        if (isRight)
        {
            scale.x = -1;
            transform.Translate(Vector3.up * 2f * Time.deltaTime);
        }
        else
        {
            scale.x = 1;
            transform.Translate(Vector3.down * 2f * Time.deltaTime);
        }

        // up lại scale
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("quai"))
        {
            isRight = !isRight;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            var direction = collision.GetContact(0).normal;
            if (Mathf.Round(direction.y) == -1)
            {
                GetComponent<SpriteRenderer>().sprite = newSprite;
                GetComponent<Animator>().enabled = false;
                viTriBanDau = transform.position;
                GetComponent<BoxCollider2D>().isTrigger = true;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                StartCoroutine(GoUp());
                Destroy(gameObject, 3);
            }
           
        }

        if (collision.gameObject.CompareTag("qua_thong_nen"))
        {

            GetComponent<SpriteRenderer>().sprite = newSprite;
            GetComponent<Animator>().enabled = false;
            viTriBanDau = transform.position;
            GetComponent<BoxCollider2D>().isTrigger = true;
            StartCoroutine(GoUp());
            Destroy(gameObject, 3);

        }
    }

    IEnumerator GoUp()
    {
        while (true)
        {
            transform.position = new Vector2(
                transform.position.x, transform.position.y + speedUp * Time.deltaTime);
            if (transform.position.y > viTriBanDau.y + height) break;
            yield return null;

        }
    }
}
