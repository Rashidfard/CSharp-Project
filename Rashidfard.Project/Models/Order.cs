using Microsoft.EntityFrameworkCore;
using Rashidfard.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashidfard.Project.Models
{
    public class Order
    {
        public int id { get; set; }
        public string order_registration_datetime { get; set; }
        public string order_delivery_date { get; set; }
        public int Customerid { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderDetail> orderDetails { get; set; }
        
        public void Add_Customr_Order()
        {
            Console.WriteLine("enter firstname : ");
            string fname = Console.ReadLine().ToLower();
            Console.WriteLine("enter lastname : ");
            string lname = Console.ReadLine().ToLower();
            Console.WriteLine("enter phonenumber : ");
            string phnumber = Console.ReadLine().ToLower();
            Dbproject db = new Dbproject();
            List<Customer> existcustomer = db.Customers.ToList().Where(x => x.firstname == fname && x.lastname == lname).ToList();
            if (existcustomer.Count == 0)
            {
                Customer customer = new Customer();
                customer.firstname = fname;
                customer.lastname = lname;
                customer.phonenumber = phnumber;
                List<Order> orderlist = new List<Order>();
                Order orders = new Order();
                orders.order_registration_datetime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                DateTime today = DateTime.Now;
                TimeSpan duration = new TimeSpan(5, 0, 0, 0);
                DateTime answer = today.Add(duration);
                orders.order_delivery_date = answer.ToString("dd/MM/yyyy");
                orders.Customerid = customer.id;
                Product product = new Product();
                product.Show_Table();
                List<OrderDetail> orderdetaillist = new List<OrderDetail>();
                int counter = 1;
                while (true)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    Console.WriteLine($"enter {counter}th product id : ");
                    int productid = int.Parse(Console.ReadLine().ToLower());
                    orderDetail.product_name = product.Search_Name_By_Product_Id(productid);
                    Console.WriteLine($"enter {counter}th product count : ");
                    orderDetail.product_order_count = int.Parse(Console.ReadLine());
                    orderDetail.product_price = product.Search_Price_By_Product_Id(productid);
                    orderDetail.sum = orderDetail.product_order_count * orderDetail.product_price;
                    orderDetail.Productid = productid;
                    orderDetail.Orderid = orders.id;
                    product.Update_Product_Count(productid, orderDetail.product_order_count);
                    orderdetaillist.Add(orderDetail);
                    Console.WriteLine("do you want to add more product ? \nif you don't want to continue press 'n'");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                        break;
                    counter++;
                }
                orders.orderDetails = orderdetaillist;
                orderlist.Add(orders);
                customer.orders = orderlist;
                db.AddRange(customer);
                db.SaveChanges();
                Console.WriteLine($"customer with firstname '{customer.firstname}' and lastname '{customer.lastname}' and phonenumber '{customer.phonenumber}' added successfully");
                Console.WriteLine($"order with id '{orders.id}' order registration datetime '{orders.order_registration_datetime}' order delivery date '{orders.order_delivery_date}' added successfully");
                OrderDetail orderDetail1 = new OrderDetail();
                orderDetail1.Factor(orders.id);
            }
            else
            {
                db.Customers.ToList().Where(x => x.firstname == fname && x.lastname == lname).ToList().ForEach(x =>
                {
                    Customer findcustomer = db.Customers.Find(x.id);
                    Console.WriteLine($"customer with firstname '{fname}' and lastname '{lname}' and phonenumber '{phnumber}' updated successfully");
                    List<Order> orderlist = new List<Order>();
                    Order orders = new Order();
                    orders.order_registration_datetime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    DateTime today = DateTime.Now;
                    TimeSpan duration = new TimeSpan(5, 0, 0, 0);
                    DateTime answer = today.Add(duration);
                    orders.order_delivery_date = answer.ToString("dd/MM/yyyy");
                    orders.Customerid = findcustomer.id;
                    Product product = new Product();
                    product.Show_Table();
                    List<OrderDetail> orderdetaillist = new List<OrderDetail>();
                    int counter1 = 1;
                    while (true)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        Console.WriteLine($"enter {counter1}th product id : ");
                        int productid = int.Parse(Console.ReadLine().ToLower());
                        orderDetail.product_name = product.Search_Name_By_Product_Id(productid);
                        Console.WriteLine($"enter {counter1}th product count : ");
                        orderDetail.product_order_count = int.Parse(Console.ReadLine());
                        orderDetail.product_price = product.Search_Price_By_Product_Id(productid);
                        orderDetail.sum = orderDetail.product_order_count * orderDetail.product_price;
                        orderDetail.Productid = productid;
                        orderDetail.Orderid = orders.id;
                        product.Update_Product_Count(productid, orderDetail.product_order_count);
                        orderdetaillist.Add(orderDetail);
                        Console.WriteLine("do you want to add more product ? \nif you don't want to continue press 'n'");
                        string choice = Console.ReadLine();
                        if (choice == "n")
                            break;
                        counter1++;
                    }
                    orders.orderDetails = orderdetaillist;
                    orderlist.Add(orders);
                    findcustomer.orders = orderlist;
                    db.Update(findcustomer);
                    db.SaveChanges();
                    Console.WriteLine($"customer with firstname '{findcustomer.firstname}' and lastname '{findcustomer.lastname}' and phonenumber '{findcustomer.phonenumber}' updated successfully");
                    Console.WriteLine($"order with id '{orders.id}' order registration datetime '{orders.order_registration_datetime}' order delivery date '{orders.order_delivery_date}' added successfully");
                    OrderDetail orderDetail1 = new OrderDetail();
                    orderDetail1.Factor(orders.id);
                });
            }
        }
        public void Show_Table()
        {
            Dbproject db = new Dbproject();
            List<Order> orderlist = db.Orders.ToList();
            if(orderlist.Count == 0)
            {
                Console.WriteLine("there is no order in order table");
            }
            db.Orders.Include(x => x.orderDetails).Include(x => x.Customer).ToList().ForEach(x =>
            {
                Console.WriteLine($"order id '{x.id}' \t order registration datetime '{x.order_registration_datetime}' \t order delivery date '{x.order_delivery_date}' \t customer '{x.Customer.firstname} {x.Customer.lastname}'");
                x.orderDetails.ToList().ForEach(y => Console.WriteLine($"orderdetail id '{y.id}' \t product name '{y.product_name}' \t product count '{y.product_order_count}' \t product price '{y.product_price}' \t sum '{y.sum}' \t product id '{y.Productid}'"));
            });
        }
        public void Search_Orderdetail_By_Orderid()
        {
            Dbproject db = new Dbproject();
            Console.WriteLine("enter order id number : ");
            int id = int.Parse(Console.ReadLine());
            List<Order> orderlist = db.Orders.ToList().Where(x => x.id == id).ToList();
            if(orderlist.Count == 0)
            {
                Console.WriteLine($"order with id '{id}' did not find in order table");
            }
            db.Orders.Include(x => x.orderDetails).Where(x => x.id == id).ToList().ForEach(x =>
            {
                Console.WriteLine($"order id : {x.id} \t order registration date : {x.order_registration_datetime} \t order delivery date : {x.order_delivery_date}");
                x.orderDetails.ToList().ForEach(y => Console.WriteLine($"product name : {y.product_name} \t count : {y.product_order_count} \t price : {y.product_price} \t sum : {y.sum}"));
            });
        }
        public void Delete_Order_Orderdetail()
        {
            Dbproject db = new Dbproject();
            Order order = new Order();
            order.Show_Table();
            Console.WriteLine("enter order id number for delete : ");
            int id = int.Parse(Console.ReadLine());
            List<Order> orderlist = db.Orders.ToList().Where(x => x.id == id).ToList();
            if (orderlist.Count == 0)
            {
                Console.WriteLine($"order with id '{id}' did not find in order table");
            }
            db.Orders.Include(x => x.orderDetails).ToList().Where(x => x.id == id).ToList().ForEach(x =>
            {
                x.orderDetails.ToList().ForEach(y =>
                {
                    Product product = new Product();
                    product.Update_Product_Count_Delete_Orderdetail(y.Productid, y.product_order_count);
                    db.Remove(y);
                });
                db.Remove(x);
                db.SaveChanges();
                Console.WriteLine($"order with id : {id} deleted successfully");
            });
        }
        public void Delete_Order_Orderdetail_By_Customer()
        {
            Dbproject db = new Dbproject();
            Console.WriteLine("enter your firstname : ");
            string fname = Console.ReadLine().ToLower();
            Console.WriteLine("enter your lastname : ");
            string lname = Console.ReadLine().ToLower();
            List<Customer> customerlist = db.Customers.ToList().Where(x => x.firstname == fname && x.lastname == lname).ToList();
            if(customerlist.Count == 0)
            {
                Console.WriteLine($"customer with firstname '{fname}' and lastname '{lname}' did not find in customer table");
            }
            else
            {
                db.Customers.Include(x => x.orders).ThenInclude(x => x.orderDetails).ToList().Where(x => x.firstname == fname && x.lastname == lname).ToList().ForEach(x =>
                {
                    x.orders.ToList().ForEach(y =>
                    {
                        Console.WriteLine($"id : {y.id}  \t registration date time : {y.order_registration_datetime} \t delivery date : {y.order_delivery_date} ");
                        y.orderDetails.ToList().ForEach(z =>
                        {
                            Console.WriteLine($"name : {z.product_name} \t   count : {z.product_order_count} \t price : {z.product_price} \t sum : {z.sum}");
                        });
                    });
                });
                Console.WriteLine("enter order id number for delete : ");
                int id = int.Parse(Console.ReadLine());
                List<Order> orderlist = db.Orders.ToList().Where(x => x.id == id).ToList();
                if (orderlist.Count == 0)
                {
                    Console.WriteLine($"order with id '{id}' did not find in order table");
                }
                db.Orders.Include(x => x.orderDetails).ToList().Where(x => x.id == id).ToList().ForEach(x =>
                {
                    x.orderDetails.ToList().ForEach(y =>
                    {
                        Product product = new Product();
                        product.Update_Product_Count_Delete_Orderdetail(y.Productid, y.product_order_count);
                        db.Remove(y);
                    });
                    db.Remove(x);
                    db.SaveChanges();
                    Console.WriteLine($"order with id : {id} deleted successfully");
                });
            } 
        }
        public void Update_Order()
        {
            Dbproject db = new Dbproject();
            Order order = new Order();
            order.Show_Table();
            Console.WriteLine("enter order id number for update : ");
            int id = int.Parse(Console.ReadLine());
            List<Order> orderlist = db.Orders.ToList().Where(x => x.id == id).ToList();
            if (orderlist.Count == 0)
            {
                Console.WriteLine($"order with id '{id}' did not find in order table");
            }
            db.Orders.ToList().Where(x => x.id == id).ToList().ForEach(x => 
            {
                Order findorder = db.Orders.Find(x.id);
                string datetime = findorder.order_delivery_date;
                if(findorder != null)
                {
                    DateTime today = DateTime.Now;
                    Console.WriteLine("how many days would order deliver ? ");
                    int days = int.Parse(Console.ReadLine());
                    TimeSpan duration = new TimeSpan(days, 0, 0, 0);
                    DateTime answer = today.Add(duration);
                    findorder.order_delivery_date = answer.ToString("dd/MM/yyyy");
                    Console.WriteLine($"order delivery date {datetime} changed to {findorder.order_delivery_date} successfully ");
                    db.Update(findorder);
                    db.SaveChanges();
                }
            });
        }
    }
}
