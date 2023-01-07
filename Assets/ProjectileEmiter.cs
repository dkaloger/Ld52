using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEmiter : MonoBehaviour
{
   public GameObject ProjectilePrefab;
    public GameObject latest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnturnChange()
    {
       // print("turn change received");
        // might change emision location
       latest= Instantiate(ProjectilePrefab,transform.position, Quaternion.Euler( Vector3.zero));
        latest.GetComponent<Projectile>().dontkill = gameObject;
    }
}
