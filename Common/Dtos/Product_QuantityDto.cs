using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billify.API.Common.Dtos
{
    public record Product_QuantityDto
    (
        ProductDto product,
        int prd_quantity
        );
}
