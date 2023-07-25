namespace TemplateMethod;

public class LinearSearchMachine : SearchMachine
{
    public LinearSearchMachine(params int[] values) : base(values) { }

    protected override int? Find(int value)
    {
        for (var i = 0; i < Values.Length; i++)
        {
            if (Values[i] == value) { return i; }
        }
        return null;
    }
}
