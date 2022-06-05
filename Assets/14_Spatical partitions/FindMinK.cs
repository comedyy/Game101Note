using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMinK : MonoBehaviour
{
    public int totalCount = 30;
    float[] numbers;

    // Start is called before the first frame update
    void Start()
    {
        numbers = new float[totalCount];
        for(int i = 0; i < totalCount; i++)
        {
            numbers[i] = i + 1;
        }

        for(int i = 0; i < totalCount; i++)
        {
            int index = UnityEngine.Random.Range(0, totalCount);
            Swap(numbers, i, index);
        }

        for(int i = 1; i <= totalCount; i++)
        {
            var numberNew = (float[])numbers.Clone();
            var value = CalMinK(numberNew, 0, numberNew.Length - 1, i);
            if(value != totalCount - i + 1)
            {
                Debug.LogError("errror " + i);
            }
            Debug.Log(i + " " + value);
        }
    }

    public static float CalMinK(float[] numbers, int begin, int end, int k)
    {
        if(numbers == null || numbers.Length == 0 || k <= 0 || k > numbers.Length)
        {
            throw new Exception("Invalid input");
        }

        var middleIndex = (begin + end) / 2;
        var middleP = numbers[middleIndex];
        Swap(numbers, end, middleIndex);

        var i = begin;
        var j = end;
        
        while(i < j)
        {
            while(i < j && numbers[i] < middleP) i++;
            while(i < j && numbers[j] >= middleP) j--;
            if(numbers[i] > numbers[j])
            {
                Swap(numbers, i, j);
            }
        }
        
        Swap(numbers, j, end);

        var highRangeCount = end - j + 1;
        if(highRangeCount == k)
        {
            return middleP;
        }
        else if(highRangeCount > k)
        {
            return CalMinK(numbers, i + 1, end, k);
        }
        else
        {
            return CalMinK(numbers, begin, i - 1, k - highRangeCount);
        }
    }

    static void Swap(float[] members, int i , int j)
    {
        var temp = members[j];
        members[j] = members[i];
        members[i] = temp;
    }
}
