using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Transactions
    {
        public Transactions()
        {
            
        }

        public decimal CurrentAccountBalance { get; set; } = 0.00M;

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public decimal AddMoney(decimal moneyAdded)
        {
            if (CurrentAccountBalance + moneyAdded <= 4200M)
            {
                return CurrentAccountBalance += moneyAdded;
            }
            return CurrentAccountBalance;
        }

        public decimal PurchasingItems(decimal price, int quantity)
        {
            CurrentAccountBalance -= price * quantity;
            return CurrentAccountBalance;
        }

        public string AddMoneyToAudit
        {
            get
            {
                return "ADD MONEY:";
            }
        }

        public string CustomerChange (decimal change)
        { 
            int numberOfTwenties = 0;
            int numberOfTens = 0;
            int numberOfFives = 0;
            int numberOfOnes = 0;
            int numberOfQuarters = 0;
            int numberOfDimes = 0;
            int numberOfNickles = 0;

            while (change != 0.00M)
            {
                if (change / 20.00M >= 1.00M)
                {
                    numberOfTwenties++;
                    change -= 20.00M;
                }
                else if (change / 10.00M >= 1.00M)
                {
                    numberOfTens++;
                    change -= 10.00M;
                }
                else if (change / 5.00M >= 1.00M)
                {
                    numberOfFives++;
                    change -= 5.00M;
                }
                else if (change / 1.00M >= 1.00M)
                {
                    numberOfOnes++;
                    change -= 1.00M;
                }
                else if (change / 0.25M >= 1.00M)
                {
                    numberOfQuarters++;
                    change -= 0.25M;
                }
                else if (change / 0.10M >= 1.00M)
                {
                    numberOfDimes++;
                    change -= 0.10M;
                }
                else
                {
                    numberOfNickles++;
                    change -= 0.05M;
                }
            }
            return $"Your change is {numberOfTwenties} twenties, {numberOfTens} tens, {numberOfFives} fives, {numberOfOnes} ones, {numberOfQuarters} quarter, {numberOfDimes} dimes, and {numberOfNickles} nickels.";
        }
    }
}
