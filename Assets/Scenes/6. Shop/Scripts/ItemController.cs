using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(ItemView))]
public class ItemController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] 
    private ItemView _view;
    
    [field:SerializeField] 
    public ItemsNames Name { get; private set; }
    
    private IntReactiveProperty _count = new IntReactiveProperty(0);
    private Subject<ItemController> _subject = new Subject<ItemController>();
    private CompositeDisposable _subscriptions;

    public void ChangeCount(int count)
    {
        _count.Value += count;
    }
    public void Initialize(Action<ItemController> itemController)
    {
        _view.Initialize();
        _subscriptions = new CompositeDisposable();
        _count.Subscribe(_view.ChangeCountOfItems).AddTo(_subscriptions);
        _view.Button.OnClickAsObservable().Subscribe(_ => DestroyItem()).AddTo(_subscriptions);
        _subject.Subscribe(itemController).AddTo(_subscriptions);
    }

    private void DestroyItem()
    {
        if (_count.Value > 1)
        {
            ChangeCount(-1);
            return;
        }
        ChangeCount(-1);
        _subject.OnNext(this);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}