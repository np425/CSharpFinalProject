namespace CSharpFinalProject.Data;

public class DataQueryApiArgs: DataQueryFilterArgs
{
    public int? Skip { get; set; }
    public int? Top { get; set; }
}
