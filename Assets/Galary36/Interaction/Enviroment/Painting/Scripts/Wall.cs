using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private List<GameObject> paintings;
    void Start()
    {
        StartCoroutine(CreatePaintingsRoutine());
    }

    IEnumerator CreatePaintingsRoutine()
    {

        foreach (GameObject painting in paintings)
        {
            Debug.Log(painting);
            painting.SetActive(true);
            yield return new WaitForSeconds(1);
        }

    }
}
