using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions12
{
    private static System.Random rng = new System.Random();
    public static void Shuffle1<T>(this IList<T> list)
    {
        int n = list.Count;
        while(n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
