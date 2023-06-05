using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_xanh : MonoBehaviour
{
    public GameObject goXanh;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(goXanhh());
    }


    IEnumerator goXanhh()
    {
        while (true)
        {
            if (isActive)
            {
                goXanh.SetActive(false);
                isActive = false;
            }
            else
            {
                goXanh.SetActive(true);
                isActive = true;
            }
            
            yield return new WaitForSeconds(3);
        }
    }
}
