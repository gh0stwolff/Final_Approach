using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Randomizer
{
    public static void Randomize<T>(T[] items)
    {
        for (int i = 0; i < items.Length - 1; i++)
        {
            int j = Utils.Random(i, items.Length);
            T temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
    }
}
