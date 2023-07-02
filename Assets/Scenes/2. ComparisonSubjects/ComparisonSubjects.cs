using System;
using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes._2._ComparisonSubjects
{
    public class ComparisonSubjects : MonoBehaviour
    {
        [SerializeField] private Button _subjectButton;
        [SerializeField] private Button _replaySubjectButton;
        [SerializeField] private Button _behaviorSubjectButton;
        [SerializeField] private Button _asyncSubjectButton;
        [SerializeField] private Button _subscribeButton;
        [SerializeField] private Button _completeStreamButton;

        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TextMeshProUGUI _outputText;

        private ISubject<int> _currentSubject;
        private IDisposable _subscription;


        private void Start()
        {
            Subscribe();
        }

        [UsedImplicitly]
        public void OnInputFieldChanged()
        {
            if (int.TryParse(_inputField.text, out var value))
            {
                _currentSubject?.OnNext(value);
                StartCoroutine(ClearInputField());
            }
        }

        private void Subscribe()
        {
            _subjectButton.onClick.AddListener(() =>
            {
                ClearOutputText();
                var subject = new Subject<int>();
                _currentSubject = subject;
            });

            _replaySubjectButton.onClick.AddListener(() =>
            {
                ClearOutputText();
                var replaySubject = new ReplaySubject<int>();
                _currentSubject = replaySubject;
            });

            _behaviorSubjectButton.onClick.AddListener(() =>
            {
                ClearOutputText();
                var behaviorSubject = new BehaviorSubject<int>(-1);
                _currentSubject = behaviorSubject;
            });

            _asyncSubjectButton.onClick.AddListener(() =>
            {
                ClearOutputText();
                var asyncSubject = new AsyncSubject<int>();
                _currentSubject = asyncSubject;
            });


            _subscribeButton.onClick.AddListener(() =>
            {
                if (_currentSubject != null)
                {
                    _subscription?.Dispose();

                    _subscription = _currentSubject
                        .Subscribe(value => _outputText.text += $"{value}\n")
                        .AddTo(this);
                }
            });

            _completeStreamButton.onClick.AddListener(() => _currentSubject.OnCompleted());
        }

        private IEnumerator ClearInputField()
        {
            yield return new WaitForSeconds(0.25f);
            _inputField.text = string.Empty;
        }
        
        private void ClearOutputText()
        {
            _outputText.text = string.Empty;
        }

        private void Unsubscribe()
        {
            _subscription?.Dispose();
            
            _subjectButton.onClick.RemoveAllListeners();
            _replaySubjectButton.onClick.RemoveAllListeners();
            _behaviorSubjectButton.onClick.RemoveAllListeners();
            _asyncSubjectButton.onClick.RemoveAllListeners();
            _subscribeButton.onClick.RemoveAllListeners();
            _completeStreamButton.onClick.RemoveAllListeners();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}