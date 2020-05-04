using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consultas
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Student> studentList = new List<Student>()
        {
            new Student() { StudentID = 1, StudentName = "John", Age = 13, StandardID =1 },
            new Student() { StudentID = 2, StudentName = "Moin",  Age = 21, StandardID =3 },
            new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, StandardID =2 },
            new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, StandardID =2 },
            new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 }
        };
            //Where sin ToList()
            Console.WriteLine("Where \n-------");
            var objWhere = studentList.Where(filter => filter.Age == 15);
            Console.WriteLine(objWhere);

            Console.WriteLine("Select \n-------");
            var objSelect = objWhere.Select(filter => filter.Age == 15).ToList();
            Console.WriteLine(objWhere);

            var objWhereConlist = studentList.Where(filter => filter.Age == 15).ToList();
            Console.WriteLine(objWhereConlist);

            /* CREANDO UN NUEVO USUARIO FILTRANDO Y CON SELECT */
            var selectobjet = studentList
                               .Where(filter => filter.Age == 15)
                               .Select(filter => new Student { StudentID = 6, StudentName = "Carlos", Age = 26 }).ToList();

            Console.WriteLine(string.Concat($"El numero de estudiante es ", $"{selectobjet[0].StudentID}", $" Su nombre es ", $"{selectobjet[0].StudentName}"));

            /* CREANDO UN NUEVO USUARIO FILTRANDO Y CON SELECT */
            var onlySelectTolist = studentList
                               .Where(filter => filter.Age == 15)
                               .Select(filter => new { filter.StudentName, filter.StandardID }).ToList();

            /* FIRSTORDEFAULT*/
            var onlySelectFirst = studentList
                               .Where(filter => filter.Age == 15)
                               .Select(filter => new { filter.StudentName, filter.StandardID }).FirstOrDefault();

            Console.WriteLine(onlySelectTolist[0].StandardID); //devuelve un listado y se puede recorrer
            Console.WriteLine(onlySelectFirst.StandardID); //Solo devuelve uno

            //All Trae un booleano y todo se tiene que cumplir
            Console.WriteLine("All \n-------");
            var objAll = studentList.All(student => student.Age > 10 && student.Age < 18);
            Console.WriteLine(objAll);

            //Any Uno en especifico
            Console.WriteLine("\nAny \n-------");
            var objAny = studentList.Any(student => student.Age == 14);
            Console.WriteLine(objAny);

            //Si contiene
            Console.WriteLine("\nContains - check value \n-------");
            IList<int> intList = new List<int>() { 10, 2, 3, 4, 5 };
            bool resultContains = intList.Contains(10);
            Console.WriteLine(resultContains);

            Console.WriteLine("\nContains - check in collection \n-------");
            Student std = new Student() { StudentID = 3, StudentName = "Bill", StandardID = 2 };

            bool resultObj = studentList.Contains(std); //returns false
            Console.WriteLine(resultObj);

            bool resultComparer = studentList.Contains(std, new StudentComparer());
            Console.WriteLine(resultComparer);

            Console.WriteLine("C# For Loop");
            int number = 10;
            for (int count = 0; count < number; count++)
            {
                //Thread.CurrentThread.ManagedThreadId returns an integer that 
                //represents a unique identifier for the current managed thread.
                Console.WriteLine($"value of count = {count}, thread = {Thread.CurrentThread.ManagedThreadId}");
                //Sleep the loop for 10 miliseconds
                Thread.Sleep(10);
            }
            Console.WriteLine();

            Console.WriteLine("Parallel For Loop");
            Parallel.For(0, number, count =>
            {
                Console.WriteLine($"value of count = {count}, thread = {Thread.CurrentThread.ManagedThreadId}");
                //Sleep the loop for 10 miliseconds
                Thread.Sleep(10);
            });


            //Segundo ejemplo del Parallel For Loop
            DateTime StartDateTime = DateTime.Now;
            Console.WriteLine(@"Parallel For Loop Execution start at : {0}", StartDateTime);
            Parallel.For(0, 10, i =>
            {
                long total = DoSomeIndependentTask();
                Console.WriteLine("{0} - {1}", i, total);
            });

            DateTime EndDateTime = DateTime.Now;
            Console.WriteLine(@"Parallel For Loop Execution end at : {0}", EndDateTime);
            TimeSpan span = EndDateTime - StartDateTime;
            int ms = (int)span.TotalMilliseconds;
            Console.WriteLine(@"Time Taken to Execute the Loop in miliseconds {0}", ms);
            Console.WriteLine("Press any key to exist");
            Console.ReadLine();
        }

        static long DoSomeIndependentTask()
        {
            //Do Some Time Consuming Task here
            //Most Probably some calculation or DB related activity
            long total = 0;
            for (int i = 1; i < 100000000; i++)
            {
                total += i;
            }
            return total;
        }
    }
}
