using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirect : MonoBehaviour
{
  public  Vector3 redirectDirection;
  public Vector3 redirectRotation;
    // public Animation[] allanimations;
    public string animtoplay;
    public Sprite[] allsprites;
    public float triggerdistance;
    public bool shot;
    public Sprite mysprite;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().enabled = false;
        if(redirectDirection == new Vector3(0,1,0))
        {
            redirectRotation = new Vector3(0, 0, 0);
            GetComponent<SpriteRenderer>().sprite = allsprites[0];
            mysprite = allsprites[0];
            animtoplay = "shootvertical";
        }
        if (redirectDirection == new Vector3(1, 0, 0))
        {
            redirectRotation = new Vector3(0, 0, 90);
            GetComponent<SpriteRenderer>().sprite = allsprites[1];
            mysprite = allsprites[1];
            animtoplay = "shoothorizontal";
        }
        if (redirectDirection == new Vector3(0, -1, 0))
        {
            redirectRotation = new Vector3(0, 0, 180);
            GetComponent<SpriteRenderer>().sprite = allsprites[0];
            mysprite = allsprites[0];
            GetComponent<SpriteRenderer>().flipY = true;
            animtoplay = "shootvertical";
        }
        if (redirectDirection == new Vector3(-1, 0, 0))
        {
            redirectRotation = new Vector3(0, 0, -90);
            GetComponent<SpriteRenderer>().sprite = allsprites[1];
            mysprite = allsprites[1];
            GetComponent<SpriteRenderer>().flipX = true;
            animtoplay = "shoothorizontal";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    void OnTriggerStay2D(Collider2D collision)
    {


        if(Vector3.Distance(collision.transform.position , transform.position) < triggerdistance&& !shot)
        {
            GetComponent<Animator>().enabled = true;

            shot = true;
            GetComponent<Animator>().SetTrigger(animtoplay);
            collision.gameObject.GetComponent<Projectile>().movedirection = redirectDirection;
            collision.transform.rotation = Quaternion.Euler(redirectRotation);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        shot = false;
        GetComponent<SpriteRenderer>().sprite = mysprite;   

        GetComponent<Animator>().enabled = false;
    }
}
