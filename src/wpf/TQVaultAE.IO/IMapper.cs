namespace TQVaultAE.IO
{
    internal interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }
}
