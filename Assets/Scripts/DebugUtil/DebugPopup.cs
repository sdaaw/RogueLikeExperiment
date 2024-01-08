using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugPopup : MonoBehaviour
{

    public float lifetime;

    private float _timer;

    public TMP_Text textObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _timer += 1 * Time.deltaTime;
        if(_timer > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
