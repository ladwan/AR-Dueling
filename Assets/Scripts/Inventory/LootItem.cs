using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UMA.CharacterSystem;

public class LootItem : MonoBehaviour {

    public Item item;


    public void ItemGained()
    {
        Inventory.instance.Add(item);
    }

    public void NewScene()
    {

        SceneManager.LoadScene(7);

    }

    public void NewScene1()
    {
        EquipmentSave.instance.SaveEquipment(FindObjectOfType<DynamicCharacterAvatar>().GetCurrentRecipe());
        SceneManager.LoadScene(6);
    }

    public void NewScene2()
    {
        SceneManager.LoadScene(8);
    }

    public void OpenFight()
    {
        SceneManager.LoadScene(3);
    }



}
