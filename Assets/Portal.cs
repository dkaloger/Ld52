using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal Pair;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Projectile>().latestportal != Pair)
        {
           
                collision.gameObject.GetComponent<Projectile>().latestportal = gameObject.GetComponent<Portal>();
                collision.gameObject.transform.position = Pair.transform.position;
            collision.gameObject.GetComponent<Projectile>().movedirection = collision.gameObject.GetComponent<Projectile>().movedirection * -1;
        }
      
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Projectile>().latestportal == Pair)
        {
            collision.gameObject.GetComponent<Projectile>().latestportal = null;
        }
    }

    void Update()
    {
       
        foreach (Portal item in FindObjectsOfType<Portal>())
        {
            if(item != gameObject.GetComponent<Portal>() && item.tag == gameObject.tag)
            {
                Pair = item;
                break;
            }
        }
    }
}
