namespace Practice_9.Classes.Panels
{
    public class Category
    {
        public static Category JACKETS = new(0);
        public static Category ACCESSOIRES = new(1);
        public static Category ACCESSORIES_FIT = new(2);
        public static Category BOOTS = new(3);
        public static Category SNEAKERS = new(4);
        public static Category HOODIES = new(5);
        public static Category OTHER = new(6);

        public int ID;
        //{
        //    set
        //    {
        //        switch (value)
        //        {
        //            case 0:
        //                name = "Jackets";
        //                break;
        //            case 1:
        //                name = "Accesoires";
        //                break;
        //            case 2:
        //                name = "Fitness accesoires";
        //                break;
        //            case 3:
        //                name = "Boots";
        //                break;
        //            case 4:
        //                name = "Sneakers";
        //                break;
        //            case 5:
        //                name = "Hoodies";
        //                break;
        //            default:
        //                name = "Other";
        //                break;
        //        }
        //    }
        //}

        public string name;

        public Category(int ID)
        {
            this.ID = ID;
            switch (ID)
            {
                case 0:
                    name = "JACKETS";
                    break;
                case 1:
                    name = "ACCESSORIES";
                    break;
                case 2:
                    name = "FITNESS_ACCESSORIES";
                    break;
                case 3:
                    name = "BOOTS";
                    break;
                case 4:
                    name = "SNEAKERS";
                    break;
                case 5:
                    name = "HOODIES";
                    break;
                default:
                    name = "OTHER";
                    break;
            }
        }
    }
}