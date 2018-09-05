using System;
using Microsoft.SqlServer.Dac.Compare;

namespace SchemaCompare.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var target = new SchemaCompareDatabaseEndpoint(@"Data Source=localhost;Initial Catalog=Evolution;Integrated Security=True;Connect Timeout=30");
            var source = new SchemaCompareDatabaseEndpoint(@"Data Source=localhost;Initial Catalog=qaDrmDevTrunk;Integrated Security=True;Connect Timeout=30");
            var compare = new SchemaComparison(source, target);

            var compRes = compare.Compare();
            var script = compRes.GenerateScript("Evolution");
            

            Console.Read();
        }
    }
}
