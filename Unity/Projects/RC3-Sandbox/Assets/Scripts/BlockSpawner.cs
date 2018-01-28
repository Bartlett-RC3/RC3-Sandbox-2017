using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _blockPrefab;

    [SerializeField]
    private int _countX = 10;

    [SerializeField]
    private int _countY = 10;

    [SerializeField]
    private float _scaleX = 2.0f;

    [SerializeField]
    private float _scaleY = 2.0f;

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

        for (int j = 0; j < _countY; j++)
        {
            for (int i = 0; i < _countX; i++)
            {
                var block = Instantiate(_blockPrefab, transform);
                block.localPosition = new Vector3(i * _scaleX, 0, j * _scaleY);

                _blocks.Add(block.GetComponent<Rigidbody>());
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void CreateJoints()
    {
        for(int j = 0; j < _countY; j++)
        {
            for (int i = 0; i < _countX; i++)
            {
                var b0 = _blocks[ToIndex(i, j)];

                // -x
                if (i > 0)
                    Join(b0, _blocks[ToIndex(i - 1, j)]);

                // -y
                if (j > 0)
                    Join(b0, _blocks[ToIndex(i, j - 1)]);
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


    /// <summary>
    /// 
    /// </summary>
    private int ToIndex(int x, int y)
    {
        return x + y * _countX;
    }
}
