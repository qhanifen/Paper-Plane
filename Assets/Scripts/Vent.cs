using UnityEngine;
using System.Collections;

public class Vent : MonoBehaviour {

    public float force = 200f;
    public float moveSpeed = 5f;

    Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }
     	
	void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {            
            playerRigidbody.AddForce(Vector3.up * force * Time.fixedDeltaTime, ForceMode.Force);
        }
    }
}
