using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


static class sayHi
{
    public static string on(bool withName)
    {
        string hi = "hi";
        if (withName)
        {
            hi = hi + ", Jinx";
        }
        hi = hi + "!";
        return hi;
    }
}

