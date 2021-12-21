namespace Practice_9.Classes
{
    public class Role
    {
        public int ID;
        public string name;
        public int salary;

        public static Role ADMIN = new(0,100);
        public static Role HR = new(1,100);
        public static Role OPERATOR = new(2,100);
        public static Role ACCOUNTANT = new(3,100);
        public static Role MANAGER = new(4,100);
        public static Role CLIENT = new(5,0);
        

        public Role(int ID,int salary)
        {
            this.salary = salary;
            this.ID = ID;
            switch (ID)
            {
                case 0:
                    name = "ADMIN";
                    break;
                case 1:
                    name = "HR";
                    break;
                case 2:
                    name = "OPERATOR";
                    break;
                case 3:
                    name = "ACCOUNTANT";
                    break;
                case 4:
                    name = "MANAGER";
                    break;
                case 5:
                    name = "CLIENT";
                    break;
            }
        }
    }
}