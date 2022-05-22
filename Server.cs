using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject5
{
    public class Server
    {
        public Dictionary<Cook, TableRequests> service { get; private set; }
        public int CustomerCount { get; set; }

        public Server(int n = 2)
        {
            service = new Dictionary<Cook, TableRequests>();
            for (int i = 0; i < n; i++)
                service.Add(new Cook(), new TableRequests());
        }

        public void Receive(string customerName, int quantityChiken, int quantityEgg, string drink)
        {
            CustomerCount++;
            TableRequests requests = service.Values.First(x => !x.InProccess);

            for (int i = 0; i < quantityChiken; i++)
            {
                requests.Add<Chicken>(customerName);
            }
            for (int i = 0; i < quantityEgg; i++)
            {
                requests.Add<Egg>(customerName);
            }
            GetMenuDrink(ref requests, drink, customerName);
        }

        public async Task Send(Task t)
        {
            TableRequests requests = service.Values.First(x => x.ToList().Any());
            Cook cook = service.Keys.First(x => x.InProccess);
            cook.InProccess = true;
            cook.Process(requests);
            cook.InProccess = false;
            requests.InProccess = false;
            await t;
        }

        public async Task<(List<string>, string)> Serve(Task t)
        {
            List<string> result = new List<string>();
            TableRequests requests = service.Values.First(x => x.Any() && x.InProccess == false);


            foreach (var e in requests.OrderBy(x => x))
            {
                var customer = e.ToString();
                string text = "Customer " + customer + " is served ";

                var customerOrders = requests[customer];

                int chickenCount = customerOrders.Where(x => x.GetType() == typeof(Chicken)).Count();
                int eggCount = customerOrders.Where(x => x.GetType() == typeof(Egg)).Count();

                if (chickenCount > 0)
                {
                    text += " Chicken " + chickenCount;
                }
                if (eggCount > 0)
                {
                    text += ", Egg " + eggCount;
                }

                var d = customerOrders.Where(x =>
                    x.GetType() == typeof(RSCola) ||
                    x.GetType() == typeof(Lemonad) ||
                    x.GetType() == typeof(Tea) ||
                    x.GetType() == typeof(Coca_Cola) ||
                    x.GetType() == typeof(NotDrink)
                    ).FirstOrDefault();
                string drink = (d != null) ? d.GetType().Name : " ";

                if (!string.IsNullOrEmpty(drink))
                {
                    text += ", " + drink;
                }
                result.Add(text);
            }
            if (result.Any())
            {
                result.Add("Please enjoy your meal!");
            }
            else
            {
                result.Add("Please send all orders to cooker!");
            }
            service.Remove(service.Keys.ToList()[service.Values.ToList().IndexOf(requests)]);


            service.Add(new Cook(), new TableRequests());

            CustomerCount = 0;
            await t;
            return (result, Egg.quality.ToString());

        }
        private void GetMenuDrink(ref TableRequests requests, string str, string customerName)
        {
            switch (str)
            {
                case "RC Cola":
                    requests.Add<RSCola>(customerName);
                    return;
                case "Lemonad":
                    requests.Add<Lemonad>(customerName);
                    return;
                case "Tea":
                    requests.Add<Tea>(customerName);
                    return;
                case "Coca-Cola":
                    requests.Add<Coca_Cola>(customerName);
                    return;
            }
            requests.Add<NotDrink>(customerName);
        }
    }
}