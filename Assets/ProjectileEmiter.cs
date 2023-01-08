using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEmiter : MonoBehaviour
{
   public GameObject ProjectilePrefab;
    public GameObject latest;
    public Sprite nococonut, withcoconut;
    public Vector3 shootdirection;
    public GameObject pointer;
    // Start is called before the first frame update
    void Start()
    {

     
        if (shootdirection != Vector3.zero)
        {
            PlayerPrefs.SetInt("shootX", (int)shootdirection.x);
            PlayerPrefs.SetInt("shootY", (int)shootdirection.y);
        }
        else
        {
            shootdirection.x = (int)PlayerPrefs.GetInt("shootX");
            shootdirection.y = (int)PlayerPrefs.GetInt("shootY");
        }
        if (shootdirection == new Vector3(0, 1, 0))
        {
            pointer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }
        if (shootdirection == new Vector3(1, 0, 0))
        {
            pointer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));

        }
        if (shootdirection == new Vector3(0, -1, 0))
        {
            pointer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));

        }
        if (shootdirection == new Vector3(-1, 0, 0))
        {
            pointer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

        }
    }

    private IEnumerator shooting()
    {
        GetComponent<SpriteRenderer>().sprite = nococonut;
       // yield return new WaitForSeconds(1);
        latest = Instantiate(ProjectilePrefab, transform.position, Quaternion.Euler(Vector3.zero));

        latest.GetComponent<Projectile>().movedirection = shootdirection;
        latest.GetComponent<Projectile>().dontkill = gameObject;
        yield return new WaitForSeconds(4);
        GetComponent<SpriteRenderer>().sprite = withcoconut;

    }
    // Update is called once per frame
    public void OnturnChange()
    {
        // print("turn change received");
        // might change emision location
        if(GameObject.FindWithTag("Level").GetComponent<Turnmanager>().CurrentTurn == GameObject.FindWithTag("Level").GetComponent<Turnmanager>().Maxturns)
        {
            StartCoroutine("shooting");
        }


    }
}
