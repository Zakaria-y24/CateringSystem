using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// This class provides all user communications, but not much else.
    /// All the "work" of the application should be done elsewhere
    /// </summary>
    public class UserInterface
    {
        private Catering catering = new Catering();
        private FileAccess fileAccess = new FileAccess();
        private Transactions transactions = new Transactions();
        private AuditReport auditReport = new AuditReport();

        public void RunMainMenu()
        {
            fileAccess.LoadCateringItems(catering);
            Console.WriteLine("Welcome to Weyland Corporation Catering Service");
            bool done = false;

            while (!done)
            {
                Console.WriteLine("(1) Display Catering Items");
                Console.WriteLine("(2) Order ");
                Console.WriteLine("(3) Quit");
                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1": // Display Catering Items List
                        DisplayItems();
                        break;
                    case "2": // Submenu
                        OrderMenu();
                        break;
                    case "3": // Quit
                        done = true;
                        Console.WriteLine("Thank you for ordering from us!");
                        break;
                    default:
                        Console.WriteLine("Please choose a valid option");
                        break;
                }
            }
        }

        public void DisplayItems()
        {
            foreach (CateringItem item in catering.AllItems)
            {
                Console.WriteLine("{0, 0}  {1,-20}  {2,5}  {3,8}",item.Code,item.Name,item.Price.ToString("C"), item.IsSoldOut());
            }
            Console.WriteLine();
        }

        public List<CateringItem> purchasedItems = new List<CateringItem>();
        int inputQuantity = 0;
        Dictionary<CateringItem, int> quantities = new Dictionary<CateringItem, int>();
        
        public void OrderMenu()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("(1) Add Money");
                Console.WriteLine("(2) Select Products");
                Console.WriteLine("(3) Complete Transaction");
                Console.WriteLine();
                Console.WriteLine("Current Account Balance: " + transactions.CurrentAccountBalance.ToString("C"));
                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1": // Add Money
                        DisplayNewAccountBalance();
                        break;
                    case "2": // Select Products
                        SelectProduct();
                        break;
                    case "3": // Complete Transaction
                        CompleteTransactions();
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Please choose a valid option.");
                        break;
                }
            }
        }

        public void DisplayNewAccountBalance()
        {
            try
            {
                Console.WriteLine("Amount you would like to add in whole dollar amounts : ");
                int moneyInWholeNumber = Convert.ToInt32(Console.ReadLine());
                decimal moneyAdded = Convert.ToDecimal(moneyInWholeNumber);
                decimal oldAccountBalance = transactions.CurrentAccountBalance;
                transactions.AddMoney(moneyAdded);
                if (transactions.CurrentAccountBalance == oldAccountBalance)
                {
                    Console.WriteLine("Maximum Account Balance cannot exceed $4,200.00.");
                }

                AuditReportTracking moneyAddedTracked = new AuditReportTracking(DateTime.Now, "", transactions.CurrentAccountBalance);
                moneyAddedTracked.MoneyAddedAction(true, Convert.ToDecimal(moneyInWholeNumber));
                auditReport.AddAction(moneyAddedTracked);
                auditReport.UploadAuditedItems(auditReport.AllAudits);
                
            }
            catch (FormatException ex)
            {
                Console.WriteLine("You did not enter a whole value number. " + ex.Message);
            }
        }

        public void SelectProduct()
        {
            Console.WriteLine("Please enter product code: ");
            string code = Console.ReadLine();
            CateringItem item = catering.FindItems(code);

            if (item == null)
            {
                Console.WriteLine("Please enter a valid code.");
                OrderMenu();
            }
            else if (item.IsSoldOut() == "SOLD OUT")
            {
                Console.WriteLine("This item is sold out. Please select different item");
                OrderMenu();
            }
            else
            {
                Console.WriteLine("Please enter quantity: ");
                inputQuantity = Convert.ToInt32(Console.ReadLine());

                if (item.Quantity < inputQuantity)
                {
                    Console.WriteLine($"Only {item.Quantity} left please lower your quantity.");
                    OrderMenu();
                }
                if (transactions.CurrentAccountBalance < inputQuantity * item.Price)
                {
                    Console.WriteLine("Please add more money");
                    OrderMenu();
                }
            }

            item.PurchaseProduct(inputQuantity);
            transactions.PurchasingItems(item.Price, inputQuantity);
            Console.WriteLine("You have selected " + inputQuantity + " " + item.Name + " for purchase.");
            Console.WriteLine();
            purchasedItems.Add(item);
            quantities[item] = inputQuantity;
            AuditReportTracking productSelectionTracked = new AuditReportTracking(DateTime.Now, "", transactions.CurrentAccountBalance);
            productSelectionTracked.ProductOrderingAction(true, inputQuantity, item);
            auditReport.AddAction(productSelectionTracked);
            auditReport.UploadAuditedItems(auditReport.AllAudits);

        }

        public void CompleteTransactions()
        {
            decimal grandTotal = 0.00M;
            foreach (KeyValuePair<CateringItem, int> pair in quantities)
            {
                decimal singleProductTotal = pair.Value * pair.Key.Price;
                Console.WriteLine($"{pair.Value}      {pair.Key.ItemTypeName()}       {pair.Key.Name}        {pair.Key.Price.ToString("C")}      {singleProductTotal.ToString("C")}");
                grandTotal += singleProductTotal;
            }

            decimal change = transactions.CurrentAccountBalance - grandTotal;
            Console.WriteLine();
            Console.WriteLine("Total: " + grandTotal.ToString("C"));
            Console.WriteLine();
            Console.WriteLine(transactions.CustomerChange(change));
            Console.WriteLine();
            transactions.CurrentAccountBalance = 0.00M;
            AuditReportTracking changeGivenTracked = new AuditReportTracking(DateTime.Now, "", transactions.CurrentAccountBalance);
            changeGivenTracked.GiveChangeAction(true, change);
            auditReport.AddAction(changeGivenTracked);
            auditReport.UploadAuditedItems(auditReport.AllAudits);
        }
    }
}
