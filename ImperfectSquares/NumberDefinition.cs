using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using System.Linq;

namespace ImperfectSquares
{
    class NumberDefinition
    {
        private List<int> PRIMES = new List<int>();
        public NumberDefinition()
        {
            StreamReader sr = new StreamReader(file_name);
            while (!sr.EndOfStream)
            {
                PRIMES.Add(int.Parse(sr.ReadLine()));
            }
            sr.Close();
        }
        private static string file_name = "primes-to-100k.txt";

        // Returns a string describing each number input in terms of being a)perfect square b)imperfect square c)prime or d)sum of all factors
        public string GetNumberResult(int _number)
        {
            if (IsASquare(_number))
                return _number + " = " +Math.Sqrt(_number).ToString() + "^2";
            // to Find if a number can be m^2 - n^2 then m^2 is NextSquare and n^2 is the difference between current number and m^2
            int abs_Diff = Math.Abs(FindNextSquare(_number) - _number);
            if (IsASquare(abs_Diff))
                return _number + " = " + Math.Sqrt(FindNextSquare(_number)).ToString() + "^2 - " + Math.Sqrt(abs_Diff).ToString() + "^2";
            if (IsPrime(_number))
                return _number + " is a prime number";
            string output = _number + " has " + NumberOfFactors(_number) + " factors. They sum to " + GetFactors(_number).Sum();
            return output;
        }

        //returns the next integer that is a perfect square
        private int FindNextSquare(int _original_number)
        {
            //When rooting a non-square you'll end up with a non-whole number between the previous square and the next square.
            // so, if we get the next whole integer and quare it, that will be the next square value.
            double result = Math.Sqrt(_original_number);
            result = Math.Ceiling(result);
            return (int)Math.Pow(result, 2);
        }

        // returns true if the number is a square
        private bool IsASquare(int _number)
        {
            // if the remainder after dividing the sqrt into 1 is none, then we know that the original number was a square
            return (Math.Sqrt(_number) % 1 == 0.00);
        }

        private bool IsPrime(int _number)
        {
            // If the original number can divide into any prime number then it is not prime, 
            // we only need to check prime numbers up to the originals numbers root value.
            // it won't identify itself as a prime since we're comparing up to the root of the prime and not the prime itself.

            int this_prime = 0;
            for (int i = 0;  this_prime < Math.Sqrt(FindNextSquare(_number)); i++)
            {
                this_prime = PRIMES[i];
                if ((double)_number % this_prime == 0)
                    return false;
            }
            return true;
        }

        // returns the number of factors a given integer has
        public int NumberOfFactors(int _number)
        {
            List<int> factors = new List<int>();
            factors = GetPrimeFactors(_number);
            List<int> repeating_primes;
            repeating_primes = new List<int>();
            // 1. how many time each prime repeats in the list + 1
            while(factors.Count > 0)
            {
                repeating_primes.Add(factors.FindAll(x => x == factors[0]).Count + 1);
                factors.RemoveAll(x => x == factors[0]);
            }

            // 2. multiply to find the total number of factors of the number
            int result = 1;
            repeating_primes.ForEach(x => result *= x);
            return result;
        }
        // returns the list of all factors of _number
        private List<int> GetFactors(int _number)
        {
            List<int> prime_factors = new List<int>();
            //prime_factors.Clear();
            prime_factors = GetPrimeFactors(_number);
            List<int> factors = new List<int>();
            //factors.Clear();
            // Iterate through binary equivelant to calculate all posible products
            int possibilities = (int)Math.Pow(2, prime_factors.Count);
            string items;
            int product;
            // for each possibility we want to multiply indexes with 1 together to find the product
            // each factor is made up from multiplying the numbers prime factors by each other
            for(int i = 0; i <= possibilities; i++)
            {
                // creat the binary list:
                items = Convert.ToString(i, 2).PadLeft(prime_factors.Count, '0');
                product = 1;
                // for each character c in the binary string items
                for (int c = 0; c < items.Length; c++)
                    if (items[c] == '1')
                        product *= prime_factors[c];
                factors.Add(product);
            }
            // tidy up the result:
            factors = factors.Distinct().ToList();
            factors.Sort();
            return factors;
        }

        // returns a list of all prime factors that make up _number (includes duplicates)
        private List<int> GetPrimeFactors(int _number)
        {
            // to find how many factors a number has, add 1 to the exponent of each 
            // prime factor that makes up the number and then multiply them together
            int this_prime;
            int result = _number;
            List<int> factors = new List<int>();
            int prime_index;
            for (prime_index = 0; prime_index < PRIMES.Count; prime_index++)
            {
                this_prime = PRIMES[prime_index];
                if ((double)result % this_prime == 0)
                {
                    result /= this_prime;
                    factors.Add(this_prime);
                    prime_index = -1; //begin from lowest prime again
                }
                if (IsPrime(result))
                    break;
            }
            factors.Add(result); // the final value will also be a factor
            factors.Sort();
            return factors;
        }
    }

}
