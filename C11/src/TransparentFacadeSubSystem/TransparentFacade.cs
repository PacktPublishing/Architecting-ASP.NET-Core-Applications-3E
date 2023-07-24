using System.Text;
using TransparentFacadeSubSystem.Abstractions;

namespace TransparentFacadeSubSystem;

public class TransparentFacade : ITransparentFacade
{
    private readonly IComponentA _componentA;
    private readonly IComponentB _componentB;
    private readonly IComponentC _componentC;

    public TransparentFacade(IComponentA componentA, IComponentB componentB, IComponentC componentC)
    {
        _componentA = componentA ?? throw new ArgumentNullException(nameof(componentA));
        _componentB = componentB ?? throw new ArgumentNullException(nameof(componentB));
        _componentC = componentC ?? throw new ArgumentNullException(nameof(componentC));
    }

    public string ExecuteOperationA()
    {
        return new StringBuilder()
            .AppendLine(_componentA.OperationA())
            .AppendLine(_componentA.OperationB())
            .AppendLine(_componentB.OperationD())
            .AppendLine(_componentC.OperationE())
            .ToString();
    }

    public string ExecuteOperationB()
    {
        return new StringBuilder()
            .AppendLine(_componentB.OperationC())
            .AppendLine(_componentB.OperationD())
            .AppendLine(_componentC.OperationF())
            .ToString();
    }
}
