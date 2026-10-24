
using DataAccessLayer;
using EntitiesLayer;

namespace BusinessLogicLayer
{
    public class TossedCoinManager: ITossedCoinManager
    {
        private readonly ITossedCoinStore _Store;

        public TossedCoinManager(ITossedCoinStore store)
        {
            _Store = store;
        }


        List<TossedCoin> ITossedCoinManager.GetAllTossedCoins()
        {
            return _Store.GetAllTossedCoins();
        }

        string ITossedCoinManager.TossCoin(string playerName)
        {
            TossedCoin coin = new();
            //coin.Value = GenerateValue();
            //new TossedCoin;
            _Store.SaveTossedCoin(coin);

            return coin.Value;
        }
    }
}
