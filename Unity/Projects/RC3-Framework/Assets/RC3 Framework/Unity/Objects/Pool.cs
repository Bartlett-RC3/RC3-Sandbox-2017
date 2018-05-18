
/*
 * Notes
 */ 
 
using System.Collections.Generic;
using UnityEngine;

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pool<T> : ScriptableObject
        where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        private Queue<T> _queue = new Queue<T>(4);


        /// <summary>
        /// Returns the number of items in the pool
        /// </summary>
        public int Count
        {
            get { return _queue.Count; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        public void Fill(int count)
        {
            for(int i = 0; i < count; i++)
                _queue.Enqueue(Instantiate(_prefab));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        public void FillTo(int count)
        {
            for (int i = _queue.Count; i < count; i++)
                _queue.Enqueue(Instantiate(_prefab));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T Take()
        {
            if (_queue.Count > 0)
            {
                var item = _queue.Dequeue();
                item.gameObject.SetActive(true);
                return item;
            }

            return Instantiate(_prefab);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void Return(T item)
        {
            item.gameObject.SetActive(false);
            _queue.Enqueue(item);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public void Return(IEnumerable<T> items)
        {
            foreach (var item in items)
                Return(item);
        }
    }
}
