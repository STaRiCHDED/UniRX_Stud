using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] 
    private ItemManager _itemManager;
    
    private void Awake()
    {
        Debug.Log("GameStart");
    }
}
