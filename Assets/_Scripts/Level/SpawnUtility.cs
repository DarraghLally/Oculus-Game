﻿using System.Collections.Generic;
using UnityEngine;

namespace SpawnUtils
{
    public class ListUtils //: MonoBehaviour
    {
        //add create shuffle stack here
        public static Stack<T> createShuffleStack<T>(IList<T> values)  where T : Object
        {
            Stack<T> stack = new Stack<T>();
            List<T> list = new List<T>(values);

            while(list.Count > 0)
            {
                var rIndex = UnityEngine.Random.Range(0, list.Count);
                var sp = list[rIndex];
                stack.Push(sp);
                list.RemoveAt(rIndex);
            }

            return stack;
        }
    }
}
    

