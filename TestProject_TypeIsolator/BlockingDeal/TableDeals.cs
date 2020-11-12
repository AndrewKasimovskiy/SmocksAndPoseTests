using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockingDeal
{
    [EdmEntityType (NamespaceName ="SomeNamespace", Name = "TableDeals")]
    public class TableDeals
    {
        [EdmScalarProperty (EntityKeyProperty = true, IsNullable = false)]
        public int Id { get; set; }
        [EdmScalarProperty (EntityKeyProperty = false, IsNullable = false)]
        public string Name { get; set; }
        [EdmScalarProperty (EntityKeyProperty = false, IsNullable = false)]
        public string OtherBlocks { get; set; }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
        public DateTime DateTimeNow { get; set; }
    }
}
