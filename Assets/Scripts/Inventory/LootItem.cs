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
        FadeManager.instance.FadeOut(7);
        //SceneManager.LoadScene(7);

    }

    public void NewScene1()
    {
        EquipmentSave.instance.SaveEquipment(FindObjectOfType<DynamicCharacterAvatar>().GetCurrentRecipe());
        FadeManager.instance.FadeOut(6);
    }

    public void NewScene2()
    {
        FadeManager.instance.FadeOut(8);
    }

    public void OpenFight()
    {
        SceneManager.LoadScene(3);
    }



}
