using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketManager.Gateway
{
    public class Class
    {
        public Guid Id { get; set; }

        public Class(Guid guid)
        {
            Id = guid;
        }
    }
}
