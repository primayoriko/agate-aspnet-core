using System;
using System.Collections.Generic;
using System.Text;

namespace Test_App
{
    public class Sort
    {
        public static List<int> BubbleSortInt(List<int> data)
        {
            List<int> res = new List<int>(data);
            for(var i = 0; i < res.Count - 1; i++)
            {
                for(var j = 0; j < res.Count - i - 1; j++)
                {
                    if(res[j] > res[j + 1])
                    {
                        var temp = res[j];
                        res[j] = res[j + 1];
                        res[j + 1] = temp;
                    }
                }
            }
            return res;
        }
    }
}
