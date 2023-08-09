namespace B1_Task1
{
    public class TextService
    {
        static DateTime startDate = new DateTime(2018, 08, 09);

        int range = (DateTime.Today - startDate).Days;

        readonly double _minDouble = 1;

        readonly double _maxDouble = 20;
        
        const string engChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        
        const string ruChars = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        
        public static Random rnd = new Random();


        DateTime GetRandomDate() 
            => startDate.AddDays(rnd.Next(range));

        int GetRandomInt()
            => 2 * rnd.Next(1, 50000001);

        double GetRandomDouble()
            => Math.Round(_minDouble + (rnd.NextDouble() * (_maxDouble - _minDouble)), 8);

        string GetRandomString(string charList)
        {
            string s = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                s += charList[rnd.Next(charList.Length)];
            }

            return s;
        }

        public override string ToString()
            => GetRandomDate().ToString("d") + "||" + GetRandomString(engChars) + "||" + GetRandomString(ruChars) + "||" + GetRandomInt().ToString() + "||" + GetRandomDouble().ToString() + "||";
    }
}
