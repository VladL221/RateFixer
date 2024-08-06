using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRating;

namespace MyNewProject
{
    public interface IRatingStrategy
    {
        decimal Rate(Policy policy);
    }
}
