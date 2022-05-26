using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace objectpooling
{
    public class SimpleObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        private ObjectPool<GameObject> _objectPool;
        
        private void Awake()
        {
            _objectPool = new ObjectPool<GameObject>(() => Instantiate(prefab),
                (obj) => obj.SetActive(true),
                (obj) => obj.SetActive(false),
                (obj) => Destroy(obj),
                false, 10, 20);
        }

        public GameObject Get()
        {
            return _objectPool.Get();
        }

        public void Release(GameObject gameObjToRelease)
        {
            _objectPool.Release(gameObjToRelease);
        }

        public IEnumerator ReleaseAfterXSeconds(GameObject scoreIndicator, WaitForSeconds lengthWait)
        {
            yield return lengthWait;
            _objectPool.Release(scoreIndicator);
        }
    }
}