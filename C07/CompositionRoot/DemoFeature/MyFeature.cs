namespace CompositionRoot.DemoFeature;

public class MyFeature
{
    private readonly IMyFeatureDependency _myFeatureDependency;

    public MyFeature(IMyFeatureDependency myFeatureDependency)
    {
        _myFeatureDependency = myFeatureDependency ?? throw new ArgumentNullException(nameof(myFeatureDependency));
    }
}

public interface IMyFeatureDependency { }
public class MyFeatureDependency : IMyFeatureDependency { }
