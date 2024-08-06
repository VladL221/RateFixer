using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNewProject.src.Core.Models;

namespace MyNewProject.src.Core.Interfaces
{
    public interface IPolicySource
    {
        Policy GetPolicy();
    }
}
