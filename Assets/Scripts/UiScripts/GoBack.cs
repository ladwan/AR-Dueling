using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoBack : MonoBehaviour {

    public void ToMainMenu()
    {
        Camera.main.GetComponent<CombatManager>().Gameloop();
        SceneManager.LoadScene(6);
    }
}
