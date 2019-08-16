using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UMA;
using UMA.CharacterSystem;
using UnityEditor;

public class Inventory : MonoBehaviour {

    public static Inventory instance;

    public Dropdown MainDropdown;
    public Dropdown WeaponDropdown, HeadDropdown, TorsoDropdown, LegsDropdown;

    public Text Description;

    public Image MainHandIcon, OffHandIcon, HelmetIcon, TorsoIcon, LegsIcon;

    public GameObject PlayerRef;

    public DynamicCharacterAvatar UmaRef;



    bool _doOnce;
    #region Singleton
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Ya fucked up");
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

        

    }
    #endregion



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
       
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }


    public void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);

        if (SceneManager.GetActiveScene().name == "Inventory")
        {

            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (SceneManager.GetActiveScene().name == "MainCity")
        {

            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void Start()
    {
        

        WeaponDropdown.gameObject.SetActive(true);

        HeadDropdown.gameObject.SetActive(false);
        TorsoDropdown.gameObject.SetActive(false);
        LegsDropdown.gameObject.SetActive(false);
        Description.text = "";

        MainDropdown.onValueChanged.AddListener(delegate {
            SwapDrop(MainDropdown);
        });

        WeaponDropdown.onValueChanged.AddListener(delegate {
            ChangeItem(WeaponDropdown);
        });

        HeadDropdown.onValueChanged.AddListener(delegate {
            ChangeItem(HeadDropdown);
        });

        TorsoDropdown.onValueChanged.AddListener(delegate {
            ChangeItem(TorsoDropdown);
        });

        LegsDropdown.onValueChanged.AddListener(delegate {
            ChangeItem(LegsDropdown);
        });

    }

    public List<Item> items = new List<Item>();

    public void Add (Item item)
    {
        items.Add(item);

        switch (item.type)
        {
            case "Weapon":
                WeaponDropdown.options.Add(new Dropdown.OptionData (item.name));
                break;

            case "Head":
                HeadDropdown.options.Add(new Dropdown.OptionData(item.name));
                break;

            case "Torso":
                TorsoDropdown.options.Add(new Dropdown.OptionData(item.name));
                Debug.Log(item.name);
                break;


        }



    }

    public void Remove (Item item)
    {

        switch (item.type)
        {
            case "Weapon":
                WeaponDropdown.options.Remove(new Dropdown.OptionData(item.name,item.icon));
                Description.text = "";
                break;

            case "Head":
                HeadDropdown.options.Remove(new Dropdown.OptionData(item.name, item.icon));
                Description.text = "";
                break;

            case "Torso":
                TorsoDropdown.options.Remove(new Dropdown.OptionData(item.name, item.icon));
                Description.text = "";
                break;

        }

        items.Remove(item);


    }

    public void ChangeItem(Dropdown change)
    {
       Debug.Log(change.options[change.value].text);
        Description.text = "";
        MainHandIcon.sprite = null;

        //Check items name before equipting it
        foreach (Item Ready2EquiptItem in items)
        {
            if(change.options[change.value].text == Ready2EquiptItem.name)
            {
                switch (Ready2EquiptItem.type)
                {
                    case "MainHand":
                        MainHandIcon.sprite = Ready2EquiptItem.icon;
                        Description.text = Ready2EquiptItem.Description;
                        UmaRef.SetSlot("", Ready2EquiptItem.UmaRecipe);
                        UmaRef.BuildCharacter();
                        break;

                    case "OffHand":
                        OffHandIcon.sprite = Ready2EquiptItem.icon;
                        Description.text = Ready2EquiptItem.Description;
                        UmaRef.SetSlot("", Ready2EquiptItem.UmaRecipe);
                        UmaRef.BuildCharacter();
                        break;

                    case "Helmet":
                        HelmetIcon.sprite = Ready2EquiptItem.icon;
                        Description.text = Ready2EquiptItem.Description;

                        UmaRef.SetSlot("Helmet", Ready2EquiptItem.UmaRecipe);
                        UmaRef.BuildCharacter();
                        break;

                    case "Torso":
                        UmaRef = FindObjectOfType<DynamicCharacterAvatar>();
                        //UmaRef = FindObjectOfType<PlayerScript>().UmaBoi;
                        TorsoIcon.sprite = Ready2EquiptItem.icon;
                        Description.text = Ready2EquiptItem.Description;
                        UmaRef.SetSlot("Chest", Ready2EquiptItem.UmaRecipe);
                        UmaRef.BuildCharacter();
                        
                        break;

                    case "Legs":
                        LegsIcon.sprite = Ready2EquiptItem.icon;
                        Description.text = Ready2EquiptItem.Description;
                        UmaRef.SetSlot("Legs", Ready2EquiptItem.UmaRecipe);
                        UmaRef.BuildCharacter();
                        break;
                }

            }
        }

    }


    public void SwapDrop(Dropdown change)
    {
        switch (change.options[change.value].text)
        {
            case "Weapon":


                WeaponDropdown.gameObject.SetActive(true);

                HeadDropdown.gameObject.SetActive(false);
                TorsoDropdown.gameObject.SetActive(false);
                LegsDropdown.gameObject.SetActive(false);
                Description.text = "";


                break;

            case "Head":
                HeadDropdown.gameObject.SetActive(true);

                WeaponDropdown.gameObject.SetActive(false);
                TorsoDropdown.gameObject.SetActive(false);
                LegsDropdown.gameObject.SetActive(false);
                Description.text = "";

                break;

            case "Torso":
                TorsoDropdown.gameObject.SetActive(true);

                HeadDropdown.gameObject.SetActive(false);
                WeaponDropdown.gameObject.SetActive(false);
                LegsDropdown.gameObject.SetActive(false);
                Description.text = "";

                break;

            case "Legs":
                LegsDropdown.gameObject.SetActive(true);

                HeadDropdown.gameObject.SetActive(false);
                TorsoDropdown.gameObject.SetActive(false);
                WeaponDropdown.gameObject.SetActive(false);
                Description.text = "";

                break;

        }

    }


}
