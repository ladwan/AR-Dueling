using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Shop : MonoBehaviour {

    public Dropdown T1dropdown,T2dropdown,T3dropdown;
    public Item[] AnyItem;
    public Item CurrentItem;
    public Transform SpawnPoint;
    public GameObject DisplayedItem;
    public Text Price, Prowess;
    public Button BuyButton;
    public GameObject SurePanel;
    // Use this for initialization
    void Start()
    {
        T1dropdown.onValueChanged.AddListener(delegate
        {
            DisplayItem();
        });

        T2dropdown.onValueChanged.AddListener(delegate
        {
            DisplayItemT2();
        });

        T3dropdown.onValueChanged.AddListener(delegate
        {
            DisplayItemT3();
        });

    }

    public void DisplayItem()
    {
        BuyButton.gameObject.SetActive(false);
        DisplayedItem = GameObject.FindGameObjectWithTag("DisplayItem");

        if (DisplayedItem != null)
        {
            Destroy(DisplayedItem);
            Price.text = null;
            Prowess.text = null;
        }
       var _itemArray = Resources.LoadAll("Items", typeof(Item)).Cast<Item>();
        foreach (Item item in _itemArray )
        {
            

            if (item.name == T1dropdown.options[T1dropdown.value].text)
            {
              Instantiate(item.Model, SpawnPoint).AddComponent<SmoothRotate>().transform.parent = null;
                Price.text = item.BuyPrice.ToString();
                Prowess.text = item.Prowess.ToString();
                BuyButton.gameObject.SetActive(true);
                CurrentItem = item;

            }
        }

        Debug.Log( T1dropdown.options[T1dropdown.value].text);

    }

    public void DisplayItemT2()
    {
        BuyButton.gameObject.SetActive(false);
        DisplayedItem = GameObject.FindGameObjectWithTag("DisplayItem");

        if (DisplayedItem != null)
        {
            Destroy(DisplayedItem);
            Price.text = null;
            Prowess.text = null;
        }
        var _itemArray = Resources.LoadAll("Items", typeof(Item)).Cast<Item>();
        foreach (Item item in _itemArray)
        {


            if (item.name == T2dropdown.options[T2dropdown.value].text)
            {
                Instantiate(item.Model, SpawnPoint).AddComponent<SmoothRotate>().transform.parent = null;
                Price.text = item.BuyPrice.ToString();
                Prowess.text = item.Prowess.ToString();
                BuyButton.gameObject.SetActive(true);
                CurrentItem = item;

            }
        }

        Debug.Log(T2dropdown.options[T2dropdown.value].text);

    }

    public void DisplayItemT3()
    {
        BuyButton.gameObject.SetActive(false);
        DisplayedItem = GameObject.FindGameObjectWithTag("DisplayItem");

        if (DisplayedItem != null)
        {
            Destroy(DisplayedItem);
            Price.text = null;
            Prowess.text = null;
        }
        var _itemArray = Resources.LoadAll("Items", typeof(Item)).Cast<Item>();
        foreach (Item item in _itemArray)
        {


            if (item.name == T3dropdown.options[T3dropdown.value].text)
            {
                Instantiate(item.Model, SpawnPoint).AddComponent<SmoothRotate>().transform.parent = null;
                Price.text = item.BuyPrice.ToString();
                Prowess.text = item.Prowess.ToString();
                BuyButton.gameObject.SetActive(true);
                CurrentItem = item;

            }
        }

        Debug.Log(T3dropdown.options[T3dropdown.value].text);

    }

    public void YouSure()
    {
        if(SurePanel.active == true)
        {
            SurePanel.SetActive(false);
        }
        else
        {
            SurePanel.SetActive(true);

        }
    }

    public void BuyItem()
    {
        SurePanel.SetActive(false);
        Inventory.instance.Add(CurrentItem);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
