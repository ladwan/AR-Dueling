using UnityEngine;
using System.Collections;

public class example : MonoBehaviour {

    public GameObject ColorPickedPrefab, SkinPicker;
    public Material SkinRef;
    private ColorPickerTriangle CP;
    private bool isPaint = false;
    private GameObject go;
    private Material mat;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {


        if (isPaint)
        {
            

          if (CharacterColors.isSkin == true)
            {

                mat.color = CP.TheColor;

            }
            else
            {
                mat = GetComponent<MeshRenderer>().material;
                mat.color = CP.TheColor;
            }
        }

    }

    void OnMouseDown()
    {
        if (isPaint)
        {
            StopPaint();
        }
        else
        {
            StartPaint();
        }
    }

    public void StartPaint()
    {
       if (go != null)
        {
            Destroy(go);
        }

        if (CharacterColors.isSkin == false)
        {
            Debug.Log("444");
            go = (GameObject)Instantiate(ColorPickedPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            go.transform.localScale = Vector3.one * .2f;
            go.transform.LookAt(Camera.main.transform);
            CP = go.GetComponent<ColorPickerTriangle>();

            isPaint = true;
        }
        else
        {
            Debug.Log("555");
            go = (GameObject)Instantiate(SkinPicker, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            go.transform.localScale = Vector3.one * .2f;
            go.transform.LookAt(Camera.main.transform);
            CP = go.GetComponent<ColorPickerTriangle>();
            mat.color = SkinRef.color;
            CP.TheColor = mat.color;
            CP.SetNewColor(SkinRef.color);
            isPaint = true;
        }

    }



    public void StopPaint()
    {
        Destroy(go);
        isPaint = false;
    }
}
