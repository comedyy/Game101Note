using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KK
{
    public Transform t1;
    public Transform t2;
    public Transform t3;
    public Transform t4;
}
public class BezierPlant : MonoBehaviour
{
    public KK k1;
    public KK k2;
    public KK k3;
    public KK k4;

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
        for(float x = 0; x < 1; x+= 0.01f)
        {
            for(float y = 0; y < 1; y += 0.01f)
            {
                var v1 = CalBezier(k1.t1.position, k1.t2.position, k1.t3.position, k1.t4.position, x);
                var v2 = CalBezier(k2.t1.position, k2.t2.position, k2.t3.position, k2.t4.position, x);
                var v3 = CalBezier(k3.t1.position, k3.t2.position, k3.t3.position, k3.t4.position, x);
                var v4 = CalBezier(k4.t1.position, k4.t2.position, k4.t3.position, k4.t4.position, x);

                var r = CalBezier(v1, v2, v3, v4, y);
                Gizmos.DrawCube(r, Vector3.one);
            }
        }
    }

    Vector3 CalBezier(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, float t)
    {
        var rt = 1 - t;
        return v1 * t*t*t + 3 * v2 * t * t * rt + 3 * v3 * rt * rt * t + v4 * rt * rt * rt;
    }
}
