using System;
using System.Collections.Generic;
using System.Text;

namespace Test_App
{
    public class Prime
    {
        public static bool SqrtMethod(int n)
        {
            if (n <= 1) 
                return false;

            bool res = true;
            for(var i = 2; i * i <= n && res; i++)
            {
                res = n % i != 0;
            }
            return res;
        }
    }
}
