using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestEx2
{
    class Program
    {
        public static string city;
        public static string answer;
        static void Main(string[] args)
        {
            Console.Write("Введите город: ");
            city = Console.ReadLine();
            try
            {
                ConnectAsync().Wait();
            }
            catch (Exception)
            {
                Console.WriteLine("Город не найден или отсутствует подключение");
            }
        }
        public static async Task ConnectAsync()
        {
            string textToFile = "";
            string path;
            DateTime UnixToDateTime(double unixTime)
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0);
                return origin.AddSeconds(unixTime);
            }
            WebRequest request = WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&&appid=94631e5b4efb8c67742789d8848178e1");
            request.Method = "POST";
            WebResponse response = await request.GetResponseAsync();
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            response.Close();

            WeatherResponce responce_city = JsonConvert.DeserializeObject<WeatherResponce>(answer);
            Console.WriteLine("Средняя температура: " + responce_city.main.temp + " C");
            Console.WriteLine("Влажность: " + responce_city.main.humidity + " %");
            Console.WriteLine("Восход солнца: " + UnixToDateTime(responce_city.sys.sunrise + responce_city.timezone).ToString("t"));
            Console.WriteLine("Закат солнца: " + UnixToDateTime(responce_city.sys.sunset + responce_city.timezone).ToString("t"));

            textToFile += responce_city.name + 
                "\n" + "Средняя температура: " + responce_city.main.temp + " C\n" + 
                "Влажность: " + responce_city.main.humidity + " %\n" + 
                "Восход солнца: " + UnixToDateTime(responce_city.sys.sunrise + responce_city.timezone).ToString("t") + 
                "\nЗакат солнца: " + UnixToDateTime(responce_city.sys.sunset + responce_city.timezone).ToString("t");
            path = @"C:\" + DateTime.Now.ToString("d") + ".txt";
            Console.WriteLine("Файл сохранен в: " +  path);
            File.WriteAllText(path, textToFile); 
        }
        
    }
}
