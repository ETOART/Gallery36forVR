using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnotationAnimatorEvents : MonoBehaviour
{
    [SerializeField] private GameObject annotationScreen;
    public void DeactivateScreen(){
        annotationScreen.SetActive(false);
    }
}
