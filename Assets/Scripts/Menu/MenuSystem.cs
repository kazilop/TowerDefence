using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using System;

[RequireComponent(typeof(UIDocument))]
public class MenuSystem : MonoBehaviour
{
    [System.Serializable]
    public class MenuEntry
    {
        public string EntryName;
        public UnityEvent Callback;
    }

    [SerializeField] private List<MenuEntry> _menuEntries;
    [SerializeField] private float _transitionDuration;
    [SerializeField] private EasingMode _easing;
    [SerializeField] private float _buttonDelay;
    [SerializeField] private VisualTreeAsset _buttonTemplate;

    private VisualElement _container;
    private WaitForSeconds _pause;
    private List<TimeValue> _durationValues;
    private StyleList<EasingFunction> _easingValues;


    private void Start()
    {
        _pause = new WaitForSeconds(_buttonDelay);
        _durationValues = new List<TimeValue>() {new TimeValue(_transitionDuration, TimeUnit.Second)};
        _easingValues = new StyleList<EasingFunction>(new List<EasingFunction> { new EasingFunction(_easing) });
        StartCoroutine(CreateMenu());
    }

    private IEnumerator CreateMenu()
    {
        _container = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("menuContainer");
        
        foreach(MenuEntry entry in _menuEntries)
        {
            VisualElement newElement = _buttonTemplate.CloneTree();
            Button button = newElement.Q<Button>("menuButton");
            button.text = entry.EntryName;

            button.clicked += delegate { OnClick(entry); };

            _container.Add(newElement);
            newElement.style.transitionDuration = _durationValues;

            newElement.style.transitionTimingFunction = _easingValues;

            newElement.style.marginTop = 100;
            yield return null;
            newElement.style.marginTop = 0;

            yield return _pause;
        }
    }

    private void OnClick(MenuEntry entry)
    {
        Debug.Log("Click " + entry.EntryName.ToString());
        entry.Callback.Invoke();
    }
}
