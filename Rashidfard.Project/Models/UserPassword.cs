namespace Rashidfard.Project.Models
{
    public class UserPassword
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public void Add_Username_Password()
        {
            Dbproject db = new Dbproject();
            List<UserPassword> userpasslist = db.userPassword.ToList();
            if (userpasslist.Count == 0)
            {
                UserPassword userPassword = new UserPassword();
                userPassword.username = Make_Username();
                userPassword.password = Make_Password();
                db.Add(userPassword);
                db.SaveChanges();
            }
            else 
            {
                Console.WriteLine("username and password added before");
            }
        }
        public void Update_Username_Password()
        {
            Dbproject db = new Dbproject();
            UserPassword usernamepass = new UserPassword();
            Console.WriteLine("enter old username : ");
            string oldusername = Console.ReadLine();
            Console.WriteLine("enter old password : ");
            string oldpassword = usernamepass.GetPassword();
            Console.WriteLine();
            List<UserPassword> userpasslist = db.userPassword.ToList().Where(x => x.username == oldusername && x.password == oldpassword).ToList();
            if(userpasslist.Count == 0)
            {
                Console.WriteLine("old username and password is wrong !!!");
            }
            else
            {
                db.userPassword.ToList().Where(x => x.username == oldusername && x.password == oldpassword).ToList().ForEach(x =>
                {
                    UserPassword finduserpass = db.userPassword.Find(x.id);
                    if (finduserpass != null)
                    {
                        x.username = Make_Username();
                        x.password = Make_Password();
                        db.Update(x);
                        db.SaveChanges();
                    }
                });
            }
        }
        public void Delete_Username_Password()
        {
            Dbproject db = new Dbproject();
            db.userPassword.ToList().ForEach(x =>
            {
                UserPassword finduserpass = db.userPassword.Find(id);
                if(finduserpass != null)
                {
                    db.Remove(finduserpass);
                    db.SaveChanges();
                }
            });
        }

        private string Make_Username()
        {
            Console.WriteLine("please enter username : ");
            string username = Console.ReadLine();
            return username;
        }
        private string Make_Password()
        {
            Console.WriteLine($"your password should have at least 8 character \n"+
                              $"your password should have upper case character \n"+
                              $"your password should have lower case character \n"+
                              $"your password should have at least a number \n"+
                              $"your password should have at least a sign");
            string pass;
            while (true)
            {
                UserPassword userPassword = new UserPassword();
                Console.WriteLine("please enter your password : ");
                string password = userPassword.GetPassword();
                Console.WriteLine();
                int point = 0;
                int up_password = upper_case(password);
                int low_password = lower_case(password);
                int num_case = number_case(password);
                int sgn_case = sign_case(password);
                if (password.Length >= 8)
                    point++;
                else
                    Console.WriteLine("you should enter at least 8 character");
                if (up_password == 1)
                    point++;
                else
                    Console.WriteLine("you should enter upper case character");
                if (low_password == 1)
                    point++;
                else
                    Console.WriteLine("you should enter lower case character");
                if (num_case == 1)
                    point++;
                else
                    Console.WriteLine("you should enter at least a number");
                if (sgn_case == 1)
                    point++;
                else
                    Console.WriteLine("you should enter at least a sign");
                if (point == 5)
                {
                    Console.WriteLine("Good password");
                    pass = password;
                    break;
                }
            }
            return pass;
        }
        private int upper_case(string pass)
        {
            List<string> numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "@", "~", "#", "$", "%", "^", "&", "!", "*", "+" };
            string name = pass;

            foreach (string i in numbers)
            {
                name = name.Replace(i, string.Empty);
            }

            string upper = name.ToUpper();
            int point = 0;
            for (int i = 0; i < upper.Length; i++)
            {
                if (upper[i] == name[i])
                {
                    point++;
                }
            }
            if (point != 0)
                return 1;
            else
                return 0;
        }
        private int lower_case(string pass)
        {
            List<string> numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "@", "~", "#", "$", "%", "^", "&", "!", "*", "+" };
            string name = pass;

            foreach (string i in numbers)
            {
                name = name.Replace(i, string.Empty);
            }

            string lower = name.ToLower();
            int point = 0;
            for (int i = 0; i < lower.Length; i++)
            {
                if (lower[i] == name[i])
                {
                    point++;
                }
            }
            if (point != 0)
                return 1;
            else
                return 0;
        }
        private int number_case(string pass)
        {
            int point = 0;
            List<char> numbers = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < numbers.Count; i++)
                for (int j = 0; j < pass.Length; j++)
                    if (numbers[i] == pass[j])
                        point++;
            if (point != 0)
                return 1;
            else
                return 0;
        }
        private int sign_case(string pass)
        {
            int point = 0;
            List<char> numbers = new List<char> { '@', '~', '#', '$', '%', '^', '&', '!', '*', '+' };
            for (int i = 0; i < numbers.Count; i++)
                for (int j = 0; j < pass.Length; j++)
                    if (numbers[i] == pass[j])
                        point++;
            if (point != 0)
                return 1;
            else
                return 0;
        }
        private string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }
        public bool Admin_User_Password()
        {
            UserPassword userPassword1 = new UserPassword();
            userPassword1.Add_Username_Password();
            Dbproject db = new Dbproject();
            List<UserPassword> userpass = db.userPassword.ToList();
            string username = "";
            string password = "";
            foreach (UserPassword userPassword in userpass)
            {
                username = userPassword.username.ToString();
                password = userPassword.password.ToString();
            }
            Console.WriteLine("username : ");
            string enterd_username = Console.ReadLine();
            Console.WriteLine("password : ");
            string enterd_password = userPassword1.GetPassword();
            if (username == enterd_username && password == enterd_password)
            {
                return true;
            }
            return false;
        }
    }
}
