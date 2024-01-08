using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScape : MonoBehaviour
{
    // Start is called before the first frame update



    [SerializeField]
    private GameObject _blockPrefab;

    [SerializeField]
    private Vector3 _size;

    [SerializeField]
    private float _roughness;

    [SerializeField]
    private Vector2 _scapeSize;

    [SerializeField]
    private List<GameObject> _objects;

    [SerializeField]
    private float _zdist;

    private Vector3 _pos, _cameraPos;
    void Start()
    {
        _cameraPos = Camera.main.transform.position;
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Generate();
        }
    }

    private void Generate()
    {
        if(_objects.Count != 0)
        {
            foreach(GameObject obj in _objects) 
            {
                Destroy(obj);
            }
            _objects = new();
        }

        //calculate the middle position of grid based on the size of the grid and size of the individual blocks, and set it in the middle of the camera.
        _pos = new(_cameraPos.x - (_scapeSize.x * _size.x / 2), _cameraPos.y - (_scapeSize.y * _size.y / 2), _zdist);

        //calculate the offset of the individual block size to match the absolute middle position of the grid
        _pos = new(_pos.x + (_size.x / 2), _pos.y + (_size.y / 2), _zdist);


        for(int i = 0; i < _scapeSize.x; i++) 
        {
            for(int j = 0; j < _scapeSize.y; j++)
            {
                Vector3 pos = new(_pos.x + _size.x * i, _pos.y + _size.y * j, _zdist + Random.Range(-_roughness, _roughness));
                GameObject block = Instantiate(_blockPrefab, pos, Quaternion.identity);
                block.transform.localScale = _size;
                _objects.Add(block);
            }
        }
    }
}
