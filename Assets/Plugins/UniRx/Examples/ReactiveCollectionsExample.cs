using UniRx;
using UnityEngine;

public class ReactiveCollectionsExample : MonoBehaviour
{
    public ReactiveCollection<int> _collection = new ReactiveCollection<int>();

    private void Start()
    {
        _collection
            .ObserveAdd()
            .Subscribe(data => Debug.Log("_collection[" + data.Index + "] = " + data.Value));
        
        _collection.Add(1);
        _collection.Add(2);
        _collection.Add(3);
    }
}