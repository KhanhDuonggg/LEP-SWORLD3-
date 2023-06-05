using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gach_vo : MonoBehaviour
{
    public UnityEvent _event;
    public GameObject boom;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var direction = collision.GetContact(0).normal;
            if (Mathf.Round(direction.y) == 1)
            {
                _event.Invoke();
                GameObject bumm = Instantiate(boom, transform.position, Quaternion.identity);
                Destroy(bumm, 2);
                Destroy(gameObject, 1);
            }
        }
    }
}
