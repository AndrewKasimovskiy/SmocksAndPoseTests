using System;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace BlockingDeal
{
    public class BlockDeal
    {
        public TableDeals TableDeal;
        IEfContext efContext;
        public BlockDeal(IEfContext efContext)
        {
            this.efContext = efContext;
        }

        public DateTime BlockingOneDeal(int id)
        {
            TableDeal = efContext.GetDeal(id);

            TableDeal.DateTimeNow = DateTime.Now;

            return TableDeal.DateTimeNow;
        }
    }

    public interface IEfContext
    {
        TableDeals GetDeal(int id);
    }

    public class EfContext : IEfContext
    {
        ObjectContext objectContext;

        public EfContext(string connectionString)
        {
            objectContext = new ObjectContext(connectionString);
            objectContext.DefaultContainerName = "SomeNamespace";
        }

        public TableDeals GetDeal(int id)
        {
            var deals = objectContext.CreateObjectSet<TableDeals>();
            return deals.Single(d => d.Id == id);
        }
    }
}
