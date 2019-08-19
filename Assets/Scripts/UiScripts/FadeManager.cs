using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FadeManager : MonoBehaviour {

    public static FadeManager instance;

    Image _fadePanel;
    int _sceneToLoad;
    bool _alreadyLoaded;
    Canvas _canvasREF;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(instance);

        }
    }
    #endregion


    public void Start()
    {

        _fadePanel = gameObject.GetComponentInChildren<Image>();
        _canvasREF = gameObject.transform.GetChild(0).GetComponent<Canvas>();
    }
    public void FadeOut (int SceneToLoad)
    {
        _sceneToLoad = SceneToLoad;
        _alreadyLoaded = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(FadeTo(1.0f, 1.0f));
    }



    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = _fadePanel.color.a;

        if (!_alreadyLoaded)
        {
            _canvasREF.sortingOrder = 1;

        }

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            _fadePanel.color = newColor;

            yield return null;
        }

        if (_alreadyLoaded)
        {
            _canvasREF.sortingOrder = -1;

        }

        if (!_alreadyLoaded)
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(_sceneToLoad);
            StartCoroutine(Reset());
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

    }


    IEnumerator Reset()
    {

/*
        if (!_doSwitch)
        {
            StartCoroutine(FadeTo(1.0f, 1.0f));
            _doSwitch = true;
            yield return null;
            StopCoroutine(Reset());
        }
        else */
        {

            _alreadyLoaded = true;
            StartCoroutine(FadeTo(0.0f, 1.0f));
 
            yield return null;
            //StopCoroutine(Reset());

        }

    }
}
