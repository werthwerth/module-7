using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;
using System.Runtime.InteropServices;

class Program
{

    abstract class Delivery
    {
        public string deliveryType;
    }

    class HomeDelivery : Delivery
    {
        public HomeDelivery(double productsMass, string address)
        {
            mass = productsMass;
            deliveryAddress = address;
            deliveryType = "Home";
        }
        public string deliveryAddress;
        public double mass;
    }

    class PickPointDelivery : Delivery
    {
        public PickPointDelivery(double productsMass, string address)
        {
            mass = productsMass;
            deliveryAddress = address;
            deliveryType = "PickPoint";
        }
        public string deliveryAddress;
        public double mass;
    }

    class DigitalDelivery : Delivery
    {
        public DigitalDelivery(int size, string mail)
        {
            sizeByte = size;
            emailAddress = mail;
            deliveryType = "Digital";
        }
        public string emailAddress;
        public int sizeByte;
    }

    abstract class BaseClient
    {
        public int customerId;
        public string address = "";
        public string phone = "";
        public string email = "";
        public string clientType = "";

    }

    class PersonClient : BaseClient
    {
        public PersonClient(PersonDBdata data)
        {
            firstName = data.firstName;
            lastName = data.lastName;
            base.customerId = data.customerId;
            base.address = data.address;
            base.phone = data.phone;
            base.email = data.email;
            base.clientType = data.clientType;
        }
        public string firstName;
        public string lastName;
    }

    class CompanyClient : BaseClient
    {
        public CompanyClient(CompanyDBdata data)
        {
            companyName = data.companyName;
            tin = data.tin;
            contactPerson = data.contactPerson;
            base.customerId = data.customerId;
            base.address = data.address;
            base.phone = data.phone;
            base.email = data.email;
            base.clientType = data.clientType;
        }
        public string companyName;
        public string tin;
        public string contactPerson;
    }

    /* Emulate database answer for person client data arequest */
    class PersonDBdata
    {
        public int customerId = 1;
        public string clientType = "person";
        public string address = "Rostov-on-Don, Pushkina street, Kolotushkin house, 34, ap 11";
        public string phone = "8-800-555-35-35 ad. 1";
        public string email = "misterDirector@theMail.com";
        public string firstName = "Pereira";
        public string lastName = "Sebastian";
    }

    /* Emulate database answer for company client data arequest */
    class CompanyDBdata
    {
        public int customerId = 2;
        public string clientType = "company";
        public string address = "Rostov-on-Don, Pushkina street, Kolotushkin house, 34, ap 12";
        public string phone = "8-800-555-35-35";
        public string email = "theSuperCompany@theMail.com";
        public string companyName = "theSuperCompany";
        public string tin = "2996792000000004";
        public string contactPerson = "Pereira Sebastian";
    }

    class Customer
    {
        public Customer(int customerId)
        {
            /* Here I get client info from database. But we dont have DB, thats why I create class with ABSOLUTELY RANDOM values. */
            /* So, here i select from database clientType */
            string clientTypeFromDB = "person";


            if (clientTypeFromDB == "person")
            {
                PersonDBdata personDBdata = new PersonDBdata();
                client = new PersonClient(personDBdata);
            }
            else if (clientTypeFromDB == "company")
            {
                CompanyDBdata CompanyDBdata = new CompanyDBdata();
                client = new CompanyClient(CompanyDBdata);
            }
        }
        public BaseClient client;
    }
    class Order
    {
        public Order(int clientId, int[] productIDs, string deliveryType)
        {
            customer = new Customer(clientId);
            products = productIDs;
            switch (deliveryType)
            {
                case "Home":
                    delivery = new HomeDelivery(massCalculator(productIDs), customer.client.address);
                    break;
                case "PickPoint":
                    delivery = new PickPointDelivery(massCalculator(productIDs), customer.client.address);
                    break;
                case "Digital":
                    delivery = new DigitalDelivery(sizeCalculator(productIDs), customer.client.email);
                    break;
            }
        }
        private double massCalculator(int[] products)
        {
            /* Here I get mass from database, but...you know...rnd.Next */
            Random randomizer = new Random();
            return randomizer.Next(1 / 100, 999999 / 100);
        }
        private int sizeCalculator(int[] products)
        {
            /* Here I get size in byte from database....sooooooooo.... */
            Random randomizer = new Random();
            return randomizer.Next(1, 999999);
        }

        private Customer customer;
        private Delivery delivery;
        private int[] products;

        public int getOrderPrice()
        {
            int sum = 0;
            foreach (int id in products)
            {
                /* Here I get prices current order from database and sum. But I havent DB, sooooooo, little randomize again */
                Random randomizer = new Random();
                sum += randomizer.Next(50, 10000);
            }
            return sum;
        }

        public string getDeliveryType()
        {
            return delivery.deliveryType;
        }

        public string getCustomerType()
        {
            return customer.client.clientType;
        }
    }


    static void Main(string[] args)
    {
        int[] productIDList = new int[5] { 12, 333, 45, 66, 7 };
        Order order = new Order(1, productIDList, "Digital");
        Console.WriteLine(order.getOrderPrice().ToString());
        Console.WriteLine(order.getCustomerType());
        Console.WriteLine(order.getDeliveryType());
    }
}