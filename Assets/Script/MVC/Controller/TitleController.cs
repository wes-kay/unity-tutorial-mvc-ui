
using Zenject;

public interface ITitleController
{
void Initialize();
void SetVisable(bool v);
void Tick();
void Dispose();
}

public class TitleController : ITitleController
{
    ITitleModel model;

    [Inject]
    public void Construct(ITitleModel model) => this.model = model;

    public void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public void SetVisable(bool v) => model.IsEnabled = v;

    public void Tick()
    {
         throw new System.NotImplementedException();
    }

    public void Dispose()
    {
         throw new System.NotImplementedException();
    }
}