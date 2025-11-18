namespace Carpocalypse
{
    public interface IPoolable
    {
        void OnSpawnFromPool();
        void OnReturnToPool();
    }
}
