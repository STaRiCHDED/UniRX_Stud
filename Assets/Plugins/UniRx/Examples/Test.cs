using UniRx;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        // IObservable
        // IObesrver

        ReplaySubject<int> subject = new ReplaySubject<int>();
        
        subject.OnNext(5);
        subject.OnNext(7);
        subject.OnNext(10);
        
        subject.Subscribe(value => Debug.Log(value));

        subject.OnNext(20);
        subject.OnNext(30);
    }
    
}