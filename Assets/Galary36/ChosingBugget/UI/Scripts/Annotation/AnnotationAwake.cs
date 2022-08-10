using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnotationAwake : MonoBehaviour
{
    [SerializeField] private SnapingScrolling changeEventComponent;
    [SerializeField] private ChangeGeneralText changeGeneralText;
    [SerializeField] private ChangeArtisticName changeArtisticName;
    private void Awake(){
        changeEventComponent.chooseAppearance += changeGeneralText.ChangeText;
        changeEventComponent.chooseAppearance += changeArtisticName.ChangeText;
    }
}
