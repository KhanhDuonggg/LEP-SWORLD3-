using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.SetActive(false);
            StartCoroutine(loadScene());
        }

        IEnumerator loadScene()
        {
           
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(2);
        }
    }
}
