using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ItemManager : MonoBehaviour
{
    [Header("Items Array")] 
    [SerializeField]
    private ItemController[] _items;
    [Header("Dependencies")] 
    [SerializeField] 
    private RectTransform _inventoryRoot;
    [field: SerializeField] 
    private Button Button{ get; set;}
    
    
    private ReactiveDictionary<ItemsNames, ItemController> _itemsDictionary;
    private Random _randomizer;
    private CompositeDisposable _subscriptions;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _subscriptions = new CompositeDisposable();
        _itemsDictionary = new ReactiveDictionary<ItemsNames, ItemController>();
        _randomizer = new Random();
        Button.OnClickAsObservable().Subscribe(_ => AddRandomItem()).AddTo(_subscriptions);
    }

    // Update is called once per frame
    private void AddRandomItem()
    {
        var number = _randomizer.Next(0, _items.Length);
        if (_itemsDictionary.ContainsKey(_items[number].Name))
        {
            _itemsDictionary[_items[number].Name].ChangeCount(1);
        }
        else
        {
            var item = Instantiate(_items[number], _inventoryRoot);
            item.Initialize(DestroyDictionaryObject);
            _itemsDictionary.Add(item.Name,item);
            _itemsDictionary[_items[number].Name].ChangeCount(1);
        }
        Debug.Log($"{_items[number].Name} was added");
    }

    private void DestroyDictionaryObject(ItemController itemController)
    {
        _itemsDictionary.Remove(itemController.Name);
        Debug.Log($"{itemController.Name} was deleted");
    }
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}