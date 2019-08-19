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
        PlayerPrefs.SetString("BaseRecipe", _baseRecipe);
        Debug.Log("Equipment Has Been Saved !");
    }


    public void LoadEquipment(DynamicCharacterAvatar _refUma)
    {
        //_refUma.GetColor("");
       // _refUma.SetColor("", C);
        _refUma.LoadFromRecipeString(PlayerPrefs.GetString("BaseRecipe"));
        Debug.Log("Equipment Has Been Loaded!");
    }
}
