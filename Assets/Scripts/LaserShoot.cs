using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    private LineRenderer lr;
    public Transform fp;
    //public Transform tr; 
     
	void Start ()
    {
        lr = GetComponent<LineRenderer>(); 
	}
     
    void Update()
    {
        lr.SetPosition(0, fp.position);
        lr.SetPosition(1, fp.position + fp.right*14);
        RaycastHit2D hit = Physics2D.Raycast(fp.position, fp.right, 15);  
        if (hit.collider)
        {
            Debug.Log("hit" + hit.transform.name);
            //lr.SetPosition(1, hit.point);
        }  
    }
}
