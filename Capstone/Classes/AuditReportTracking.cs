using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class AuditReportTracking
    {
        public AuditReportTracking(DateTime actionTakenTime, string actionTaken, decimal actionCurrentAccountBalance)
        {
            this.ActionTakenTime = actionTakenTime;
            this.ActionTaken = actionTaken;
            this.ActionCurrentAccountBalance = actionCurrentAccountBalance;
        }
        
        public DateTime ActionTakenTime { get; set; } = DateTime.Now;

        public string ActionTaken { get; set; }

        public decimal ActionCurrentAccountBalance { get; set; }

        public void MoneyAddedAction(bool wasMoneyAdded, decimal moneyAdded)
        {
            if (wasMoneyAdded == true)
            {
                ActionTaken = "ADD MONEY: " + moneyAdded.ToString("C");
            }
        }
        
        public void GiveChangeAction(bool wasChangeGiven, decimal change)
        {
            if (wasChangeGiven == true)
            {
                ActionTaken = "GIVE CHANGE: " + change.ToString("C");
            }
        }

        public void ProductOrderingAction(bool areCustomersOrderingProduct, int quantity, CateringItem item )
        {
            if (areCustomersOrderingProduct == true)
            {
                ActionTaken = quantity + " " + item.Name + " " + item.Code + " " + (item.Price * quantity).ToString("C");
            }
        }

        public void CurrentAccoutBalanceLog(Transactions transaction)
        {
            ActionCurrentAccountBalance = transaction.CurrentAccountBalance;
        }
    }
}
