﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Core
{
    public class Pool
    {
        public static Pool I => instance ?? (instance = new Pool());

        private static Pool instance;

        private Dictionary<Type, Queue<MonoBehaviour>> caches = new Dictionary<Type, Queue<MonoBehaviour>>();

        public T Get<T>() where T : MonoBehaviour
        {
            if (!caches.ContainsKey(typeof(T)))
            {
                return null;
            }

            var items = caches[typeof(T)];

            if (items.Count == 0) { return null; }

            var item = caches[typeof(T)].Dequeue();

            return (T)item;
        }

        public void Put<T>(T item) where T : MonoBehaviour
        {
            if (caches.ContainsKey(typeof(T)))
            {
                caches[typeof(T)].Enqueue(item);
                return;
            }

            var itemsList = new Queue<MonoBehaviour>();
            itemsList.Enqueue(item); 

            caches.Add(typeof(T), itemsList);
        }
    }
}
