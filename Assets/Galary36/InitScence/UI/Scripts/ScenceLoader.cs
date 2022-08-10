using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenceLoader : MonoBehaviour
{
    [SerializeField] private GameObject _buttonLoad;
    [SerializeField] private GameObject _sliderLoad;
    
   public void LoadScence(int scenceIndex)
    {
        StartCoroutine(LoadScenceRoutine(scenceIndex));
    }
    private IEnumerator LoadScenceRoutine(int scenceIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenceIndex);
        _buttonLoad.SetActive(false);
        _sliderLoad.SetActive(true);
        Slider slider = _sliderLoad.GetComponent<Slider>();
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
