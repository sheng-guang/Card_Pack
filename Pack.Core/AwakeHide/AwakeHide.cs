﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeHide : MonoBehaviour
{
    private void Awake()
    {

        gameObject.SetActive(false);
        transform.SetAsFirstSibling();
    }
}
