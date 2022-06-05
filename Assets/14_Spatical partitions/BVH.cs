using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class SphereObj
{
    public Vector3 pos;
    public float radius;
    public float rayIntersectTime;

    public Bounds GetBounding()
    {
        return new Bounds(pos, Vector3.one * radius * 2);
    }

    internal float GetAxis(int maxAxis)
    {
        if(maxAxis == 0)
        {
            return pos.x;
        }
        else if(maxAxis == 1)
        {
            return pos.y;
        }
        else
        {
            return pos.z;
        }
    }
}

class BVHTreeNode
{
    public BVHTreeNode child1;
    public BVHTreeNode child2;
    public int level;

    public Bounds bouding;

    public List<SphereObj> objs;
    public BVHTreeNode(List<SphereObj> list, int level)
    {
        objs = list;
        this.level = level;
    }

    public void Init()
    {
        if(objs == null || objs.Count == 0)
        {
            throw new Exception("invalid param");
        }

        bouding = objs[0].GetBounding();
        for(int i = 1; i < objs.Count; i++)
        {
            bouding.Encapsulate(objs[i].GetBounding());
        }

        if(objs.Count <= 5)
        {
            return;
        }

        int maxAxis = GetMaxAxis();
        DiviceByAxis(maxAxis);
    }

    private void DiviceByAxis(int maxAxis)
    {
        float[] maxValues = GetByAxis(maxAxis);
        var k = maxValues.Length / 2;
        FindMinK.CalMinK(maxValues, 0, maxValues.Length - 1, k);
        var kValue = maxValues[k];

        (List<SphereObj> l1, List<SphereObj> l2) = GetDiviceByAxis(maxAxis, kValue);
        child1 = new BVHTreeNode(l1, level + 1);
        child2 = new BVHTreeNode(l2, level + 1);

        child1.Init();
        child2.Init();
    }

    private (List<SphereObj> l1, List<SphereObj> l2) GetDiviceByAxis(int maxAxis, float kValue)
    {
        List<SphereObj> l1 = new List<SphereObj>();
        List<SphereObj> l2 = new List<SphereObj>();

        foreach(var item in objs)
        {
            var comp = item.GetAxis(maxAxis) > kValue;
            if(comp)
            {
                l1.Add(item);
            }
            else
            {
                l2.Add(item);
            }
        }

        return (l1, l2);
    }

    private float[] GetByAxis(int maxAxis)
    {
        if(maxAxis == 0){
            return objs.Select(m=>m.pos.x).ToArray();
        }
        else if(maxAxis == 1)
        {
            return objs.Select(m=>m.pos.y).ToArray();
        }
        else
        {
            return objs.Select(m=>m.pos.z).ToArray();
        }
    }

    int GetMaxAxis(){
        var x = bouding.size.x;
        var y = bouding.size.y;
        var z = bouding.size.z;

        if(x > y && x > z)
        {
            return 0;
        }
        else if(y > x && y > z)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
}

public class BVH : MonoBehaviour
{
    public Bounds bounding = new Bounds(Vector3.zero, Vector3.one * 10);
    public int count = 20;
    public Vector2 sizeRange = new Vector2(0.5f, 3);
    List<SphereObj> objs;
    public int seed;

    public Transform rayTrans;

    BVHTreeNode root;
    
    void Start()
    {
        System.Random random = new System.Random(seed);
        objs = new List<SphereObj>();

        for(int i = 0; i < count; i++)
        {
            objs.Add(CreateOne(random));
        }

        root = new BVHTreeNode(objs, 0);
        root.Init();
    }

    private SphereObj CreateOne(System.Random random)
    {
        var x = Mathf.Lerp(bounding.min.x, bounding.max.x, (float)random.NextDouble());
        var y = Mathf.Lerp(bounding.min.y, bounding.max.y, (float)random.NextDouble());
        var z = Mathf.Lerp(bounding.min.z, bounding.max.z, (float)random.NextDouble());
        Vector3 pos = new Vector3(x, y, z);
        
        return new SphereObj(){
            pos = pos,
            radius = Mathf.Lerp(sizeRange.x, sizeRange.y, (float)random.NextDouble())
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        if(objs == null)
        {
            return;
        }

        var ray = new Ray(rayTrans.position, rayTrans.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ray.origin, 0.1f);
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000);

        CheckRay(root, ray);
        DrawBoundingBox(root);

        Gizmos.color = Color.white;
        foreach(var item in objs)
        {
            var isChecked = item.rayIntersectTime > Time.time - 0.2f;
            Gizmos.color = isChecked ? Color.green : Color.red;
            Gizmos.DrawWireSphere(item.pos, item.radius);
        }
    }

    private void CheckRay(BVHTreeNode node, Ray ray)
    {
        if(!node.bouding.IntersectRay(ray))
        {
            return;
        }

        if(node.child1 != null)
        {
            CheckRay(node.child1, ray);
            CheckRay(node.child2, ray);
        }
        else
        {
            foreach(var obj in node.objs)
            {
                if(obj.GetBounding().IntersectRay(ray))
                {
                    obj.rayIntersectTime = Time.time;
                }
            }
        }
    }

    Color[] colors = new Color[]{
        Color.blue, Color.yellow, Color.cyan, new Color(1, 0, 1, 1)
    };

    private void DrawBoundingBox(BVHTreeNode node)
    {
        Gizmos.color = colors[node.level];
        Gizmos.DrawWireCube(node.bouding.center , node.bouding.size - 0.5f * node.level * Vector3.one);

        if(node.child1 != null)
        {
            DrawBoundingBox(node.child1);
        }

        if(node.child2 != null)
        {
            DrawBoundingBox(node.child2);
        }
    }
}
