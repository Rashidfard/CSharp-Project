using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rashidfard.Project.Models
{
    public class OrderDetail
    {
        public int id { get; set; }
        public string product_name { get; set; }
        public int product_order_count { get; set; }
        public int product_price { get; set; }
        public int sum { get; set; }
        [ForeignKey("Orderid")]
        public int Orderid { get; set; }
        [ForeignKey("Productid")]
        public int Productid { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }

        public void Add_City_Product_Orderdetail()
        {
            City city = new City();
            city.Show_Table();
            Console.WriteLine("enter name of city : ");
            Dbproject db = new Dbproject();
            string cityname = Console.ReadLine().ToLower();
            List<City> cityfind = db.Cities.ToList().Where(x => x.name == cityname).ToList();
            if (cityfind.Count == 0)
            {
                Console.WriteLine($"city with name '{cityname}' did not find in city table");
            }
            db.Cities.Include(x => x.products).ThenInclude(x => x.orderDetails).ToList().Where(x => x.name.ToLower() == cityname).ToList().ForEach(x =>
            {
                Console.WriteLine($"city id : {x.id} \t city name : {x.name}");

                x.products.ToList().ForEach(y =>
                {
                    Console.WriteLine($"product id : {y.id} \t product name : {y.name} \t product count : {y.count} \t product price : {y.price}");
                });
                Console.WriteLine("enter name of product : ");
                string productname = Console.ReadLine().ToLower();
                List<Product> productlist = db.Products.ToList().Where(x => x.name == productname).ToList();
                if (productlist.Count == 0)
                {
                    Console.WriteLine($"product with name '{productname}' did not find");
                }
                x.products.ToList().Where(y => y.name.ToLower() == productname).ToList().ForEach(y =>
                {
                    Console.WriteLine("enter firstname of customer : ");
                    string fname = Console.ReadLine().ToLower();
                    Console.WriteLine("enter lastname of customer : ");
                    string lname = Console.ReadLine().ToLower();
                    Console.WriteLine("enter phonenumber of customer : ");
                    string phnumber = Console.ReadLine().ToLower();
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
                        orderlist.Add(orders);
                        customer.orders = orderlist;
                        db.AddRange(customer);
                        db.SaveChanges();
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.product_name = y.name;
                        Console.WriteLine("enter product order count : ");
                        orderDetail.product_order_count = int.Parse(Console.ReadLine());
                        orderDetail.product_price = y.price;
                        orderDetail.sum = orderDetail.product_order_count * orderDetail.product_price;
                        orderDetail.Orderid = orders.id;
                        orderDetail.Productid = y.id;
                        Product product = new Product();
                        product.Update_Product_Count(orderDetail.Productid, orderDetail.product_order_count);
                        db.Add(orderDetail);
                        db.SaveChanges();
                        Console.WriteLine($"orderdetail with order number {orderDetail.Orderid} added in {orders.order_registration_datetime}");
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
                            orderlist.Add(orders);
                            findcustomer.orders = orderlist;
                            db.Update(findcustomer);
                            db.SaveChanges();
                            OrderDetail orderDetail = new OrderDetail();
                            orderDetail.product_name = y.name;
                            Console.WriteLine("enter product order count : ");
                            orderDetail.product_order_count = int.Parse(Console.ReadLine());
                            orderDetail.product_price = y.price;
                            orderDetail.sum = orderDetail.product_order_count * orderDetail.product_price;
                            orderDetail.Orderid = orders.id;
                            orderDetail.Productid = y.id;
                            Product product = new Product();
                            product.Update_Product_Count(orderDetail.Productid, orderDetail.product_order_count);
                            db.Add(orderDetail);
                            db.SaveChanges();
                            Console.WriteLine($"orderdetail with order number {orderDetail.Orderid} added in {orders.order_registration_datetime}");
                        });
                    }
                });
            });
        }
        public void ShowTable()
        {
            Dbproject db = new Dbproject();
            List<OrderDetail> orderdetaillist = db.OrderDetails.ToList();
            if(orderdetaillist.Count == 0)
            {
                Console.WriteLine("there is no orderdetail in orderdetail table");
            }
            db.OrderDetails.ToList().ForEach(x => Console.WriteLine($"id : {x.id}   name : {x.product_name}   \t order count : {x.product_order_count} \t price : {x.product_price} \t sum : {x.sum} \t product id : {x.Productid} \t order id : {x.Orderid}"));
        }
        public void Delete_Orderdetail_By_Order()
        {
            Dbproject db = new Dbproject();
            Order order = new Order();
            order.Show_Table();
            Console.WriteLine("enter order id number for delete orderdetail : ");
            int id = int.Parse(Console.ReadLine());
            List<Order> orderlist = db.Orders.ToList().Where(x => x.id == id).ToList();
            if(orderlist.Count == 0)
            {
                Console.WriteLine($"order with id '{id}' did not find in order table");
            }
            db.Orders.Include(x => x.orderDetails).ToList().Where(x => x.id == id).ToList().ForEach(x =>
            {
                Console.WriteLine($"order id : {x.id} \t order registration datetime : {x.order_registration_datetime} \t order delivery date : {x.order_delivery_date}");
                x.orderDetails.ToList().ForEach(y =>
                {
                    Console.WriteLine($"id : {y.id} \t product name : {y.product_name} \t order count : {y.product_order_count} \t price : {y.product_price} \t sum : {y.sum} \t product id : {y.Productid} \t order id : {y.Orderid}");
                });
                Console.WriteLine("enter orderdetail id for delete : ");
                int orderdetailid = int.Parse(Console.ReadLine());
                OrderDetail findorderdetail = db.OrderDetails.Find(orderdetailid);
                if (findorderdetail != null)
                {
                    Product product = new Product();
                    product.Update_Product_Count_Delete_Orderdetail(findorderdetail.Productid, findorderdetail.product_order_count);
                    db.Remove(findorderdetail);
                    db.SaveChanges();
                    Console.WriteLine($"orderdetail with id : {orderdetailid} product deleted successfully");
                }
                else
                {
                    Console.WriteLine($"orderdetail id with id '{orderdetailid}' did not find");
                }
            });
        }
        public void Update_Orderdetail_By_Order()
        {
            Dbproject db = new Dbproject();
            Order order = new Order();
            order.Show_Table();
            Console.WriteLine("enter order id number for update :");
            int id = int.Parse(Console.ReadLine());
            List<Order> orderlist = db.Orders.ToList().Where(x => x.id == id).ToList();
            if (orderlist.Count == 0)
            {
                Console.WriteLine($"order with id '{id}' did not find in order table");
            }
            else
            {
                OrderDetail orderdetail = new OrderDetail();
                orderdetail.Show_Orderdetail_By_Orderid(id);
                Console.WriteLine("do you want to delete product from factor ?\nif you want to delete press 'y'");
                string choice = Console.ReadLine().ToLower();
                if (choice == "y")
                {
                    int counter = 1;
                    while (true)
                    {
                        Product product = new Product();
                        Console.WriteLine($"enter {counter}th orderdetail id for delete : ");
                        int ordid = int.Parse(Console.ReadLine());
                        List<OrderDetail> orderdetaillist = db.OrderDetails.ToList().Where(x => x.id == ordid).ToList();
                        if(orderdetaillist.Count == 0)
                        {
                            Console.WriteLine($"orderdetail with id '{ordid}' did not find");
                        }
                        else
                        {
                            OrderDetail findorderdetail = db.OrderDetails.Find(ordid);
                            product.Update_Product_Count_Delete_Orderdetail(findorderdetail.Productid, findorderdetail.product_order_count);
                            db.Remove(findorderdetail);
                            db.SaveChanges();
                            Console.WriteLine("do you want to delete more product from factor ?\nif you don't want to contiue press 'n'");
                            string choice2 = Console.ReadLine().ToLower();
                            if (choice2 == "n")
                                break;
                            counter++;
                        }
                    }
                }
                Console.WriteLine("do you want to add product to factor ?\nif you want to add press 'y'");
                string choice1 = Console.ReadLine().ToLower();
                if (choice1 == "y")
                {
                    int id2 = id;
                    int counter1 = 1;
                    while (true)
                    {
                        OrderDetail orderdetail1 = new OrderDetail();
                        orderdetail1.Show_Orderdetail_By_Orderid(id2);
                        Product product = new Product();
                        product.Show_Table();
                        Console.WriteLine($"enter {counter1}th product id for add : ");
                        int proid = int.Parse(Console.ReadLine());
                        List<Product> productlist = db.Products.ToList().Where(x => x.id == proid).ToList();
                        if (productlist.Count == 0)
                        {
                            Console.WriteLine($"product with id '{proid}' did not find");
                        }
                        else
                        {
                            Product findproduct = db.Products.Find(proid);
                            orderdetail1.product_name = findproduct.name;
                            Console.WriteLine($"enter {counter1}th product order count : ");
                            orderdetail1.product_order_count = int.Parse(Console.ReadLine());
                            orderdetail1.product_price = findproduct.price;
                            orderdetail1.sum = orderdetail1.product_order_count * orderdetail1.product_price;
                            orderdetail1.Productid = findproduct.id;
                            orderdetail1.Orderid = id2;
                            db.Add(orderdetail1);
                            db.SaveChanges();
                            product.Update_Product_Count(findproduct.id, orderdetail1.product_order_count);
                            Console.WriteLine("do you want to add more product to factor ?\nif you don't want to contiue press 'n'");
                            string choice2 = Console.ReadLine().ToLower();
                            if (choice2 == "n")
                                break;
                            counter1++;
                        }
                    }
                }
            }
        }
        public void Update_Orderdetail_By_Order_Customer()
        {
            Dbproject db = new Dbproject();
            Console.WriteLine("enter your firstname : ");
            string fname = Console.ReadLine().ToLower();
            Console.WriteLine("enter your lastname : ");
            string lname = Console.ReadLine().ToLower();
            List<Customer> customerlist = db.Customers.ToList().Where(x => x.firstname == fname && x.lastname == lname).ToList();
            if (customerlist.Count == 0)
            {
                Console.WriteLine($"customer with firstname '{fname}' and lastname '{lname}' did not find in customer table");
            }
            else
            {
                Console.WriteLine("enter order id number for update :");
                int id = int.Parse(Console.ReadLine());
                List<Order> orderlist = db.Orders.ToList().Where(x => x.id == id).ToList();
                if (orderlist.Count == 0)
                {
                    Console.WriteLine($"order with id '{id}' did not find in order table");
                }
                else
                {
                    OrderDetail orderdetail = new OrderDetail();
                    orderdetail.Show_Orderdetail_By_Orderid(id);
                    Console.WriteLine("do you want to delete product from factor ?\nif you want to delete press 'y'");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "y")
                    {
                        int counter = 1;
                        while (true)
                        {
                            Product product = new Product();
                            Console.WriteLine($"enter {counter}th orderdetail id for delete : ");
                            int ordid = int.Parse(Console.ReadLine());
                            List<OrderDetail> orderdetaillist = db.OrderDetails.ToList().Where(x => x.id == ordid).ToList();
                            if (orderdetaillist.Count == 0)
                            {
                                Console.WriteLine($"orderdetail with id '{ordid}' did not find");
                            }
                            else
                            {
                                OrderDetail findorderdetail = db.OrderDetails.Find(ordid);
                                product.Update_Product_Count_Delete_Orderdetail(findorderdetail.Productid, findorderdetail.product_order_count);
                                db.Remove(findorderdetail);
                                db.SaveChanges();
                                Console.WriteLine("do you want to delete more product from factor ?\nif you don't want to contiue press 'n'");
                                string choice2 = Console.ReadLine().ToLower();
                                if (choice2 == "n")
                                    break;
                                counter++;
                            }
                        }
                    }
                    Console.WriteLine("do you want to add product to factor ?\nif you want to add press 'y'");
                    string choice1 = Console.ReadLine().ToLower();
                    if (choice1 == "y")
                    {
                        int id2 = id;
                        int counter1 = 1;
                        while (true)
                        {
                            OrderDetail orderdetail1 = new OrderDetail();
                            orderdetail1.Show_Orderdetail_By_Orderid(id2);
                            Product product = new Product();
                            product.Show_Table();
                            Console.WriteLine($"enter {counter1}th product id for add : ");
                            int proid = int.Parse(Console.ReadLine());
                            List<Product> productlist = db.Products.ToList().Where(x => x.id == proid).ToList();
                            if (productlist.Count == 0)
                            {
                                Console.WriteLine($"product with id '{proid}' did not find");
                            }
                            else
                            {
                                Product findproduct = db.Products.Find(proid);
                                orderdetail1.product_name = findproduct.name;
                                Console.WriteLine($"enter {counter1}th product order count : ");
                                orderdetail1.product_order_count = int.Parse(Console.ReadLine());
                                orderdetail1.product_price = findproduct.price;
                                orderdetail1.sum = orderdetail1.product_order_count * orderdetail1.product_price;
                                orderdetail1.Productid = findproduct.id;
                                orderdetail1.Orderid = id2;
                                db.Add(orderdetail1);
                                db.SaveChanges();
                                product.Update_Product_Count(findproduct.id, orderdetail1.product_order_count);
                                Console.WriteLine("do you want to add more product to factor ?\nif you don't want to contiue press 'n'");
                                string choice2 = Console.ReadLine().ToLower();
                                if (choice2 == "n")
                                    break;
                                counter1++;
                            }
                        }
                    }
                }
            }
        }
        public void Show_Orderdetail_By_Orderid(int id)
        {
            Dbproject db = new Dbproject();
            db.OrderDetails.ToList().Where(x => x.Orderid == id).ToList().ForEach(x =>
            {
                Console.WriteLine($"id : {x.id} \t name : {x.product_name} \t order count : {x.product_order_count} \t price : {x.product_price} \t sum : {x.sum} \t product id : {x.Productid} \t order id : {x.Orderid}");
            });
        }
        public void Factor(int id)
        {
            Dbproject db = new Dbproject();
            Order order = new Order();
            List<Order> orderlist = db.Orders.ToList().Where(x => x.id == id).ToList();
            if(orderlist.Count == 0)
            {
                Console.WriteLine($"order with id '{id}' did not find in order table");
            }
            else
            {
                db.Orders.ToList().Where(x => x.id == id).ToList().ForEach(x =>
                {
                    Console.WriteLine($"order id : {x.id}     {x.order_registration_datetime} \ndelivery date \t\t   {x.order_delivery_date}");
                });
                List<OrderDetail> orderdetaillist = db.OrderDetails.ToList().Where(x => x.Orderid == id).ToList();
                int sum = 0;
                foreach (OrderDetail orderdetail in orderdetaillist)
                {
                    sum += orderdetail.sum;
                    Console.WriteLine($"{orderdetail.Productid}    {orderdetail.product_name} \t {orderdetail.product_order_count} \t {orderdetail.product_price} \t {orderdetail.sum}");
                }
                Console.WriteLine($"sum \t\t\t\t {sum}");
            }
        }
        public void Report_By_Customer()
        {
            Dbproject db = new Dbproject();
            Console.WriteLine("enter customer firstname : ");
            string fname = Console.ReadLine();
            Console.WriteLine("enter customer lastname : ");
            string lname = Console.ReadLine();
            List<Customer> customerlist = db.Customers.ToList().Where(x => x.firstname == fname && x.lastname == lname).ToList();
            if (customerlist.Count == 0)
            {
                Console.WriteLine($"customer with firstname '{fname}' lastname '{lname}' did not find in customer table");
            }
            db.Customers.Include(x => x.orders).ThenInclude(x => x.orderDetails).ToList().Where(x => x.firstname == fname && x.lastname == lname).ToList().ForEach(x =>
            {
                Console.WriteLine($"id : {x.id} \t firstname : {x.firstname} \t lastname : {x.lastname}");
                List<int> finalSum = new List<int>();
                x.orders.ToList().ForEach(y =>
                {
                    Console.WriteLine($"order id : {y.id} \t order registration datetime : {y.order_registration_datetime} \t order delivery date : {y.order_delivery_date}");
                    int sum1 = 0;
                    y.orderDetails.ToList().ForEach(z =>
                    {
                        Console.WriteLine($"name : {z.product_name} \t count : {z.product_order_count} \t price : {z.product_price} \t sum : {z.sum}");
                        sum1 += z.sum;
                    });
                    finalSum.Add(sum1);
                });
                int finalsumprice = 0;
                foreach (int number in finalSum)
                {
                    finalsumprice += number;
                }
                Console.WriteLine($"final customer orders price : {finalsumprice}");
            });
        }
        public void Report_Customer_Order_OrderDetail()
        {
            Dbproject db = new Dbproject();
            List<OrderDetail> orderdetaillist = db.OrderDetails.ToList();
            if (orderdetaillist.Count == 0)
            {
                Console.WriteLine($"there is no orderdetail for report");
            }
            List<Reports> reportlist = new List<Reports>();
            db.Customers.Include(x => x.orders).ThenInclude(x => x.orderDetails).ToList().ForEach(x =>
            {
                List<int> finalSum = new List<int>();
                x.orders.ToList().ForEach(y =>
                {
                    int sum1 = 0;
                    y.orderDetails.ToList().ForEach(z =>
                    {
                        sum1 += z.sum;
                    });
                    finalSum.Add(sum1);
                });
                int finalsumprice = 0;
                foreach (int number in finalSum)
                {
                    finalsumprice += number;
                }
                Reports reports = new Reports();
                reports.customerid = x.id;
                reports.customerfirstname = x.firstname;
                reports.customerlastname = x.lastname;
                reports.sumprice = finalsumprice;
                reportlist.Add(reports);
            });
            reportlist.OrderByDescending(x => x.sumprice).ToList().ForEach(x => Console.WriteLine($"customer id : {x.customerid} \t firstname : {x.customerfirstname} \t lastname : {x.customerlastname} \t total price : {x.sumprice} "));
        }
        public void Report_City_Product_OrderDetail ()
        {
            Dbproject db = new Dbproject();
            List<OrderDetail> orderdetaillist = db.OrderDetails.ToList();
            if (orderdetaillist.Count == 0)
            {
                Console.WriteLine($"there is no orderdetail for report");
            }
            List<ReportCity> reportcitylist = new List<ReportCity>();
            db.Cities.Include(x => x.products).ThenInclude(x => x.orderDetails).ToList().ForEach(x =>
            {
                x.products.ToList().ForEach(y =>
                {
                    int sumprice = 0;
                    int sumcount = 0;
                    y.orderDetails.ToList().ForEach(z =>
                    {
                        sumprice += z.sum;
                        sumcount += z.product_order_count;
                    });
                    ReportCity reportcity = new ReportCity();
                    reportcity.cityname = x.name;
                    reportcity.productname = y.name;
                    reportcity.totalcount = sumcount;   
                    reportcity.totalprice = sumprice;
                    reportcitylist.Add(reportcity);
                });
            });
            reportcitylist.OrderByDescending(x => x.totalcount).ToList().ForEach(x => Console.WriteLine($"city : {x.cityname}     \t product : {x.productname}     \t  total count : {x.totalcount}     \t total price : {x.totalprice} "));
        }
    }
}
