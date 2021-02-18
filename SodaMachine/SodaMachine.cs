using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachine
    {
        //Member Variables (Has A)
        private List<Coin> _register;
        private List<Can> _inventory;



        //Constructor (Spawner)
        public SodaMachine()
        {
            _register = new List<Coin>();
            _inventory = new List<Can>();
            FillInventory();
            FillRegister();
        }

        //Member Methods (Can Do)

        //A method to fill the sodamachines register with coin objects.
        public void FillRegister()
        {
            for (int i = 0; i <= 20; i++)
            {
                Quarter quarter = new Quarter(); // self note: here we are INSTANTIATING a NEW coin(QUARTER) each time !!!
                _register.Add(quarter);
            }

            for (int i = 0; i <= 10; i++)
            {
                Dime dime = new Dime();
                _register.Add(dime);
            }

            for (int i = 0; i <= 20; i++)
            {
                Nickel nickel = new Nickel();
                _register.Add(nickel);
            }

            for (int i = 0; i <= 50; i++)
            {
                Penny penny = new Penny();
                _register.Add(penny);
            }

        }
        //A method to fill the sodamachines inventory with soda can objects.
        public void FillInventory()
        {      // self note: what are we filling it with? sodas - orange soda, rootbeer, cola
            for (int i = 0; i <= 10; i++)
            {
                OrangeSoda orangeSoda = new OrangeSoda();
                _inventory.Add(orangeSoda);
            }

            for (int i = 0; i <= 10; i++)
            {
                RootBeer rootBeer = new RootBeer();
                _inventory.Add(rootBeer);
            }

            for (int i = 0; i <= 10; i++)
            {
                Cola cola = new Cola();
                _inventory.Add(cola);
            }
        }
        //Method to be called to start a transaction.
        //Takes in a customer which can be passed freely to which ever method needs it.
        public void BeginTransaction(Customer customer)
        {
            bool willProceed = UserInterface.DisplayWelcomeInstructions(_inventory);  ///*********  WHATS GOING ON HERE???
            if (willProceed)
            {
                Transaction(customer);
            }
        }
        
        //This is the main transaction logic think of it like "runGame".  This is where the user will be prompted for the desired soda.
        //grab the desired soda from the inventory.
        //get payment from the user.
        //pass payment to the calculate transaction method to finish up the transaction based on the results.
        private void Transaction(Customer customer)
        {
            string customerCanSelection = "";   // code exaxmple proivded by charles  -- whats happenign here?
            Can canchoice = GetSodaFromInventory(customerCanSelection);
            customer.GatherCoinsFromWallet(canchoice);
           
        }
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda)//"Cola"
        {
            //find first can in the inventory whose name property matches the parameter



            return null;
        }

        //This is the main method for calculating the result of the transaction.
        //It takes in the payment from the customer, the soda object they selected, and the customer who is purchasing the soda.
        //This is the method that will determine the following:
        //If the payment is greater than the price of the soda, and if the sodamachine has enough change to return: Despense soda, and change to the customer.
        //If the payment is greater than the cost of the soda, but the machine does not have ample change: Despense payment back to the customer.
        //If the payment is exact to the cost of the soda:  Despense soda.
        //If the payment does not meet the cost of the soda: despense payment back to the customer.
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)  // !!!! this list of coins... 
                                                                                                 // so we end up creating a new list for every situation ??
                                                                                                 // meaning, theres the lsit here, GatherChange method, total coin method
        {
            double totalValue = TotalCoinValue(payment);
            double totalChange = DetermineChange(totalValue,chosenSoda.Price);
            List<Coin> ChangeList = GatherChange(totalChange);
            //if (payment != chosenSoda.Price)
            // {

            // }
        }
        //Takes in the value of the amount of change needed.
        //Attempts to gather all the required coins from the sodamachine's register to make change.
        //Returns the list of coins as change to despense.
        //If the change cannot be made, return null.
        public List<Coin> GatherChange(double changeValue) //self note: need 28 cents in change, .25 added, still 3 cents
        {
            List<Coin> coinChange = new List<Coin>();


            while (changeValue > 0)
            {
                if (changeValue > .25)
                {
                    RegisterHasCoin("quarter");
                    Coin getQuarter = GetCoinFromRegister("quarter");
                    coinChange.Add(getQuarter);
                    changeValue -= .25;
                    
                }
                if (changeValue > .10)
                {
                    RegisterHasCoin("dime");
                    Coin getDime = GetCoinFromRegister("dime");
                    coinChange.Add(getDime);
                    changeValue -= .10;
                    
                }
            }


            // if statement
            // coins are going to come from _register
            return null;
        }
        //Reusable method to check if the register has a coin of that name.
        //If it does have one, return true.  Else, false.
        public bool RegisterHasCoin(string name)
        {
            foreach(Coin coinName in _register)
            {
                if (name == coinName.Name)
                {
                    return true;
                }
            }
            return false;
        }
        //Reusable method to return a coin from the register.
        //Returns null if no coin can be found of that name.
        public Coin GetCoinFromRegister(string name) //self note: return/remove? coin from register
        {
            foreach(Coin coin in _register) // cant really remove from within a FOREACH .. use FOR LOOP 
            {
                if (name == coin.Name)
                {
                    //_register.Remove(coin);
                    return coin;
                }
            }
            return null;
        }
        //Takes in the total payment amount and the price of can to return the change amount.
        public double DetermineChange(double totalPayment, double canPrice)
        {
            double changeLeftOver = totalPayment - canPrice;
            return changeLeftOver;

        }
        //Takes in a list of coins to returnt the total value of the coins as a double.
        private double TotalCoinValue(List<Coin> payment)   // -- self note: this method is complete??
        {
            //list of coins, EACH coin has a Value
            //Add up those values
            double paymentSum = 0;
            foreach (Coin coin in payment)
            {
                paymentSum += coin.Value;
            }
            return paymentSum;
           //return the variable that you added the Values into
        }
        //Puts a list of coins into the soda machines register.
        private void DepositCoinsIntoRegister(List<Coin> coins)  //self note: coins need to be ADDED to REGISTER
        {
            foreach(Coin coin in coins)
            {
                _register.Add(coin);
            }
        }
    }
}
