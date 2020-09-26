using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Spawner
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] protected Transform _container;
        [SerializeField] private int _capacity;

        private List<SpawnObject> _pool = new List<SpawnObject>();

        protected void Initialize(SpawnObject prefab)
        {
            for (int i = 0; i < _capacity; i++)
            {
                SpawnObject spawned = Instantiate(prefab, _container);
                spawned.gameObject.SetActive(false);

                _pool.Add(spawned);
            }
        }

        protected void Initialize(SpawnObject[] prefabs, Transform container, Quaternion rotate)
        {
            if (_capacity > prefabs.Length)
            {
                for (int i = 0, j = 0; i < _capacity; i++, j++)
                {
                    if (j == prefabs.Length)
                        j = 0;

                    SpawnObject spawned = Instantiate(prefabs[j], container.position, rotate);
                    spawned.transform.SetParent(container);
                    spawned.gameObject.SetActive(false);

                    _pool.Add(spawned);
                }
            }
            else
            {
                for (int i = 0; i < prefabs.Length; i++)
                {
                    SpawnObject spawned = Instantiate(prefabs[i], container.position, rotate);
                    spawned.transform.SetParent(container);
                    spawned.gameObject.SetActive(false);

                    _pool.Add(spawned);
                }
            }
        }

        protected bool TryGetObject(out SpawnObject result)
        {
            result = _pool.First(p => p.gameObject.activeSelf == false);

            return result != null;
        }

        protected bool TryGetRandomObject(out SpawnObject result, Transform container)
        {
            List<SpawnObject> _pool_container = _pool.Where<SpawnObject>(p => p.transform.parent == container).ToList();

            result = _pool_container[Random.Range(0, _pool_container.Count)];

            return result != null;
        }
    }
}