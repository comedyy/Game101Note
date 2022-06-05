using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 使用重心坐标来计算ray跟三角形相交。

public class RayTriangleInetersectonBaryCentric : MonoBehaviour
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

        // b1(p1 - p0) + b2(p2 - p0) - td = o - p0
        Matrix4x4 mat = new Matrix4x4();
        mat.SetColumn(0, p2.position - p1.position);
        mat.SetColumn(1, p3.position - p1.position);
        mat.SetColumn(2, -ray.direction);
        mat.SetColumn(3, new Vector4(0, 0, 0, 1));

        var result = mat.inverse * (ray.origin - p1.position);

        var pIntersection = ray.origin + ray.direction * result.z;
        var pIntersection1 = (1 - result.x - result.y) * p1.position + result.x * p2.position + result.y * p3.position;

        var intersection = result.z > 0 && result.x > 0 && result.y > 0 && (result.x + result.y) < 1;
        Gizmos.color = intersection ? Color.green : Color.blue;
        Gizmos.DrawLine(ray.origin, pIntersection1);
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
