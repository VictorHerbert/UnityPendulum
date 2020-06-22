using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollow : MonoBehaviour
{
    public Transform sphere;
    LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        line.SetPosition(0,gameObject.transform.position);
        line.SetPosition(1,sphere.position);      
    }
}
