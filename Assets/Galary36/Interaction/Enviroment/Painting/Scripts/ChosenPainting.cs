using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenPainting : MonoBehaviour
{
    public GameObject Painting;

    public void ChnageBagguete(BaguetteData bagguete)
    {
        Painting.GetComponent<BaggueteSpawner>().ChangeBagguete(bagguete.objectPrefab);
    }
}
