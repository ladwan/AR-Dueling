using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA.CharacterSystem;

public class EquipmentSave : MonoBehaviour {

    public static EquipmentSave instance;
    
    #region Singleton
      private void Awake()
      {
          if (instance != null)
          {
              Debug.Log("More than 1 EquipmentSave dectected");
              return;
          }
          instance = this;
          DontDestroyOnLoad(this);
      }
      #endregion
      
    public void SaveEquipment(string _baseRecipe)
    {
       // PlayerPrefs.SetFloat("SkinR",_skin.r);
      //  PlayerPrefs.SetFloat("SkinG", _skin.g);
        //PlayerPrefs.SetFloat("SkinB", _skin.b);

        PlayerPrefs.SetString("BaseRecipe", _baseRecipe);

        Debug.Log("Loadout Has Been Saved !");
    }


    public void LoadEquipment(DynamicCharacterAvatar _refUma)
    {
       // _refUma.SetColor("Skin", new Color (PlayerPrefs.GetFloat("SkinR"), PlayerPrefs.GetFloat("SkinG"), PlayerPrefs.GetFloat("SkinB")));
        _refUma.LoadFromRecipeString(PlayerPrefs.GetString("BaseRecipe"));
        _refUma.BuildCharacter();

        Debug.Log("Equipment Has Been Loaded!");
    }
}
