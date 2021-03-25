using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, string guid) : base($"{name} '{guid}' not found")
        {
        }
    }
}
