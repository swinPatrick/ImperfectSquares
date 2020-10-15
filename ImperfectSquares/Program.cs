using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ImperfectSquares
{
    class Program
    {
        static void Main(string[] args)
        {
            int StartingNumber = 2400, FinalNumber = 2600;
            NumberDefinition nd = new NumberDefinition();
            
            Console.WriteLine("Welcome to Patricks' number definer!");
            for(int i = StartingNumber; i <= FinalNumber; i++)
            {
                Console.WriteLine(nd.GetNumberResult(i));
            }
            
            //Console.WriteLine(nd.NumberOfFactors(24));

            Console.ReadLine();
        }

        
    }
}
