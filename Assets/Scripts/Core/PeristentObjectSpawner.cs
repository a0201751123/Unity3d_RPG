using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core
{
    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawne=false;

        private void Awake()
        {
            if (hasSpawne) return;

            SpawnPersistentObjects();

            hasSpawne = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
