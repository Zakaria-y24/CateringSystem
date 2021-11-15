using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
    public class TransactionTests
    {
        [TestMethod]
        public void DoesAddMoneyMethodAddCorrectAmountOfMoney()
        {
            // Arrange
            Transactions transactions = new Transactions();

            // Act
            transactions.AddMoney(500M);

            //Assert
            Assert.AreEqual(transactions.CurrentAccountBalance, 500M);
        }

        [TestMethod]
        public void DoesAddMoneyNotExceed4200()
        {
            // Arrange
            Transactions transactions = new Transactions();

            //Act
            decimal oldAccountBalance = transactions.CurrentAccountBalance;
            transactions.AddMoney(4201M);

            //Assert
            Assert.AreEqual(transactions.CurrentAccountBalance, oldAccountBalance);
        }
    }
}
