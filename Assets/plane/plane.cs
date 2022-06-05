using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane : MonoBehaviour
{
    public float a;
    public float b;
    public float c;
    public float d;

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
        Gizmos.color = Color.white;
        var normal = new Vector3(a, b, c).normalized;
        Gizmos.DrawLine(Vector3.one - normal * 100, Vector3.one + normal * 100);

        var point0 = new Vector3(0, 0, -d / c);
        var point1 = new Vector3(0, -d / b, 0);
        var point2 = new Vector3(-d / a, 0, 0);

        Gizmos.DrawLine(point0, point1);
        Gizmos.DrawLine(point0, point2);
        Gizmos.DrawLine(point2, point1);

        Gizmos.color = Color.red;
        var value = Vector3.Dot(normal, point0);
        Gizmos.DrawLine(Vector3.zero, value * normal);
        Debug.Log(value);
    }
}
