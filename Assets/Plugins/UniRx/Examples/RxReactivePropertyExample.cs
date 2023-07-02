using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class RxReactivePropertyExample : MonoBehaviour
{
    [SerializeField]
    private IntReactiveProperty _health;

    [SerializeField]
    private Button _button;
    private BehaviorSubject<bool> _value = new BehaviorSubject<bool>(false);

    private void Start()
    {
        _health.Subscribe(value => Debug.Log($"<color=red>ReactiveProperty</color>: {value}"));
    }
}