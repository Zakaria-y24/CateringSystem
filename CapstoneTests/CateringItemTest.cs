using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class CateringItemTest
    {
        [TestMethod]
        public void AddCateringItemMatchesInfoOnItem()
        {
            // Arrange 
            Catering catering = new Catering();

            // Act
            CateringItem item = new CateringItem("C", "C4", "Candy", 4.50M);
            
           // Assert
            Assert.AreEqual(item.Name, "Candy");
            Assert.AreEqual(item.Type, "C");
            Assert.AreEqual(item.Code, "C4");
            Assert.AreEqual(item.Price, 4.50M);
        }

        [TestMethod]
        public void DoesSoldOutMethodShowIfItemIsSoldOut()
        {
            // Arrange 
            CateringItem item = new CateringItem("C", "C4", "Candy", 4.50M);

            // Act
            string expected = " remaining";

            // Assert
            Assert.AreEqual(item.IsSoldOut(), item.Quantity + expected);
        }

        [TestMethod]
        public void DoesItemNameAppearCorrectly()
        {
            // Arrange 
           CateringItem item = new CateringItem("B", "B4", "Beverage", 4.50M);

           // Act
            string expected = "Beverage";

           // Assert
            Assert.AreEqual(item.ItemTypeName(), expected);
        }

        [TestMethod]
        public void DoesFindItemFindItem()
        {
            // Arrange 
            Catering catering = new Catering();
            CateringItem item = new CateringItem("B", "B4", "Beverage", 4.50M);

            // Act
            catering.AddCateringItem(item);
            CateringItem expected = catering.FindItems("B4");

           // Assert
            Assert.AreEqual(item, expected);
        }
    }
}
