using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billify.API.Common.Dtos
{
    public record ProductDto(int Id, string name, bool status);
}
