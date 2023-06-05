using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Demon : MonoBehaviour
{
    public float start, end, speed;
    private bool isRight;
    private float timeSpawn;
    private float time;
    public GameObject bossFire, win;
    private Animator animator;
    public bool isFire;
    private int live = 10;
    public TMP_Text wins;

    //dí
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        timeSpawn = 2;
        time = timeSpawn;
        animator= GetComponent<Animator>();
        isFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        var positionE = transform.position.x;
        var positiony = transform.position.y;

        //dí player
         var positionPlay = player.transform.position.x;
        var positionPlayY = player.transform.position.y;
        
        if (positionPlay > start && positionPlay < end && positiony > positionPlayY)
        {
            // tự động quay mặt
            if (positionPlay < positionE) isRight = false;
            if (positionPlay > positionE) isRight = true;
            speed = 3;
            
        }

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
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            scale.x = 1;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // up lại scale
        transform.localScale = scale;

        StartCoroutine(fire());
        isFire = false;
        if(live == 0)
        {
            StartCoroutine(demonDie());
            Destroy(gameObject,2);
            Time.timeScale = 0;
            win.SetActive(true);
            wins.text = "you win";
        }
    }

    

    IEnumerator fire()
    {
        if (isFire)
        {
            animator.SetBool("fire", true);
            yield return new WaitForSeconds(1f);
            bossFire.SetActive(true);
            yield return new WaitForSeconds(1f);
            bossFire.SetActive(false);
            animator.SetBool("fire", false);       
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isFire= true;
            speed = 0;
        }
        if (collision.gameObject.CompareTag("qua_thong_nen"))
        {
            StartCoroutine(demonDie());
            live--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
    }

    IEnumerator demonDie()
    {
        int i = 10;
        while (true)
        {
            animator.SetTrigger("die");
            if (i < 0)
            {
                break;
            }
            i--;
            yield return null;
        }
    }
}
