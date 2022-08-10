using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaggueteFrame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BaggueteSpawner>().SetFrame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
