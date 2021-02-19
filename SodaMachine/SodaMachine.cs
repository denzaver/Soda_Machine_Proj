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
            bool willProceed = UserInterface.DisplayWelcomeInstructions(_inventory);  ///*********  WHATS GOING ON HERE??? - is this complete?

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
            // code exaxmple proivded by charles  

            // need input from customer of the soda that they want
            string customerCanSelection = ""; 

            // grab the desired soday from the invetory based on the customers choice
            Can canchoice = GetSodaFromInventory(customerCanSelection);

            // get the payment from the customer
            List<Coin> payment = customer.GatherCoinsFromWallet(canchoice); // accessing the method witin the customer class

            // pass payent to the CalculateTransaction method

            CalculateTransaction(payment, canchoice, customer);  

           
        }
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda)//"Cola"
        {
            //find first can in the inventory whose name property matches the parameter

            for (int i = 0; i < _inventory.Count; i++)
            {
                if(_inventory[i].Name == nameOfSoda)
                {
                    _inventory.RemoveAt(i);
                    return _inventory[i]; 
                }
            }
            return null;

            //foreach(Can soda in _inventory)
            //{
            //    if (nameOfSoda == soda.Name)
            //    {
            //        Console.WriteLine();
            //        return soda;
            //    }
            //}
            //return null;
        }

        //This is the main method for calculating the result of the transaction.
        //It takes in the payment from the customer, the soda object they selected, and the customer who is purchasing the soda.
        //This is the method that will determine the following:
        // ---------------------------------------------------------------------------------------------------------------------------------------------------------
        //If the payment is greater than the price of the soda, and if the sodamachine has enough change to return: Despense soda, and change to the customer.
        //If the payment is greater than the cost of the soda, but the machine does not have ample change: Despense payment back to the customer.
        //If the payment is exact to the cost of the soda:  Despense soda.
        //If the payment does not meet the cost of the soda: despense payment back to the customer.
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)                                                                                    
        {
            double totalValue = TotalCoinValue(payment);  // takes in the list of coins(payment) and turns them into a DOUBLE amount 
            double totalChange = DetermineChange(totalValue,chosenSoda.Price);
            List<Coin> ChangeList = GatherChange(totalChange);

            if (totalValue > chosenSoda.Price && ChangeList != null)
            {
                // Despense soda, and change to the customer.
                GetSodaFromInventory(chosenSoda.Name);
                customer.AddCanToBackpack(chosenSoda);
                customer.AddCoinsIntoWallet(ChangeList);
            }
            else if (totalValue > chosenSoda.Price && ChangeList == null)
            {
                customer.AddCoinsIntoWallet(payment);
            }
            else if (totalValue == chosenSoda.Price)
            {
                GetSodaFromInventory(chosenSoda.Name);
                customer.AddCanToBackpack(chosenSoda);
            }
            else if (totalValue < chosenSoda.Price)
            {
                customer.AddCoinsIntoWallet(payment);
            }

        }
        //Takes in the value of the amount of change needed.
        //Attempts to gather all the required coins from the sodamachine's register to make change.
        //Returns the list of coins as change to despense.
        //If the change cannot be made, return null.
        public List<Coin> GatherChange(double changeValue) //self note: 
        {
            List<Coin> coinChange = new List<Coin>();

            while (changeValue > 0)
            {
                if (changeValue >= .25 && RegisterHasCoin("Quarter"))
                {

                    Coin getQuarter = GetCoinFromRegister("Quarter");
                    coinChange.Add(getQuarter);
                    changeValue -= .25;  // performing the arithmetic
                    changeValue = Math.Round(changeValue, 2); //reassign the value to be rounded
                }

                else if (changeValue >= .10 && RegisterHasCoin("Dime"))
                {
                    Coin getDime = GetCoinFromRegister("Dime");
                    coinChange.Add(getDime);
                    changeValue -= .10;
                    changeValue = Math.Round(changeValue, 2);
                }

                else if (changeValue >= .05 && RegisterHasCoin("Nickle"))
                {
                    Coin getNickle = GetCoinFromRegister("Nickle");
                    coinChange.Add(getNickle);
                    changeValue -= 0.5;
                    changeValue = Math.Round(changeValue, 2);
                }
                else if (changeValue >= .01 && RegisterHasCoin("Penny"))
                {
                    
                    Coin getPenny = GetCoinFromRegister("Penny");
                    coinChange.Add(getPenny);
                    changeValue -= 0.1;
                    changeValue = Math.Round(changeValue, 2); // the 2 represents where want to "cut off "              
                }
                else
                {
                    Console.WriteLine("Needed change is not available!");
                    return null; // why did we add null here? -- we return null sice there is a possibility of not being able to return the 
                                 // the needed amount of change. We then NULL the operation and notify the user... 
                }
            }
            return coinChange;
            // self note: if statement
            // self note: coins are going to come from _register

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
            for (int i = 0; i < _register.Count; i++)
            {
                if (_register[i].Name == name)
                {
                    _inventory.RemoveAt(i);
                    return _register[i];
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
        //Takes in a list of coins to return the total value of the coins as a double.
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
