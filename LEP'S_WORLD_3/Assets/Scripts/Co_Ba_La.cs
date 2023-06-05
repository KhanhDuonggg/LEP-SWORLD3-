using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Co_Ba_La : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 viTriBanDau;
    public Sprite newBlock;
    public float speed, height;
    private bool canChange;
    public GameObject item, item1,item2;
    GameObject newItem;
  


    private void Start()
    {
        viTriBanDau = transform.position;
        canChange = true;
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canChange) { return; }
        if (collision.gameObject.CompareTag("Player"))
        {
            var direction = collision.GetContact(0).normal;
            if (Mathf.Round(direction.y) == 1)
            {
                // nay len rot xuong

                StartCoroutine(GoUpAndDown());

                // chuyen thanh hinh khac

                // tao vat pham
                int random = Random.Range(0, 3);

                if (random == 0)
                {
                    newItem = Instantiate<GameObject>(item);
                }
                else if (random == 1)
                {
                    newItem = Instantiate<GameObject>(item1);
                }
                else
                {
                    newItem = Instantiate<GameObject>(item2);
                }

                newItem.transform.position = new Vector2(viTriBanDau.x, viTriBanDau.y + 1);


                // phat nhat


                StartCoroutine(ItemCoin(newItem));
                canChange = false;
                GetComponent<Animator>().enabled= false;
                GetComponent<SpriteRenderer>().sprite = newBlock;
                // GetComponent<Animator>().enabled = false;
                Debug.Log(">>>>>des");
            }
            
         

        }

    }


    IEnumerator GoUpAndDown()
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
    }

    IEnumerator ItemCoin(GameObject newItem)
    {
        while (true)
        {
            newItem.transform.position = new Vector2(
                 newItem.transform.position.x, newItem.transform.position.y + 2f * Time.deltaTime);
            if (newItem.transform.position.y > viTriBanDau.y + 0.5)
            {
                break;
            }
            yield return null;
        }
    }
}
