namespace Backend.Helpers;

public interface IDtoConversion<TInternalData, TDtoData>
{
    public TDtoData ToDto(TInternalData internalData);

    public TInternalData ToInternal(TDtoData dtoData);

    public void LoadDtoData(TInternalData internalData, TDtoData dtoData);
}
