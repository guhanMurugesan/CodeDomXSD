using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenXSD
{
    class Program
    {
        static void Main(string[] args)
        {
            Processor.GenerateCode(Processor.Process());
            Console.ReadLine();
        }
    }
}
