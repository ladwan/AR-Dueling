using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA.CharacterSystem;
using UnityEngine.SceneManagement;

public class CallSave : MonoBehaviour {

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "ARfight")
        {
            if (GameObject.FindGameObjectWithTag("Host") != null)
            {
                GameObject _RefToPlayer = GameObject.FindGameObjectWithTag("Host");
                EquipmentSave.instance.LoadEquipment(_RefToPlayer.GetComponent<DynamicCharacterAvatar>());

            }
        }
        else if (FindObjectOfType<DynamicCharacterAvatar>() != null)
        {
            EquipmentSave.instance.LoadEquipment(FindObjectOfType<DynamicCharacterAvatar>());

        }
    }
}
