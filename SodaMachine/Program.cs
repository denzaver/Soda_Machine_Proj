using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            //Simulation simulation = new Simulation();
            //simulation.Simulate();

            SodaMachine sodaMachine = new SodaMachine();
            sodaMachine.FillRegister();


            List<Coin> testList = new List<Coin> { new Quarter(), new Quarter() };


            double testDouble = sodaMachine.TotalCoinValue(testList);            //testDouble is .5

            List<Coin> testList2 = new List<Coin> { new Penny(), new Dime() };

            double Tesdouble2 = sodaMachine.TotalCoinValue(testList2);
            //testDouble should be .11
            Console.WriteLine(Tesdouble2);
            Console.ReadLine();
        }
    }
}
