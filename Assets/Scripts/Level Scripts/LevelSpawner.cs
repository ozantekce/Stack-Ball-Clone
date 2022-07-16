using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{

    public GameObject[] models;

    [HideInInspector]
    public GameObject[] modelPrefab = new GameObject[4];
    public GameObject winPrefab;


    private GameObject temp1, temp2;


    private int level = 1, addOn =7;
    float i = 0;




    void Start()
    {

        level = PlayerPrefs.GetInt("Level", 1);

        if(level > 9)
        {
            addOn = 0;
        }

        ModelSelection();
        float random = Random.value;
        for (i = 0; i > -level-addOn; i-=0.5f)
        {
            if(level <=20)
                temp1 = Instantiate(modelPrefab[Random.Range(0,2)]); 
            if (level > 20 && level <= 50)
                temp1 = Instantiate(modelPrefab[Random.Range(1, 3)]);
            if (level > 50 && level <= 100)
                temp1 = Instantiate(modelPrefab[(Random.Range(2, 4))]);
            if (level > 100)
                temp1 = Instantiate(modelPrefab[(Random.Range(3, 4))]);

            temp1.transform.position = new Vector3(0, i - 0.01f, 0);
            temp1.transform.eulerAngles = new Vector3(0, i*8, 0);


            if(Mathf.Abs(i) >= level * .3f && Mathf.Abs(i) <= level *.6f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);
                temp1.transform.eulerAngles += Vector3.up * 180;
            }
            else if(Mathf.Abs(i) >= level * .8f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);
                if(random > .75f)
                    temp1.transform.eulerAngles += Vector3.up * 180;
            }

            temp1.transform.parent = FindObjectOfType<Rotator>().transform;

        }

        temp2 = Instantiate(winPrefab);
        temp2.transform.position = new Vector3(0, i - 0.01f, 0);




    }


    void ModelSelection()
    {
        
        int randomModel = Random.Range(0, 5);

        switch (randomModel)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefab[i] = models[i];
                }

                break;
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefab[i] = models[i+4];
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefab[i] = models[i + 8];
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefab[i] = models[i + 12];
                }
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefab[i] = models[i + 16];
                }
                break;

        }

    }


    public void NextLevel()
    {
        level++;
        PlayerPrefs.SetInt("Level",level);
        SceneManager.LoadScene(0);
        
    }

    public void ReloadLevel()
    {
        PlayerPrefs.SetInt("Level", level);
        SceneManager.LoadScene(0);
    }


}
