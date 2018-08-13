using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour {

    Rigidbody rigidbody;
    BoxCollider boxCollider;

    float damageToDeal = 1;
    bool hasBeenThrown = false;
    bool dealsDamage = false;

    MeshRenderer meshRenderer;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material dealsDamageMaterial;

    public GameObject ObjectToFollow { get; set; }

    bool beingHeld = false;
    public bool BeingHeld {
        set
        {
            beingHeld = value;
            if (beingHeld == true)
            {
                DisableMovement();
            }
            else
            {
                EnableMovement();
            }
        }
    }

    public bool HasBeenThrown
    {
        set
        {
            hasBeenThrown = value;
            if(hasBeenThrown == true)
            {
                dealsDamage = true;
                meshRenderer.material = dealsDamageMaterial;
                transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            }
            else
            {
                dealsDamage = false;
                meshRenderer.material = normalMaterial;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

	// Use this for initialization
	void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
	}

    private void FixedUpdate()
    {
        if (beingHeld)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector3 playerObjectHeight = new Vector3(0, 1.0f, 0);
        Vector3 newPosition = ObjectToFollow.transform.position
            + playerObjectHeight;
        transform.position = newPosition;
    }

    public void ThrowSelf(Vector3 throwVector)
    {
        EnableMovement();
        BeingHeld = false;
        HasBeenThrown = true;
        dealsDamage = true;

        rigidbody.velocity = throwVector;
    }

    void DisableMovement()
    {
        rigidbody.useGravity = false;
        boxCollider.enabled = false;
    }

    void EnableMovement()
    {
        rigidbody.useGravity = true;
        boxCollider.enabled = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (dealsDamage)
        {
            //If we collide with something after thrown
            //We want to stop being in the "thrown" state
            if(other.gameObject != ObjectToFollow)
            {

                if (other.gameObject.tag == "Player" && other.gameObject != ObjectToFollow)
                {
                    Vector3 currentVelocity = gameObject.GetComponent<Rigidbody>().velocity;

                    PlayerHealth otherHealth = other.gameObject.GetComponent<PlayerHealth>();
                    otherHealth.TakeDamage(damageToDeal);

                    PlayerScript otherMovement = other.gameObject.GetComponent<PlayerScript>();
                    otherMovement.HandleKnockback(currentVelocity);
                }
                HasBeenThrown = false;
            }
        }
    }

}
