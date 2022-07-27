using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupJoin
{
    public class Program
    {
        static void Main(string[] args)
        {
            var del = new List<Installment>()
            {
                new Installment
                {
                    Id =1,
                    ExternalNumber =1,
                    Date = "2022-07-01",
                    Value = 1
                },
                new Installment
                {
                    Id =2,
                    ExternalNumber =2,
                    Date = "2022-07-01",
                    Value = 2
                },
                new Installment
                {
                    Id =3,
                    ExternalNumber =3,
                    Date = "2022-07-01",
                    Value = 3
                },
                new Installment
                {
                    Id =4,
                    ExternalNumber =4,
                    Date = "2022-07-01",
                    Value = 4
                }
            };
            var gen = new List<Installment>()
            {
                new Installment
                {
                    Id =1,
                    ExternalNumber =1,
                    Date = "2022-07-01",
                    Value = 1
                },
                new Installment
                {
                    Id =2,
                    ExternalNumber =2,
                    Date = "2022-07-01",
                    Value = 2
                },
                new Installment
                {
                    Id =5,
                    ExternalNumber =6,
                    Date = "2022-07-01",
                    Value = 5
                },
                new Installment
                {
                    Id =6,
                    ExternalNumber =5,
                    Date = "2022-07-01",
                    Value = 6
                }
            };



            var query = gen.GroupJoin(
                del,
                gen => gen.ExternalNumber,
                del => del.ExternalNumber,
                (gen, del) => new { gen, del })
                .SelectMany(
                x => x.del.DefaultIfEmpty(),
                (x, y) => new
                {
                    x.gen,
                    del = y
                });


            query.ToList().ForEach(x =>
            {
                Console.WriteLine(x.gen.ToString() + " : " + x.del?.ToString());
            });

            query.Where(x => x.del == null).Select(x => x.gen).ToList().ForEach(x =>
            {
                Console.WriteLine(x.ToString());
            });


            //var t = query.Where(x => x.y == null).Select(x => x);
            //t.ToList().ForEach(x => Console.WriteLine(x.d.ToString()));


            //var result = d.GroupJoin(
            //    g,
            //    d => d.ExternalNumber,
            //    g => g.ExternalNumber,
            //    (d, g) => new
            //    {
            //        d,
            //        g
            //    }).SelectMany(
            //     x => x.g.DefaultIfEmpty(),
            //     (x, y) => new
            //     {
            //         x.g,
            //         x.d,
            //         y
            //     }).ToList();


            //result.ForEach(x =>
            //{
            //    Console.WriteLine(x.d.ToString() + " : "+ x.y?.ToString() + " : " + x.d?.ToString());
            //});

            //var t = result.Where(x => x.y == null).Select(x => x);

            //t.ToList().ForEach(x => Console.WriteLine(x.d.ToString()));
        }
    }

    internal class Installment
    {
        public int Id { get; set; }
        public int ExternalNumber { get; set; }
        public string Date { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return $"Id: {this.Id}, ExternalNumber: {this.ExternalNumber}, Date: {this.Date}, Value: {this.Value},";
        }
    }
}
