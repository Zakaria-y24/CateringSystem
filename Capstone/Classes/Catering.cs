using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// This class should contain all the "work" for catering
    /// </summary>
    public class Catering
    {
        private List<CateringItem> items = new List<CateringItem>();

        public Catering() : base()
        {

        }

        public void AddCateringItem(CateringItem newItem)
        {
            items.Add(newItem);
        }
        public CateringItem[] AllItems
        {
            get
            {
                return items.ToArray();
            }
        }
        public CateringItem FindItems(string code)
        {
            foreach (CateringItem item in items)
            {
                if (item.Code == code)
                {
                    return item;
                }
            }
            return null;  
        }
    }
}
