using EntitiesLayer;

namespace BusinessLogicLayer
{
    public interface ITossedCoinManager
    {
        List<TossedCoin> GetAllTossedCoins();
        string TossCoin(string playerName);

    }
}