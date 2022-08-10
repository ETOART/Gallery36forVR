using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MusicController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject sound;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        
        StartCoroutine(FinishFirst(1.5f));
        animator.SetBool("Active", !animator.GetBool("Active"));
    }
    IEnumerator FinishFirst(float waitTime)
    {

       
        yield return new  WaitForSeconds(waitTime);
        sound.GetComponent<AudioSource>().enabled = !sound.GetComponent<AudioSource>().enabled;
    }
}
