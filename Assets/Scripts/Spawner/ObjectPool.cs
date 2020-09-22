using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Spawner
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private int _capacity;

        private List<SpawnObject> _pool = new List<SpawnObject>();

        protected void Initialize(SpawnObject prefab)
        {
            for (int i = 0; i < _capacity; i++)
            {
                SpawnObject spawned = Instantiate(prefab, _container.transform);
                spawned.gameObject.SetActive(false);

                _pool.Add(spawned);
            }
        }

        protected bool TryGetObject(out SpawnObject result)
        {
            result = _pool.First(p => p.gameObject.activeSelf == false);

            return result != null;
        }
    }
}