using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEmiter : MonoBehaviour
{
   public GameObject ProjectilePrefab;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnturnChange()
    {
       // print("turn change received");
        // might change emision location
        Instantiate(ProjectilePrefab,transform.position, Quaternion.Euler( Vector3.zero));
    }
}
