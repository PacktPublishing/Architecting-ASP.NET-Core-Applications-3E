using StateR;
using StateR.Reducers;

namespace WASM.Features;

public class Counter
{
    public record State(int Count) : StateBase;

    public class InitialState : IInitialState<State>
    {
        public State Value => new(0);
    }

    public record Increment : IAction;
    public record Decrement : IAction;

    public class Reducers : IReducer<Increment, State>, IReducer<Decrement, State>
    {
        public State Reduce(Increment action, State state) => state with { Count = state.Count + 1 };
        public State Reduce(Decrement action, State state) => state with { Count = state.Count - 1 };
    }
}
