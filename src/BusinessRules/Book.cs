using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessRules
{
    [XmlType("book")]
    public class Book
    {
        public int id { get; set; }
        public int author { get; set; }
        public int title { get; set; }
        public int genre { get; set; }
        public int price { get; set; }
        public int publish_data { get; set; }
        public int description { get; set; }
       

    }

    public class catalog
    {
        [XmlElement("Book")]
        public List<Book> Books { get; set; }
    }

    [XmlTypeAttribute(AnonymousType = true)]
    public class CustomersData
    {
        [XmlElement("CustomerRet")]
        public List<Customer> Customers { get; set; }

        public CustomersData()
        {
            Customers = new List<Customer>();
        }

        public int TotalCount { get; set; }


    }

    public class Customer
    {
        [XmlElement(ElementName = "ListID")]
        public string ListID { get; set; }

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "FullName")]
        public string FullName { get; set; }

        [XmlElement(ElementName = "Phone")]
        public string Phone { get; set; }
    }
}
