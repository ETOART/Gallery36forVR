using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBaguettePrefab : MonoBehaviour
{
    [SerializeField] private BaguetteSnapScrolling baguetteSnapScrolling;
    private void Awake(){
        
    }
    public void ChangeAppearance(BaguetteData baguetteData){
        DestroiOldPrefab(baguetteData.objectPrefab);
        
    }
    private void DestroiOldPrefab(GameObject prefab){
        if(transform.childCount!=0){
            Destroy(transform.GetChild(0).gameObject);
        }
        if(transform.childCount!=0){
            Destroy(transform.GetChild(0).gameObject);
        }
        InsNewPrefab(prefab);
    }
    private void InsNewPrefab(GameObject prefab){
        Instantiate(prefab,transform,false);
    }
}
