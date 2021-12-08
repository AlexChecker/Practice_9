namespace Practice_9.Classes
{
    public class Role
    {
        public int ID;
        public string name;

        public static Role ADMIN = new(0);
        public static Role HR = new(1);
        public static Role OPERATOR = new(2);
        public static Role ACCOUNTANT = new(3);
        public static Role MANAGER = new(4);
        public static Role CLIENT = new(5);
        

        public Role(int ID)
        {
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