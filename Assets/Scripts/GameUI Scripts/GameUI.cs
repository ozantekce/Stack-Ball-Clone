using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{

    public GameObject homeUI, inGameUI;
    public GameObject allButtons;

    private bool buttons;

    [Header("PreGame")]
    public Button soundButton;
    public Sprite soundOnSprite, soundOffSprite;

    [Header("InGame")]
    public Image levelSlider;
    public Image currentLevelImage;
    public Image nextLevelImage;


    private Material playerMaterial;

    private Player player;


    void Awake()
    {

        playerMaterial = FindObjectOfType<Player>().transform.GetChild(0).GetComponent<MeshRenderer>().material;
        player = FindObjectOfType<Player>();

        levelSlider.transform.parent.GetComponent<Image>().color = playerMaterial.color + Color.gray;
        levelSlider.color = playerMaterial.color;

        currentLevelImage.color = playerMaterial.color;
        nextLevelImage.color = playerMaterial.color;

        soundButton.onClick.AddListener(()=>SoundManager.Instance.SoundOnOff());


    }

    private bool IgnoreUI()
    {

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData,raycastResultList);
        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<Ignore>() != null)
            {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }

        print(raycastResultList.Count);

        return raycastResultList.Count > 0;
    }

    void Update()
    {

        if(player.playerState == Player.PlayerState.Prepare)
        {
            if(SoundManager.Instance.Sound && soundButton.GetComponent<Image>().sprite != soundOnSprite)
                soundButton.GetComponent<Image>().sprite = soundOnSprite;
            else if(!SoundManager.Instance.Sound && soundButton.GetComponent<Image>().sprite != soundOffSprite)
                soundButton.GetComponent<Image>().sprite = soundOffSprite;
        }

        if (Input.GetMouseButtonDown(0) && !IgnoreUI() && player.playerState == Player.PlayerState.Prepare)
        {
            player.playerState = Player.PlayerState.Playing;
            homeUI.SetActive(false);
            inGameUI.SetActive(true);

        }

    }


    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }


    public void Settings()
    {
        buttons = !buttons;
        allButtons.SetActive(buttons);
    }


}
