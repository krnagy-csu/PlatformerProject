using System;
using UnityEngine;

public class climboxScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            transform.parent.position = new Vector3(transform.parent.position.x,transform.parent.position.y+0.5f,transform.parent.position.z);
            Debug.Log("Climbbox triggered");
            //throw new NotImplementedException();
        }
    }
}
