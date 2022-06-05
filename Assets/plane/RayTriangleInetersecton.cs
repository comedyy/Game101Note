using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 测试ray跟三角形焦点。
// 1. 先找到跟平面的交点
// 2. 再判断焦点是否再三角形内。

public class RayTriangleInetersecton : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    public Transform p3;

    public Transform rayTrans;
    public Ray ray;

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
        ray = new Ray(rayTrans.position, rayTrans.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ray.origin, 0.1f);
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000);
        
        DrawTriangle();
        
        var normal = Vector3.Cross(p1.position - p2.position, p2.position - p3.position).normalized;
        var p = p1.position;

        var t = Vector3.Dot(p - ray.origin, normal) / Vector3.Dot(ray.direction, normal);

        var pIntersection = ray.origin + ray.direction * t;
        var intersection = CheckPointInTriangle(pIntersection, p1.position, p2.position, p3.position);
        Gizmos.color = intersection ? Color.green : Color.blue;
        Gizmos.DrawLine(ray.origin, pIntersection);
    }

    void DrawTriangle()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(p2.position, p1.position);
        Gizmos.DrawLine(p3.position, p2.position);
        Gizmos.DrawLine(p1.position, p3.position);
    }

    static bool CheckPointInTriangle(Vector3 p, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        var sign1 = Vector3.Cross(p - p1, p2 - p1);
        var sign2 = Vector3.Cross(p - p2, p3 - p2);
        var sign3 = Vector3.Cross(p - p3, p1 - p3);

        return Vector3.Dot(sign1, sign2) > 0 && Vector3.Dot(sign2, sign3) > 0;
    }
}
