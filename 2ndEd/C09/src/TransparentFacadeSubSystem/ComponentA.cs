using TransparentFacadeSubSystem.Abstractions;

namespace TransparentFacadeSubSystem;

public class ComponentA : IComponentA
{
    public string OperationA() => "Component A, Operation A";
    public string OperationB() => "Component A, Operation B";
}
