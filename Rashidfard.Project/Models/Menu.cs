namespace Rashidfard.Project.Models
{
    public class Menu
    {
        public void MENU()
        {
            while (true)
            {
                string choice = MainMenu();
                if (choice == "1")
                {
                    UserPassword userPassword = new UserPassword();
                    bool userpass = userPassword.Admin_User_Password();
                    if (userpass == true)
                    {
                        while(true)
                        {
                            Console.WriteLine();
                            string choice1 = AdminMenu();
                            City city = new City();
                            Product product = new Product();
                            Order order = new Order();
                            OrderDetail orderDetail = new OrderDetail();
                            Customer customer = new Customer();
                            UserPassword userPassword1 = new UserPassword();
                            if (choice1 == "1")
                            {
                                city.Add_City();
                            }
                            else if (choice1 == "2")
                            {
                                city.Show_Table();
                            }
                            else if (choice1 == "3")
                            {
                                city.Search_By_Name();
                            }
                            else if(choice1 == "4")
                            {
                                city.Delete_By_Name();
                            }
                            else if(choice1 == "5")
                            {
                                city.Update_By_Name();
                            }
                            else if(choice1 == "6")
                            {
                                product.Add_City_Product();
                            }
                            else if(choice1 == "7")
                            {
                                product.Show_Table();
                            }
                            else if(choice1 == "8")
                            {
                                product.Search_Product_By_City_Name();
                            }
                            else if(choice1 == "9")
                            {
                                product.Search_By_Product_Name();
                            }
                            else if(choice1 == "10")
                            {
                                product.Delete_By_Product_Name();
                            }
                            else if(choice1 == "11")
                            {
                                product.Delete_By_City_Product_Name();
                            }
                            else if(choice1 == "12")
                            {
                                product.Update_City_Product_By_Name();
                            }
                            else if (choice1 == "13")
                            {
                                customer.Add_Customer();
                            }
                            else if (choice1 == "14")
                            {
                                customer.Show_Table();
                            }
                            else if (choice1 == "15")
                            {
                                customer.Search_By_Name();
                            }
                            else if (choice1 == "16")
                            {
                                customer.Delete_By_Name();
                            }
                            else if (choice1 == "17")
                            {
                                customer.Update_By_Name();
                            }
                            else if (choice1 == "18")
                            {
                                order.Add_Customr_Order();
                            }
                            else if (choice1 == "19")
                            {
                                order.Show_Table();
                            }
                            else if (choice1 == "20")
                            {
                                order.Search_Orderdetail_By_Orderid();
                            }
                            else if (choice1 == "21")
                            {
                                order.Delete_Order_Orderdetail();
                            }
                            else if (choice1 == "22")
                            {
                                order.Update_Order();
                            }

                            else if (choice1 == "23")
                            {
                                orderDetail.Add_City_Product_Orderdetail();
                            }
                            else if(choice1 == "24")
                            {
                                orderDetail.ShowTable();
                            }
                            else if(choice1 == "25")
                            {
                                orderDetail.Delete_Orderdetail_By_Order();
                            }
                            else if(choice1 == "26")
                            {
                                orderDetail.Update_Orderdetail_By_Order();
                            }
                            else if(choice1 == "27")
                            {
                                orderDetail.Report_By_Customer();
                            }
                            else if(choice1 == "28")
                            {
                                orderDetail.Report_Customer_Order_OrderDetail();
                            }
                            else if (choice1 == "29")
                            {
                                orderDetail.Report_City_Product_OrderDetail();
                            }
                            else if(choice1 == "30")
                            {
                                Console.WriteLine("enter order id : ");
                                int id = int.Parse(Console.ReadLine());
                                orderDetail.Factor(id);
                            }
                            else if(choice1 == "31")
                            {
                                userPassword1.Update_Username_Password();
                            }
                            else if(choice1 == "32")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("you entered wrong number !!!");
                            }
                        }
                    } 
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("username or password is not correct");
                    }
                }
                else if (choice == "2")
                {
                    Customer customer = new Customer();
                    Product product = new Product();
                    Order order = new Order();
                    OrderDetail orderdetail = new OrderDetail();
                    while(true)
                    {
                        string choice2 = UserMenu();
                        if (choice2 == "1")
                        {
                            customer.Add_User_Customer();
                        }
                        else if (choice2 == "2")
                        {
                            product.Show_Table();
                        }
                        else if (choice2 == "3")
                        {
                            product.Filter_Price();
                        }
                        else if (choice2 == "4")
                        {
                            product.Search_Product_By_City_Name();
                        }
                        else if (choice2 == "5")
                        {
                            product.Search_By_Product_Name();
                        }
                        else if (choice2 == "6")
                        {
                            order.Add_Customr_Order();
                        }
                        else if (choice2 == "7")
                        {
                            order.Delete_Order_Orderdetail_By_Customer();
                        }
                        else if (choice2 == "8")
                        {
                            orderdetail.Update_Orderdetail_By_Order_Customer();
                        }
                        else if (choice2 == "9")
                        {
                            Console.WriteLine("enter order id : ");
                            int id = int.Parse(Console.ReadLine());
                            orderdetail.Factor(id);
                        }
                        else if (choice2 == "10")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("you entered wrong number !!!");
                        }
                    }
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("you entered wrong number !!!");
                }
            }
        }
        private string MainMenu()
        {
            
            Console.WriteLine("1. Admin \n2. User \n3. Exit");
            Console.WriteLine("enter number of your choice : ");
            string choice = Console.ReadLine();
            return choice;
        }
        private string AdminMenu()
        {
            Console.WriteLine($"City : \n 1. Add City \n 2. Show City Table \n 3. Search City \n 4. Delete City \n 5. Update City \n" +
                              $"Product : \n 6. Add City Product \n 7. Show Product Table \n 8. Search Products By City Name \n 9. Search Product By Name \n 10. Delete Products By Name \n 11. Delete Product By City Name \n 12. Update Product By City Name \n" +
                              $"Customer : \n 13. Add Customer \n 14. Show Customer Table \n 15. Search Customer \n 16. Delete Customer \n 17. Update Customer \n" +
                              $"Order : \n 18. Add Customer Order Orderdetail \n 19. Show Order Table \n 20. Search Orderdetail By Order Id \n 21. Delete Order By Order Id \n 22. Update Order Delivery Date \n" +
                              $"OrderDetail : \n 23. Add Orderdetail By City And Product Name \n 24. Show Orderdetail Table \n 25. Delete Orderdetail By Order Id \n 26. Update Orderdetail By Order Id \n 27. Report By Customer \n 28. Report Total Sell Price By Customer \n 29. Report Total Sell Count By City \n 30. Factor By Order Id \n" +
                              $"UserPassword : \n 31. Change Username and Password \n" +
                              $"Back To Main Menu : \n 32. Back To Main Menu");
            Console.WriteLine("enter number of your choice : ");
            string choice = Console.ReadLine();
            return choice;
        }
        private string UserMenu()
        {
            Console.WriteLine("1. Login \n2. Show Product \n3. Filter price \n4. Search Products By City Name \n5. Search By Product Name \n6. Add Order \n7. Delete Order \n8. Update Order \n9. Show Factor By Order Id \n10. Back To Main Menu");
            Console.WriteLine("enter number of your choice : ");
            string choice = Console.ReadLine();
            return choice;
        }
    }
}
