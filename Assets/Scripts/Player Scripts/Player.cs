using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    private Rigidbody rigidbody;

    private bool smash;


    void Awake()
    {

        rigidbody = GetComponent<Rigidbody>();

    }


    void Update()
    {

        smash = Input.GetMouseButtonDown(0) ? true : smash;

        smash = Input.GetMouseButtonUp(0) ? false : smash;



    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            smash = true;
            rigidbody.velocity = new Vector3(0, -100 * Time.deltaTime * 7, 0);

        }

        if (rigidbody.velocity.y > 5)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 5, rigidbody.velocity.z);
        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!smash)
        {
            rigidbody.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);

        }
        else
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                Destroy(collision.transform.parent.gameObject);
            }

            if (collision.gameObject.CompareTag("plane"))
            {
                Debug.Log("Game Over!!!");
            }

        }


    }

    private void OnCollisionStay(Collision collision)
    {
        if (!smash)
        {
            rigidbody.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);

        }
    }


}
