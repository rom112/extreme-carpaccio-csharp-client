using System.IO;
using System.Text;
using System.Threading;

namespace xCarpaccio.client
{
    using Nancy;
    using System;
    using Nancy.ModelBinding;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => "It works !!! You need to register your server on main server.";

            Post["/order"] = _ =>
            {
                using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    Console.WriteLine("Order received: {0}", reader.ReadToEnd());
                }

                var order = this.Bind<Order>();
                Bill bill = new Bill();
                bill.total = 0;
                int compteurQuantities = order.Quantities.Length;
                int compteurPrices = order.Prices.Length;

                if (compteurQuantities == compteurPrices)
                {
                    for (int i = 0; i <= compteurQuantities - 1; i++)
                    {
                        bill.total = bill.total + (order.Quantities[i]*order.Prices[i]);
                    }
                }

                //TODO: do something with order and return a bill if possible
                // If you manage to get the result, return a Bill object (JSON serialization is done automagically)
                // Else return a HTTP 404 error : return Negotiate.WithStatusCode(HttpStatusCode.NotFound);

                decimal taxes = 0;

                if (order.Country == "DE" || order.Country == "FR" || order.Country == "RO" || order.Country == "NL" || order.Country == "EL" || order.Country == "LV" || order.Country == "MT")
                {
                    taxes = Convert.ToDecimal(1.2);
                }

                if (order.Country == "UK" || order.Country == "PL" || order.Country == "BG" || order.Country == "DK" || order.Country == "IE" || order.Country == "CY")
                {
                    taxes = Convert.ToDecimal(1.21);
                }

                if (order.Country == "IT" || order.Country == "LU")
                {
                    taxes = Convert.ToDecimal(1.25);
                }

                if (order.Country == "ES" || order.Country == "CZ")
                {
                    taxes = Convert.ToDecimal(1.19);
                }

                if (order.Country == "BE" || order.Country == "SI")
                {
                    taxes = Convert.ToDecimal(1.24);
                }

                if (order.Country == "PT" || order.Country == "SE" || order.Country == "HR" || order.Country == "LT")
                {
                    taxes = Convert.ToDecimal(1.23);
                }

                if (order.Country == "AT" || order.Country == "EE")
                {
                    taxes = Convert.ToDecimal(1.22);
                }

                if (order.Country == "HU")
                {
                    taxes = Convert.ToDecimal(1.27);
                }

                if (order.Country == "SK")
                {
                    taxes = Convert.ToDecimal(1.18);
                }

                if (order.Country == "FI")
                {
                    taxes = Convert.ToDecimal(1.17);
                }

                if (taxes != 0)
                {
                    bill.total = bill.total*taxes;

                    if (order.Reduction != "PAY THE PRICE")
                    {
                        if (bill.total >= 50000)
                        {
                            bill.total = bill.total - (bill.total*Convert.ToDecimal(0.15));
                        }

                        if (bill.total >= 10000 && bill.total < 50000)
                        {
                            bill.total = bill.total - (bill.total*Convert.ToDecimal(0.10));
                        }

                        if (bill.total >= 7000 && bill.total < 10000)
                        {
                            bill.total = bill.total - (bill.total*Convert.ToDecimal(0.07));
                        }

                        if (bill.total >= 5000 && bill.total < 7000)
                        {
                            bill.total = bill.total - (bill.total*Convert.ToDecimal(0.05));
                        }

                        if (bill.total >= 1000 && bill.total < 5000)
                        {
                            bill.total = bill.total - (bill.total*Convert.ToDecimal(0.03));
                        }
                    }
                    return bill;
                }

                else
                {
                    bill = null;
                    return bill;
                }
            };

            Post["/feedback"] = _ =>
            {
                var feedback = this.Bind<Feedback>();
                Console.Write("Type: {0}: ", feedback.Type);
                Console.WriteLine(feedback.Content);
                return Negotiate.WithStatusCode(HttpStatusCode.OK);
            };
        }
    }
}