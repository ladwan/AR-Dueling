using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlinkingText : MonoBehaviour {

    Text _thisText;
    bool doSwitch;
	// Use this for initialization
	void Start () {
        _thisText = gameObject.GetComponent<Text>();
        StartCoroutine(FadeTo(0.0f, 1.0f));
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.anyKeyDown)
            {
                FadeManager.instance.FadeOut(1);
            }
        }
	}

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = _thisText.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            _thisText.color = newColor;


           

            yield return null;


        }

        yield return new WaitForSeconds(1);
        StartCoroutine(Reset());
    }


    IEnumerator Reset()
    {
       

        if (!doSwitch)
        {
            StartCoroutine(FadeTo(1.0f, 1.0f));
            doSwitch = true;
            yield return null;
            StopCoroutine(Reset());
        }
        else
        {


            StartCoroutine(FadeTo(0.0f, 1.0f));
            doSwitch = false;
            yield return null;
            StopCoroutine(Reset());

        }
        
    }
}

