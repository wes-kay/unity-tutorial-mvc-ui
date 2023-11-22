using System;

  public interface ITitleModel
    {
        bool IsEnabled { get; set; }
        bool IsDirty { get; set; }
        Action<bool> OnIsEnabledChanged { get; set; }
        Action<bool> OnIsDirtyChanged { get; set; }
        void Save();
        void Load();
    }

public class TitleModel : ITitleModel
{
private bool isEnabled;
public bool IsEnabled
{
    get { return isEnabled; }
    set
    {
        if (isEnabled != value)
        {
            isEnabled = value;
            OnIsEnabledChanged?.Invoke(isEnabled);
        }
    }
}

private bool isDirty;
public bool IsDirty
{
    get { return isDirty; }
    set
    {
        if (isDirty != value)
        {
            isDirty = value;
            OnIsDirtyChanged?.Invoke(isDirty);
        }
    }
}

public Action<bool> OnIsEnabledChanged {get;set;}
public Action<bool> OnIsDirtyChanged{get;set;}

public void Save()
{
    if (IsDirty)
    {
        IsDirty = false;
    }
}

public void Load()
{
    throw new NotImplementedException();
}
}