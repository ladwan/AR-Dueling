
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public string type = "";
    public GameObject Model = null;
    new public string name = "New Item";
    public Sprite icon = null;
    public int BuyPrice = 0;
    public int Prowess = 0;
    public string UmaRecipe = "";
    public string Description = "";

}
