using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Customer
    {
        //Member Variables (Has A)
        public Wallet Wallet;
        public Backpack Backpack;

        //Constructor (Spawner)
        public Customer()
        {
            Wallet = new Wallet();
            Backpack = new Backpack();
        }
        //Member Methods (Can Do)

        //This method will be the main logic for a customer to retrieve coins form their wallet.
        //Takes in the selected can for price reference
        //Will need to get user input for coins they would like to add.
        //When all is said and done this method will return a list of coin objects that the customer will use a payment for their soda.
        public List<Coin> GatherCoinsFromWallet(Can selectedCan)
        {
           
            List<Coin> payCoins = new List<Coin>();
            double targetValue = selectedCan.Price;       // declared this variable      

            while (targetValue > TotalCoinValue(payCoins))
            {
                //prompt user to choose a coin with CW/CR
                Console.WriteLine("Please select a coin to enter:");
                UserInterface.CoinSelection(selectedCan, );
                string userIinput = Console.ReadLine();
                //then depending on their choice, call GetCoinFromWallet and pass in the appropriate name
                Coin coin = GetCoinFromWallet(userIinput);
                //GetCoinFromWallet will return a coin, add that coin to payCoins
                payCoins.Add(coin);
            }
            return payCoins;



        }
        //Returns a coin object from the wallet based on the name passed into it.
        //Returns null if no coin can be found
        public Coin GetCoinFromWallet(string coinName)
        {
            for (int i = 0; i < Wallet.Coins.Count; i++)
            {
                //only do this IF the coin's name == coinName
                if (Wallet.Coins[i].Name == coinName)
                {
                    Wallet.Coins.RemoveAt(i);
                    return Wallet.Coins[i];
                }

            }
            Console.WriteLine("This coin is not accepted. Please enter a different coin.");
            return null;
        }

        //GetCoinfRomWallet("Nickle")
        //Takes in a list of coin objects to add into the customers wallet.
        public void AddCoinsIntoWallet(List<Coin> coinsToAdd)
        {
            foreach (Coin coin in coinsToAdd)
            {
                Wallet.Coins.Add(coin);
            }
        }
        //Takes in a can object to add to the customers backpack.
        public void AddCanToBackpack(Can purchasedCan)
        {
            Backpack.cans.Add(purchasedCan);
        }

        private double TotalCoinValue(List<Coin> payment)   // this method was copied from the SodaMachine class!!!
        {
            //list of coins, EACH coin has a Value
            //Add up those values
            double paymentSum = 0;
            foreach (Coin coin in payment)
            {
                paymentSum += coin.Value;
            }
            return paymentSum;
        }
    }
}
