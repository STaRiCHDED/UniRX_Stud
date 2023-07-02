using System;
using UniRx;
using UnityEngine;

public class RxSubjectsExample : MonoBehaviour
{
    private IDisposable _subscription;

    private void Start()
    {
        Debug.Log("<color=red><b>Subject</b></color>");
        var subject = new Subject<int>();
        //  Нету свойства: subject.Value
        SetupAndCompleteSubject(subject);
        
        Debug.Log("<color=red><b>ReplaySubject</b></color>");
        var replaySubject = new ReplaySubject<int>();
        // Нету свойства: replaySubject.Value 
        SetupAndCompleteSubject(replaySubject);
        
        Debug.Log("<color=red><b>BehaviourSubject</b></color>");
        var behaviorSubject = new BehaviorSubject<int>(-1);
        // behaviorSubject.Value 
        SetupAndCompleteSubject(behaviorSubject);
        
        Debug.Log("<color=red><b>AsyncSubject</b></color>");
        var asyncSubject = new AsyncSubject<int>();
        // Есть asyncSubject.Value, но доступно ТОЛЬКО если был вызван OnCompleted иначе
        // кидает Exception
        SetupAndCompleteSubject(asyncSubject);
    }

    private void SetupAndCompleteSubject(ISubject<int> subject)
    {
        _subscription?.Dispose();
        
        subject.OnNext(1);
        subject.OnNext(2);
        subject.OnNext(3);
        subject.OnNext(4);

        Debug.Log("<color=blue><b>Subscribe</b></color>");
        _subscription = subject.Subscribe(value => Debug.Log(value));
        
        subject.OnNext(5);
        subject.OnNext(5);
        subject.OnCompleted();
    }
}
