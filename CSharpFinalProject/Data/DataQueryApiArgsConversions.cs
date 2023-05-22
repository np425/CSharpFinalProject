using Radzen;

namespace CSharpFinalProject.Data;

public static class DataQueryApiArgsConversions
{
    public static DataQueryApiArgs FromLoadDataArgs(LoadDataArgs args)
    {
        return new DataQueryApiArgs
        {
            Skip = args.Skip,
            Top = args.Top
        };
    }
}
