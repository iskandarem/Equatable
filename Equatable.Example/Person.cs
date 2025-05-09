using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equatable;
namespace Equatable.Example
{
    class Person : BaseEquatable
    {
        public string? Name { get; set; }
        public int Age { get; set; }

        public override List<object?> Props => [Name, Age];

        public override bool? Stringify => true;
    }
}
