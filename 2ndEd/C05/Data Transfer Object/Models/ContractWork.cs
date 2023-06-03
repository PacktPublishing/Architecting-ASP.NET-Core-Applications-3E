namespace DTOs.Models;

public record class ContractWork(int Total, int Done)
{
    public WorkState State =>
        Done == 0 ? WorkState.New :
        Done == Total ? WorkState.Completed :
        WorkState.InProgress;
}
