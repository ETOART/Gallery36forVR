using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BagertteButtonController : MonoBehaviour
{
    [SerializeField] private GameObject artitChoosScreen;
    [SerializeField] private GameObject paintScreen;
    [SerializeField] private GameObject annotationScreen;

    [SerializeField] private Animation annotatiomAnimation;
    
    public void LoadStartScene(){
       StartCoroutine(LoadYourAsyncScene("InitialScence"));
    }
    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public void LoadArtitChoosScreen(){
       artitChoosScreen.SetActive(true);
       paintScreen.SetActive(false);
   }
   public void LoadPaintScreen(){
       artitChoosScreen.SetActive(false);
       paintScreen.SetActive(true);
   }
   public void LoadWeb(){
       Application.OpenURL("https://www.gallery36.pl/");
   }
   public void OpenAnnotation(){
       annotationScreen.SetActive(true);
       //paintScreen.transform.GetChild(0).gameObject.SetActive(false);
       annotatiomAnimation.Play("OpenAnnotation");
   }
   public void CloseAnnotation(){
       annotatiomAnimation.Play("CloseAnnotation");
   }
}
