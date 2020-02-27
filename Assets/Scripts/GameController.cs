using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool AllowGameControlls = true;

    public static  GameController gameController { get; private set; }
    public static Menu menu;
    private static GameObject UIOverlay;
    private static GameObject ReloadSymbol;
    private static GameObject AmmoCounts;

    public GameObject HealthDropPrefab;

    public float timeStopSpeed = 0.01f;

    //TODO: Fix this for Serializing Characters - GameController should load a player character form a file
    //for now done through the inspector.
    public  GameObject PC;

    public static int Kills;
    public static int Hits;
    public static int Misses;
    public static float StartTime;
    public static float EndTime;


    // Start is called before the first frame update
    void Start()
    {
        gameController = this;
        // TEMPORARY!!!
        UIOverlay = GameObject.Find("UserInterface/Canvas/GameOverlay");
        ReloadSymbol = UIOverlay.transform.Find("AmmoDisplay/Reload").gameObject;
        ReloadSymbol.SetActive(false);
        AmmoCounts = UIOverlay.transform.Find("AmmoDisplay/AmmoCounts").gameObject;
        SetMaxHealth();
        RefreshCharacterHealthDisplay();
        SetMaxAmmo();
        RefreshCharacterAmmoDisplay();


        StartTime = Time.time;
        Kills = Hits = Misses = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
            
    }

    private void SetMaxHealth()
    {
        ((TextMeshProUGUI)UIOverlay.transform.Find("HealthDisplay/MaxHealth").GetComponent<TextMeshProUGUI>()).SetText("/ "+PC.GetComponent<Character>().MaxHP.ToString());
    }
    public void RefreshCharacterHealthDisplay()
    {
        ((TextMeshProUGUI)UIOverlay.transform.Find("HealthDisplay/CurrentHealth").GetComponent<TextMeshProUGUI>()).SetText(PC.GetComponent<Character>().HP.ToString());
    }
    private void SetMaxAmmo()
    {
        ((TextMeshProUGUI)UIOverlay.transform.Find("AmmoDisplay/AmmoCounts/MaxAmmo").GetComponent<TextMeshProUGUI>()).SetText("/ " + PC.GetComponent<Weapon>().MaxAmmo.ToString());
    }
    public void RefreshCharacterAmmoDisplay()
    {
        ((TextMeshProUGUI)UIOverlay.transform.Find("AmmoDisplay/AmmoCounts/CurrentAmmo").GetComponent<TextMeshProUGUI>()).SetText(PC.GetComponent<Weapon>().Ammo.ToString());
    }
    public void SwitchAmmoDisplayState(bool reloading)
    {
        AmmoCounts.SetActive(!reloading);
        ReloadSymbol.SetActive(reloading);
    }
    public void SpawnCollectibles(Vector3 location)
    {
        float f = Random.Range(0f, 1f);
        if (f < 0.1f)
        {
            GameObject drop = Instantiate(HealthDropPrefab, location, new Quaternion());
            drop.name = "HealthDrop";
        }
    }
}
