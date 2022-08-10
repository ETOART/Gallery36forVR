using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePaintTexture : MonoBehaviour
{
    [SerializeField] private SnapingScrolling changeEventComponent;
    [SerializeField] private float resolutionFactor;
    [SerializeField] private float reSizeSpeed;

    private MeshRenderer paintRenderer;
    private Transform paintTransform;
    private Vector3 startScale;
    private Vector3 oldScale;
    private Vector3 newScale;
    private bool reSizeWork = false;
    private float step;
  
    private void Awake(){
        changeEventComponent.chooseAppearance += ChangeAppearanceFromData;
        paintRenderer = GetComponent<MeshRenderer>();
        paintTransform = GetComponent<Transform>();
        startScale = paintTransform.localScale;
    }

    public void ChangeAppearanceFromData(PaintingData paintingData){
        ChangeTexture(paintingData.texture);
        ChangeSizeParameters(paintingData.texture);
    }
    private void ChangeTexture(Texture newTexture){
        paintRenderer.sharedMaterial.mainTexture = newTexture;
        
    }
    private void ChangeSizeParameters(Texture newTexture){
        oldScale = paintTransform.localScale;
        newScale = new Vector3(resolutionFactor*((float)(newTexture.width)/(float)(newTexture.height))
                            ,(resolutionFactor*startScale.y),(resolutionFactor*startScale.z));
        paintTransform.localScale = newScale;
    }
}
