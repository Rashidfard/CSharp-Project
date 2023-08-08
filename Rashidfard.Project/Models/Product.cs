using Microsoft.EntityFrameworkCore;

namespace Rashidfard.Project.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public int price { get; set; }
        public int Cityid { get; set; }
        public City City { get; set; }
        public ICollection<OrderDetail> orderDetails { get; set; }
        
        public void Add_City_Product()
        {
            Dbproject db = new Dbproject();
            List<City> citylist = new List<City>();
            int counter1 = 1;
            while (true)
            {
                City city = new City();
                Console.WriteLine($"enter name of city {counter1}th : ");
                city.name = Console.ReadLine().ToLower();
                List<Product> productlist = new List<Product>();
                int counter = 1;
                while (true)
                {
                    Product product = new Product();
                    Console.WriteLine($"enter name of product {counter}th : ");
                    product.name = Console.ReadLine().ToLower();
                    try
                    {
                        Console.WriteLine($"enter count of product {counter}th : ");
                        product.count = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("you should enter integer!!!");
                        Console.WriteLine($"enter count of product {counter}th again : ");
                        product.count = int.Parse(Console.ReadLine());
                    }
                    try
                    {
                        Console.WriteLine($"enter price of product {counter}th : ");
                        product.price = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("you should enter integer!!!");
                        Console.WriteLine($"enter price of product {counter}th agian : ");
                        product.price = int.Parse(Console.ReadLine());
                    }
                    productlist.Add(product);
                    Console.WriteLine("do you want to add more product ?\nif you don't want to continue press 'n'");
                    string choice1 = Console.ReadLine().ToLower();
                    if (choice1 == "n")
                        break;
                    counter++;
                }
                city.products = productlist;
                citylist.Add(city);
                Console.WriteLine("do you want to add more city ?\nif you don't want to continue press 'n'");
                string choice = Console.ReadLine().ToLower();
                if (choice == "n")
                    break;
                counter1++;
            }
            foreach (City city in citylist)
            {
                List<City> existcity = db.Cities.ToList().Where(x => x.name == city.name).ToList();
                if (existcity.Count == 0)
                {
                    db.AddRange(city);
                    db.SaveChanges();
                    Console.WriteLine($"city with name '{city.name}' added successfully");
                    db.Cities.Include(x => x.products).Where(x => x.name == city.name).ToList().ForEach(x =>
                    x.products.ToList().ForEach(y => Console.WriteLine($"product with name '{y.name}' count '{y.count}' price '{y.price}' added successfully")));
                }
                else
                {
                    Console.WriteLine($"city with name '{city.name}' updated successfully");
                    db.Cities.ToList().Where(x => x.name == city.name).ToList().ForEach(x =>
                    {
                        City findcity = db.Cities.Find(x.id);
                        List<Product> existproductlist = db.Products.ToList().Where(x => x.Cityid == findcity.id).ToList();
                        List<Product> cityproductlist = city.products.ToList();
                        foreach (Product cityproduct in cityproductlist)
                        {
                            List<Product> plist = existproductlist.Where(x => x.name == cityproduct.name).ToList();
                            if (plist.Count == 0)
                            {
                                Product product1 = new Product();
                                product1.name = cityproduct.name;
                                product1.count = cityproduct.count;
                                product1.price = cityproduct.price;
                                product1.Cityid = findcity.id;
                                db.Add(product1);
                                Console.WriteLine($"product with name '{cityproduct.name}' count '{cityproduct.count}' price '{cityproduct.price}' added successfully");
                            }
                            else
                            {
                                foreach (Product p in plist)
                                {
                                    p.name = cityproduct.name;
                                    p.count = cityproduct.count;
                                    p.price = cityproduct.price;
                                    db.Update(p);
                                    Console.WriteLine($"product with name '{p.name}' count '{p.count}' price '{p.price}' updated successfully");
                                }
                            }
                        }
                    });
                db.SaveChanges();
                }
            }
        }
        public void Show_Table()
        {
            Dbproject db = new Dbproject();
            List<Product> producttable = db.Products.ToList();
            if(producttable.Count == 0)
            {
                Console.WriteLine("there is no product in product table");
            }
            db.Cities.Include(x => x.products).ToList().ForEach(x =>
            {
                Console.WriteLine($"city id : {x.id} \t city name : {x.name}");
                x.products.ToList().ForEach(y => Console.WriteLine($"product id : {y.id}   \t product name : {y.name}   \t product count : {y.count} \t product price : {y.price}"));
            });
        }
        public int Select_Id_By_City_Product_Name(string cityname, string productname)
        {
            Dbproject db = new Dbproject();
            db.Cities.Include(x => x.products).ToList().Where(x => x.name.ToLower() == cityname.ToLower()).ToList().ForEach(x =>
            {
                x.products.ToList().Where(y => y.name.ToLower() == productname.ToLower()).ToList().ForEach(y =>
                {
                    Product findproduct = db.Products.Find(y.id);
                    if (findproduct != null)
                    {
                        id = findproduct.id;
                    }
                });  
            });
            return id;
        }
        public void Search_Product_By_City_Name()
        {
            Dbproject db = new Dbproject();
            Console.WriteLine("enter name of city : ");
            string name = Console.ReadLine().ToLower();
            List<City> citylist = db.Cities.ToList().Where(x => x.name.ToLower() == name).ToList();
            if (citylist.Count == 0)
            {
                Console.WriteLine($"city with name '{name}' did not find in city table");
            }
            db.Cities.Include(x => x.products).ToList().Where(x => x.name.ToLower() == name).ToList().ForEach(x =>
            {
                Console.WriteLine($"city id : {x.id} \t city name : {x.name}");
                x.products.ToList().ForEach(y => Console.WriteLine($"product id: {y.id} \t product name: {y.name} \t product count: {y.count} \t product price: {y.price}"));
            });
        }
        public void Search_By_Product_Name()
        {
            Dbproject db = new Dbproject();
            Console.WriteLine("enter name of product : ");
            string name = Console.ReadLine().ToLower();
            List<Product> productlist = db.Products.ToList().Where(x => x.name.ToLower() == name).ToList();
            if (productlist.Count == 0)
            {
                Console.WriteLine($"product with name '{name}' did not find in product table");
            }
            db.Cities.Include(x => x.products).ToList().ForEach(x =>
            {
                x.products.ToList().Where(y => y.name.ToLower() == name).ToList().ForEach(y => Console.WriteLine($"product id: {y.id} \t product name: {y.name} \t product count: {y.count} \t product price: {y.price} \t city : {x.name}"));
            });
        }
        public int Search_Price_By_Product_Id(int id)
        {
            Dbproject db = new Dbproject();
            List<Product> productlist = db.Products.ToList().Where(x => x.id == id).ToList();
            int price = 0;
            foreach (Product product in productlist)
            {
                price = product.price;
            }
            return price;
        }
        public string Search_Name_By_Product_Id(int id)
        {
            Dbproject db = new Dbproject();
            List<Product> productlist = db.Products.ToList().Where(x => x.id == id).ToList();
            string name = "";
            foreach (Product product in productlist)
            {
                name = product.name;
            }
            return name;
        }
        public void Delete_By_Product_Name()
        {
            Product newproduct = new Product();
            Dbproject db = new Dbproject();
            newproduct.Show_Table();
            Console.WriteLine("enter name of product for delete : ");
            string name = Console.ReadLine().ToLower();
            List<Product> productlist = db.Products.ToList().Where(x => x.name.ToLower() == name).ToList();
            if (productlist.Count == 0)
            {
                Console.WriteLine($"product with name '{name}' did not find in product table");
            }
            db.Cities.Include(x => x.products).ToList().ForEach(x =>
            {
                x.products.ToList().Where(y => y.name.ToLower() == name).ToList().ForEach(y =>
                {
                    Product findproduct = db.Products.Find(y.id);
                    if (findproduct != null)
                    {
                        db.Remove(findproduct);
                        db.SaveChanges();
                        Console.WriteLine($"product with name '{y.name}' count '{y.count}' price '{y.price}' from city '{x.name}' deleted successfully");
                    }
                });
            });
        }
        public void Delete_By_City_Product_Name()
        {
            Product newproduct = new Product();
            Dbproject db = new Dbproject();
            newproduct.Show_Table();
            Console.WriteLine("enter name of city for product delete : ");
            string cityname = Console.ReadLine().ToLower();
            List<City> citylist = db.Cities.ToList().Where(x => x.name == cityname).ToList();
            if(citylist.Count == 0)
            {
                Console.WriteLine($"city with name '{cityname}' did not find in city table");
            }
            else
            {
                Console.WriteLine("enter name of product for delete : ");
                string productname = Console.ReadLine().ToLower();
                List<Product> productlist = db.Products.ToList().Where(x => x.name.ToLower() == productname).ToList();
                if (productlist.Count == 0)
                {
                    Console.WriteLine($"product with name '{productname}' did not find in product table");
                }
                db.Cities.Include(x => x.products).Where(x => x.name == cityname).ToList().ForEach(x =>
                {
                    x.products.ToList().Where(y => y.name.ToLower() == productname).ToList().ForEach(y =>
                    {
                        Product findproduct = db.Products.Find(y.id);
                        if (findproduct != null)
                        {
                            db.Remove(findproduct);
                            db.SaveChanges();
                            Console.WriteLine($"product with name '{y.name}' count '{y.count}' price '{y.price}' from city '{x.name}' deleted successfully");
                        }
                    });
                });
            }
        }
        public void Update_Product_Count(int id, int count)
        {
            Product newproduct = new Product();
            Dbproject db = new Dbproject();
            List<Product> productlist = db.Products.ToList().Where(x => x.id == id).ToList();
            if (productlist.Count == 0)
            {
                Console.WriteLine($"product with id {id} did not find");
            }
            db.Products.ToList().Where(x => x.id == id).ToList().ForEach(x =>
            {
                Product findproduct = db.Products.Find(id);
                if (findproduct != null)
                {
                    x.count = findproduct.count - count;
                    db.Update(x);
                    db.SaveChanges();
                }
            });
        }
        public void Update_Product_Count_Delete_Orderdetail(int id, int count)
        {
            Product newproduct = new Product();
            Dbproject db = new Dbproject();
            List<Product> productlist = db.Products.ToList().Where(x => x.id == id).ToList();
            if (productlist.Count == 0)
            {
                Console.WriteLine($"product with id {id} did not find");
            }
            db.Products.ToList().Where(x => x.id == id).ToList().ForEach(x =>
            {
                Product findproduct = db.Products.Find(id);
                if (findproduct != null)
                {
                    x.count = findproduct.count + count;
                    db.Update(x);
                    db.SaveChanges();
                }
            });
        }
        public void Update_City_Product_By_Name()
        {
            Product newproduct = new Product();
            Dbproject db = new Dbproject();
            newproduct.Show_Table();
            Console.WriteLine("enter name of city for update : ");
            string cityname = Console.ReadLine().ToLower();
            List<City> citylistexist = db.Cities.ToList().Where(x => x.name == cityname).ToList();
            if(citylistexist.Count == 0)
            {
                Console.WriteLine($"city with name '{cityname}' did not find in city table");
            }
            else
            {
                Console.WriteLine("enter name of product for update : ");
                string productname = Console.ReadLine().ToLower();
                List<Product> productlist = db.Products.ToList().Where(x => x.name == productname).ToList();
                if (productlist.Count == 0)
                {
                    Console.WriteLine($"product with name '{productname}' did not find in product table");
                }
                db.Cities.Include(x => x.products).ToList().Where(x => x.name == cityname).ToList().ForEach(x =>
                {
                    x.products.ToList().Where(y => y.name == productname).ToList().ForEach(y =>
                    {
                        Console.WriteLine("enter name of product for replace : ");
                        y.name = Console.ReadLine().ToLower();
                        try
                        {
                            Console.WriteLine("enter count of product for replace : ");
                            y.count = int.Parse(Console.ReadLine().ToLower());
                        }
                        catch
                        {
                            Console.WriteLine("you should enter integer!!!");
                            Console.WriteLine("enter count of product for replace again : ");
                            y.count = int.Parse(Console.ReadLine().ToLower());
                        }
                        try
                        {
                            Console.WriteLine("enter price of product for replace : ");
                            y.price = int.Parse(Console.ReadLine().ToLower());
                        }
                        catch
                        {
                            Console.WriteLine("you should enter integer!!!");
                            Console.WriteLine("enter price of product for replace again : ");
                            y.price = int.Parse(Console.ReadLine().ToLower());
                        }
                        db.Update(y);
                        Console.WriteLine($"product with name '{y.name}' count '{y.count}' price '{y.price}' from city '{x.name}' updated successfully");
                    });
                    db.SaveChanges();
                });
            }
        }
        public void Filter_Price()
        {
            Dbproject db = new Dbproject();
            Console.WriteLine("enter product price for filter : ");
            int price = int.Parse(Console.ReadLine());
            Console.WriteLine($"if you want to see products with price greater than {price} enter '>' \n" +
                              $"if you want to see products with price equal to {price} enter '=' \n" +
                              $"if you want to see products with price less than {price} enter '<' \n");
            Console.WriteLine("enter your choice : ");
            string sign = Console.ReadLine();
            if (sign == ">")
            {
                List<Product> products = db.Products.Include(x => x.City).ToList().Where(x => x.price > price).ToList();
                if(products.Count == 0)
                {
                    Console.WriteLine($"there is no product price greater than {price}");
                }
                products.ForEach(x =>Console.WriteLine($"id : {x.id} \t name : {x.name}    \t count : {x.count} \t price : {x.price} \t city : {x.City.name}"));
            }
            else if (sign == "=")
            {
                List<Product> products = db.Products.Include(x => x.City).ToList().Where(x => x.price == price).ToList();
                if(products.Count == 0)
                {
                    Console.WriteLine($"there is no product price equal to {price}");
                }
                products.ForEach(x => Console.WriteLine($"id : {x.id} \t name : {x.name}    \t count : {x.count} \t price : {x.price} \t city : {x.City.name}"));
            }
            else if (sign == "<")
            {
                List<Product> products = db.Products.Include(x => x.City).ToList().Where(x => x.price < price).ToList();
                if(products.Count == 0)
                {
                    Console.WriteLine($"there is no product price less than {price}");
                }
                products.ForEach(x => Console.WriteLine($"id : {x.id} \t name : {x.name}    \t count : {x.count} \t price : {x.price} \t city : {x.City.name}"));
            }
            else
            {
                Console.WriteLine("you enter wrong sign !!!");
            }
        }
    }
}
