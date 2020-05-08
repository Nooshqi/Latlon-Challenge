using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class ConsolePrinter
    {
        public void Print(string value){
            Console.WriteLine('\n'+value);
        }
        public void Print(string[] values){
            foreach(string value in values){
                Console.WriteLine('\n'+value);
            }
        }
    }
}
