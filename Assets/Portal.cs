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
        
    }
}
