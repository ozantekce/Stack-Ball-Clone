using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    private Rigidbody rigidbody;

    private float currentTime;

    private bool smash, invincible;


    void Awake()
    {

        rigidbody = GetComponent<Rigidbody>();

    }


    void Update()
    {

        smash = Input.GetMouseButtonDown(0) ? true : smash;

        smash = Input.GetMouseButtonUp(0) ? false : smash;


        if (invincible)
        {
            currentTime -= Time.deltaTime *0.35f;
        }
        else
        {
            if (smash)
                currentTime += Time.deltaTime * 0.8f;
            else
                currentTime -= Time.deltaTime * 0.5f;
        }

        // UI check

        if(currentTime >= 1)
        {
            currentTime = 1;
            invincible = true;
        }
        else if(currentTime <= 0)
        {
            currentTime = 0;
            invincible = false;
        }

        Debug.Log(invincible);

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
            if (invincible)
            {
                if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("plane"))
                {
                    collision.transform.parent.GetComponent<StackController>().ShatterAllParts();
                }

            }
            else
            {
                if (collision.gameObject.CompareTag("enemy"))
                {
                    collision.transform.parent.GetComponent<StackController>().ShatterAllParts();
                }

                if (collision.gameObject.CompareTag("plane"))
                {
                    Debug.Log("Game Over!!!");
                }
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
