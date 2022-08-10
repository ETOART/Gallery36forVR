using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ScrollButton : MonoBehaviour
{
    public int ID{get;set;}
    private IScrol snapingScrollingComponent;
    private void Start(){
        snapingScrollingComponent = transform.parent.gameObject.GetComponent<IScrol>();
    }
    public void OnMouseDown() 
	{
		//snapingScrollingComponent.Stop;
	}
    public void OnMouseUp() 
	{
		snapingScrollingComponent.StartMove(ID);
	}
}
