using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{


    private Rigidbody rigidbody;

    private float currentTime;

    private bool smash, invincible;

    private int currentBrokenStacks, totalStacks;

    public GameObject invincibleGO;
    public Image invincibleFill;
    public GameObject fireEffect;


    public enum PlayerState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }


    [HideInInspector]
    public PlayerState playerState = PlayerState.Prepare;

    public AudioClip bounceoffClip, deadClip, winClip, destroyClip, iDestroyClip;

    void Awake()
    {

        rigidbody = GetComponent<Rigidbody>();
        currentBrokenStacks = 0;
    }


    private void Start()
    {
        totalStacks = FindObjectsOfType<StackController>().Length;

    }

    void Update()
    {
        smash = Input.GetMouseButtonDown(0) ? true : smash;

        smash = Input.GetMouseButtonUp(0) ? false : smash;

        if (playerState == PlayerState.Playing)
        {



            if (invincible)
            {
                currentTime -= Time.deltaTime * 0.35f;
                if (!fireEffect.activeInHierarchy)
                {
                    fireEffect.SetActive(true);
                }

            }
            else
            {
                if (fireEffect.activeInHierarchy)
                {
                    fireEffect.SetActive(false);
                }

                if (smash)
                    currentTime += Time.deltaTime * 0.8f;
                else
                    currentTime -= Time.deltaTime * 0.5f;
            }

            if(currentTime >= 0.15f || invincibleFill.color == Color.red)
            {
                invincibleGO.SetActive(true);
            }
            else
            {
                invincibleGO.SetActive(false);
            }

            if (currentTime >= 1)
            {
                currentTime = 1;
                invincible = true;
                invincibleFill.color = Color.red;
            }
            else if (currentTime <= 0)
            {
                currentTime = 0;
                invincible = false;
                invincibleFill.color = Color.white;
            }


            if (invincibleGO.activeInHierarchy)
            {
                invincibleFill.fillAmount = currentTime / 1.0f;
            }


            //Debug.Log(invincible);

        }




        if (playerState == PlayerState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().NextLevel();
            }
        }



    }

    void FixedUpdate()
    {
        if(playerState == PlayerState.Playing)
        {

            if (Input.GetMouseButton(0))
            {
                smash = true;
                rigidbody.velocity = new Vector3(0, -100 * Time.deltaTime * 7, 0);

            }
        }


        if (rigidbody.velocity.y > 5)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 5, rigidbody.velocity.z);
        }


    }

    public void IncreaseBrokenStacks()
    {
        currentBrokenStacks++;
        if (!invincible)
        {
            ScoreManager.Instance.AddScore(1);
            SoundManager.Instance.PlaySoundFX(destroyClip, 0.5f);
        }
        else
        {
            ScoreManager.Instance.AddScore(2);
            SoundManager.Instance.PlaySoundFX(iDestroyClip, 0.5f);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!smash)
        {
            rigidbody.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            SoundManager.Instance.PlaySoundFX(bounceoffClip, 0.5f);
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
                    ScoreManager.Instance.ResetScore();
                    SoundManager.Instance.PlaySoundFX(deadClip, 0.5f);
                }
            }

        }

        FindObjectOfType<GameUI>().LevelSliderFill(currentBrokenStacks/(float)totalStacks);

        if(collision.gameObject.CompareTag("Finish") && playerState == PlayerState.Playing)
        {
            playerState = PlayerState.Finish;
            SoundManager.Instance.PlaySoundFX(winClip, 0.7f);
        }


    }

    private void OnCollisionStay(Collision collision)
    {
        if (!smash || collision.gameObject.CompareTag("Finish"))
        {
            rigidbody.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);

        }
    }


}
