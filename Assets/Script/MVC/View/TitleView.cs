using UnityEngine;
using Zenject;

public class TitleView : MonoBehaviour
{
    ITitleModel model;

     [Inject]
    public void Construct(ITitleModel model)
    {
        this.model = model;
    }

    public void Start()
    {
        if (model != null)
        {
            model.OnIsEnabledChanged += OnIsEnabledChanged;
            model.OnIsDirtyChanged += OnIsDirtyChanged;
        }
    }

    public void OnDestroy()
    {
        if (model != null)
        {
            model.OnIsEnabledChanged -= OnIsEnabledChanged;
            model.OnIsDirtyChanged -= OnIsDirtyChanged;
        }
    }

     void OnIsEnabledChanged(bool newIsEnabled)
     {
        gameObject.SetActive(newIsEnabled);
     }

     void OnIsDirtyChanged(bool newIsDirty)
     {
        throw new System.NotImplementedException();
     }
}