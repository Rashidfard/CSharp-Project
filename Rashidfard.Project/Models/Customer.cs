using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashidfard.Project.Models
{
    public class Customer
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phonenumber { get; set; }
        public ICollection<Order> orders { get; set; }
        public void Add_Customer()
        {
            Dbproject db = new Dbproject();
            List<Customer> customerlist = new List<Customer>();
            int counter = 1;
            while (true)
            {
                Customer customer = new Customer();
                Console.WriteLine($"enter {counter}th customer firstname : ");
                customer.firstname = Console.ReadLine().ToLower();
                Console.WriteLine($"enter {counter}th customer lastname : ");
                customer.lastname = Console.ReadLine().ToLower();
                Console.WriteLine($"enter {counter}th customer phonenumber : ");
                customer.phonenumber = Console.ReadLine().ToLower();
                Console.WriteLine("do you want to continue ?\nif you don't want to continue press 'n'");
                customerlist.Add(customer);
                string choice = Console.ReadLine();
                if (choice == "n")
                    break;
                counter++;
            }
            foreach (Customer enteredcustomer in customerlist)
            {
                List<Customer> findcustomer = db.Customers.ToList().Where(x => x.firstname.ToLower() == enteredcustomer.firstname && x.lastname.ToLower() == enteredcustomer.lastname).ToList();
                if (findcustomer.Count == 0)
                {
                    db.Add(enteredcustomer);
                    db.SaveChanges();
                    Console.WriteLine($"customer with firstname '{enteredcustomer.firstname}' lastname '{enteredcustomer.lastname}' phonenumber '{enteredcustomer.phonenumber}' added successfully");
                }
                else
                {
                    Console.WriteLine($"customer with firstname '{enteredcustomer.firstname}' lastname '{enteredcustomer.lastname}' is exist in customer table");
                }
            }
        }
        public void Add_User_Customer()
        {
            Dbproject db = new Dbproject();
            Customer customer = new Customer();
            Console.WriteLine($"enter customer firstname : ");
            customer.firstname = Console.ReadLine().ToLower();
            Console.WriteLine($"enter customer lastname : ");
            customer.lastname = Console.ReadLine().ToLower();
            Console.WriteLine($"enter customer phonenumber : ");
            customer.phonenumber = Console.ReadLine().ToLower();
            List<Customer> findcustomer = db.Customers.ToList().Where(x => x.firstname.ToLower() == customer.firstname && x.lastname.ToLower() == customer.lastname).ToList();
            if (findcustomer.Count == 0)
            {
                db.Add(customer);
                db.SaveChanges();
                Console.WriteLine($"customer with firstname '{customer.firstname}' lastname '{customer.lastname}' phonenumber '{customer.phonenumber}' added successfully");
            }
            else
            {
                Console.WriteLine($"customer with id '{customer.id}' firstname '{customer.firstname}' lastname '{customer.lastname}' is exist in customer table");
            }
        }
        public void Show_Table()
        {
            Dbproject db = new Dbproject();
            List<Product> producttable = db.Products.ToList();
            if(producttable.Count == 0)
            {
                Console.WriteLine("there is no customer in customer table");
            }
            db.Customers.ToList().ForEach(x => Console.WriteLine($"id : {x.id} \tfirstname : {x.firstname} \tlastname : {x.lastname} \tphonenumber : {x.phonenumber}"));
        }
        public void Search_By_Name()
        {
            Dbproject db = new Dbproject();
            Console.WriteLine("enter firstname : ");
            string fname = Console.ReadLine().ToLower();
            Console.WriteLine("enter lastname : ");
            string lname = Console.ReadLine().ToLower();
            List<Customer> findcustomer = db.Customers.ToList().Where(x => x.firstname.ToLower() == fname && x.lastname.ToLower() == lname).ToList();
            if(findcustomer.Count == 0)
            {
                Console.WriteLine($"customer with firstname : {fname} and lastname : {lname} did not find in customer table");
            }
            db.Customers.ToList().Where(x => x.firstname.ToLower() == fname && x.lastname.ToLower() == lname).ToList().ForEach(x =>
            {
            Console.WriteLine($"id : {x.id} \tfirstname : {x.firstname} \tlastname : {x.lastname} \tphonenumber : {x.phonenumber}");
            });
        }
        public void Delete_By_Name()
        {
            Dbproject db = new Dbproject();
            Customer customer = new Customer();
            customer.Show_Table();
            Console.WriteLine("enter firstname : ");
            string fname = Console.ReadLine().ToLower();
            Console.WriteLine("enter lastname : ");
            string lname = Console.ReadLine().ToLower();
            List<Customer> findcustomer = db.Customers.ToList().Where(x => x.firstname.ToLower() == fname && x.lastname.ToLower() == lname).ToList();
            if(findcustomer.Count == 0)
            {
                Console.WriteLine($"customer with firstname : {fname} lastname : {lname} did not find in customer table");
            }
            else
            {
                db.Customers.ToList().Where(x => x.firstname.ToLower() == fname && x.lastname.ToLower() == lname).ToList().ForEach(x =>
                {
                    Customer findcustomerid = db.Customers.Find(x.id);
                    if(findcustomerid != null)
                    {
                        db.Remove(findcustomerid);
                        db.SaveChanges();
                        Console.WriteLine($"customer with id : {findcustomerid.id} firstname : {findcustomerid.firstname} lastname : {findcustomerid.lastname} phonenumber : {findcustomerid.phonenumber} successfully deleted from customer table");
                    }
                });
            }
        }
        public void Update_By_Name()
        {
            Dbproject db = new Dbproject();
            Customer customer = new Customer();
            customer.Show_Table();
            Console.WriteLine("enter firstname : ");
            string fname = Console.ReadLine().ToLower();
            Console.WriteLine("enter lastname : ");
            string lname = Console.ReadLine().ToLower();
            List<Customer> findcustomer = db.Customers.ToList().Where(x => x.firstname.ToLower() == fname && x.lastname.ToLower() == lname).ToList();
            if(findcustomer.Count == 0)
            {
                Console.WriteLine($"customer with firstname : {fname} lastname : {lname} did not find in customer table");
            }
            else
            {
                db.Customers.ToList().Where(x => x.firstname.ToLower() == fname && x.lastname.ToLower() == lname).ToList().ForEach(x =>
                {
                    Customer findcustomerid = db.Customers.Find(x.id);
                    string phnumber = findcustomerid.phonenumber;
                    if(findcustomerid != null)
                    {
                        Console.WriteLine("enter firstname for replace : ");
                        x.firstname = Console.ReadLine().ToLower();
                        Console.WriteLine("enter lastname for replace : ");
                        x.lastname = Console.ReadLine().ToLower();
                        Console.WriteLine("enter phonenumber for replace : ");
                        x.phonenumber = Console.ReadLine().ToLower();
                        db.Update(x);
                        db.SaveChanges();
                        Console.WriteLine($"customer firstname : {fname}, lastname : {lname}, phonenumber : {phnumber} changed to firstname : {x.firstname}, lastname : {x.lastname},phonenumber : {x.phonenumber}");
                    }
                });
            }
        }
    }
}
