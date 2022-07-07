using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fallout4Hacker
{
    public class Password
    {
       private readonly string pass;

       public int? Likeness { get; set; }

       public Password(string pass)
       {
          this.pass = pass;
       }

       public override string ToString()
       {
          return pass;
       }

    }
}
