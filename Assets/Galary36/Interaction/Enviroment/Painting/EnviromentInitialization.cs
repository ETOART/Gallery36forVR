using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentInitialization : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CreateEnviromentRoutine());
    }

    IEnumerator CreateEnviromentRoutine()
    {

        foreach (Transform painting in transform)
        {
            Debug.Log(painting);
            painting.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

    }
}
