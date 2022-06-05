using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleLerp : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    public Transform p3;

    public Color c1;
    public Color c2;
    public Color c3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    void OnDrawGizmos()
    {
        Vector3 v1 = p1.transform.position;
        Vector3 v2 = p2.transform.position;
        Vector3 v3 = p3.transform.position;

        for(float i = 0; i < 1f; i+= 0.01f)
        {
            for(float j = 0; j < 1f - i; j += 0.01f)
            {
                float k = 1 - i - j;
                var p = v1 * i + v2 * j + v3 * k;

                Gizmos.color = c1 * i + c2 * j + c3 * k;
                Gizmos.DrawCube(p, Vector3.one * 0.9f);
            }
        }
    }
}
