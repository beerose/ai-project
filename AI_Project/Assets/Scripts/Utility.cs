using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Utility
{
}

public static class Extensions {
    public static T GetRandom<T>(this IList<T> col) {
        var i = UnityEngine.Random.Range(0, col.Count);
        return col[i];
    }   
}