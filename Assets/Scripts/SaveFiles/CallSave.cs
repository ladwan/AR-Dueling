using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA.CharacterSystem;

public class CallSave : MonoBehaviour {

    public void Start()
    {
        if (FindObjectOfType<DynamicCharacterAvatar>() != null)
        {
            EquipmentSave.instance.LoadEquipment(FindObjectOfType<DynamicCharacterAvatar>());

        }
    }
}
