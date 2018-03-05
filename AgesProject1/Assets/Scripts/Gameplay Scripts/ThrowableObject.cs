using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour {

    Rigidbody throwableRigidbody;
    float damageToDeal = 1;
    bool hasBeenThrown = false;
    bool dealsDamage = false;

    MeshRenderer meshRenderer;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material dealsDamageMaterial;

    public bool HasBeenThrown
    {
        set
        {
            hasBeenThrown = value;
            if(hasBeenThrown == true)
            {
                dealsDamage = true;
                meshRenderer.material = dealsDamageMaterial;
            }
            else
            {
                dealsDamage = false;
                meshRenderer.material = normalMaterial;
            }
        }
    }

	// Use this for initialization
	void Start () {
        throwableRigidbody = gameObject.GetComponent<Rigidbody>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
	}

    void OnCollisionEnter(Collision other)
    {
        if (dealsDamage)
        {
            //If we collide with something after thrown
            //We want to stop being in the "thrown" state

            HasBeenThrown = false;
            if (other.gameObject.tag == "Player")
            {
                Vector3 currentVelocity = gameObject.GetComponent<Rigidbody>().velocity;

                PlayerHealth otherHealth = other.gameObject.GetComponent<PlayerHealth>();
                PlayerScript otherMovement = other.gameObject.GetComponent<PlayerScript>();

                otherMovement.HandleKnockback(currentVelocity);
                otherHealth.TakeDamage(damageToDeal);
                
            }
        }
    }

}
