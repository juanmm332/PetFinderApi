
using EntitiesLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DataAccessLayer
{
    public interface ITossedCoinStore
    {
        List<TossedCoin> GetAllTossedCoins();
        void SaveTossedCoin(TossedCoin coin);
    }

    public class TossedCoinStore : ITossedCoinStore
    {
        

        public TossedCoinStore()
        {
            InMemory = new List<TossedCoin>();
        }

        public required List<TossedCoin> InMemory { get; set; }

        public void SaveTossedCoin(TossedCoin coin)
        {
            InMemory.Add(coin);
        }
        public List<TossedCoin> GetAllTossedCoins()
        {
            return InMemory;
        }


    }
}


