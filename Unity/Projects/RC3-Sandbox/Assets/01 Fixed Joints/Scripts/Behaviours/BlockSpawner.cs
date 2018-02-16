using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _blockPrefab;
        [SerializeField] private float _scaleX = 2.0f;
        [SerializeField] private float _scaleY = 2.0f;
        [SerializeField] private int _countX = 10;
        [SerializeField] private int _countY = 10;

        private List<Rigidbody> _blocks;


        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            CreateBlocks();
            CreateJoints();
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateBlocks()
        {
            _blocks = new List<Rigidbody>();

            for (int y = 0; y < _countY; y++)
            {
                for (int x = 0; x < _countX; x++)
                {
                    var block = Instantiate(_blockPrefab, transform);
                    block.localPosition = new Vector3(x * _scaleX, 0, y * _scaleY);

                    _blocks.Add(block.GetComponent<Rigidbody>());
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateJoints()
        {
            int index = 0;

            for (int y = 0; y < _countY; y++)
            {
                for (int x = 0; x < _countX; x++)
                {
                    var block = _blocks[index++];

                    // -x
                    if (x > 0)
                        Join(block, _blocks[index - 1]);

                    // -y
                    if (y > 0)
                        Join(block, _blocks[index - _countX]);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void Join(Rigidbody body0, Rigidbody body1)
        {
            var joint = body0.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = body1;
        }
    }
}