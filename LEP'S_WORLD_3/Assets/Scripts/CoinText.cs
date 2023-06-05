using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    public static int countCoin = 0;
    public TMP_Text coinText;
    public AudioSource soundCoin;
    // Start is called before the first frame update
    void Start()
    {
        // set score
        if (LoginUser.loginResponseMoel.score >= 0)
        {
            countCoin = LoginUser.loginResponseMoel.score;
            coinText.text = countCoin + "  x";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("coin"))
        {
            countCoin++;
            coinText.text = countCoin + ("  x");
            soundCoin.Play();
            Destroy(collision.gameObject);
        }
    }
}
