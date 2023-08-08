namespace Rashidfard.Project.Models
{
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public ICollection<Product> products { get; set; }
        public void Add_City()
        {
            Dbproject db = new Dbproject();
            List<City> citylist = new List<City>();
            int counter = 1;
            while(true)
            {
                City city = new City();
                Console.WriteLine($"enter name of city {counter}th : ");
                city.name = Console.ReadLine().ToLower();
                Console.WriteLine("do you want to continue ?\nif you don't want to continue press 'n'");
                citylist.Add(city);
                string choice = Console.ReadLine().ToLower();
                if (choice == "n")
                    break;
                counter++;
            }
            foreach (City enteredcity in citylist)
            {
                List<City> findcity = db.Cities.ToList().Where(x => x.name.ToLower() == enteredcity.name).ToList();
                if (findcity.Count == 0)
                {
                    db.Add(enteredcity);
                    db.SaveChanges();
                    Console.WriteLine($"city with name '{enteredcity.name}' added successfully");
                }
                else
                { 
                    Console.WriteLine($"city with name '{enteredcity.name}' is exist in city table");
                }
            }
        }
        public void Show_Table()
        {
            Dbproject db = new Dbproject();
            List<City> citytable = db.Cities.ToList();
            if(citytable.Count == 0)
            {
                Console.WriteLine("there is no city in table");
            }
            db.Cities.ToList().ForEach(x => Console.WriteLine($"city id : {x.id} \t city name : {x.name}"));
        }
        public int Select_Id_By_Name(string name)
        {
            Dbproject db = new Dbproject();
            db.Cities.ToList().Where(x => x.name == name).ToList().ForEach(x =>
            {
                City findcity = db.Cities.Find(x.id);
                if (findcity != null)
                {
                    id = findcity.id;
                }
            });
            return id;
        }
        public void Search_By_Name()
        {
            Dbproject db = new Dbproject();
            Console.WriteLine("enter name of city for select : ");
            string name = Console.ReadLine().ToLower();
            List<City> findcity = db.Cities.ToList().Where(x => x.name.ToLower() == name).ToList();
            if(findcity.Count == 0)
            {
                Console.WriteLine($"city with name '{name}' did not find in city table");
            }
            else
            {
                db.Cities.ToList().Where(x => x.name.ToLower() == name).ToList().ForEach(x => Console.WriteLine($"city id : {x.id} \t city name : {x.name}"));
            }
        }
        public void Delete_By_Name()
        {
            City cities = new City();
            Dbproject db = new Dbproject();
            cities.Show_Table();
            Console.WriteLine("enter name of city for delete : ");
            string name = Console.ReadLine().ToLower();
            List<City> findcity = db.Cities.ToList().Where(x => x.name.ToLower() == name).ToList();
            if(findcity.Count == 0)
            {
                Console.WriteLine($"city with name '{name}' did not find in city table");
            }
            else
            {
                db.Cities.ToList().Where(x => x.name.ToLower() == name).ToList().ForEach(x =>
                {
                    City findcityid = db.Cities.Find(x.id);
                    if (findcityid != null)
                    {
                        db.Remove(findcityid);
                        db.SaveChanges();
                        Console.WriteLine($"city with name '{name}' deleted successfully");
                    }
                });
            }
        }
        public void Update_By_Name()
        {
            City cities = new City();
            Dbproject db = new Dbproject();
            cities.Show_Table();
            Console.WriteLine("enter name of city for update : ");
            string name = Console.ReadLine().ToLower();
            List<City> findcity = db.Cities.ToList().Where(x => x.name.ToLower() == name).ToList();
            if (findcity.Count == 0)
            {
                Console.WriteLine($"city with name '{name}' did not find in city table");
            }
            else
            {
                db.Cities.ToList().Where(x => x.name.ToLower() == name).ToList().ForEach(x =>
                {
                    City findcityid = db.Cities.Find(x.id);
                    if (findcityid != null)
                    {
                        Console.WriteLine("enter name of city for replace : ");
                        x.name = Console.ReadLine().ToLower();
                        db.Update(x);
                        db.SaveChanges();
                        Console.WriteLine($"city name : '{name}' change to '{x.name}'");
                    }
                });
            }
        }
    }
}
