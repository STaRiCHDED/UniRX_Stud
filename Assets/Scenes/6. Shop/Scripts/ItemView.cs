using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [field: SerializeField]
    public Button Button{ get; private set;}
    [SerializeField]
    private TextMeshProUGUI _outputText;
    
    public void Initialize()
    {
        _outputText.text = 0.ToString();
    }
    public void ChangeCountOfItems(int count)
    {
        _outputText.text = count.ToString();
    }
}
