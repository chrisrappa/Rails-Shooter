﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f); // to do: allow customization
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
