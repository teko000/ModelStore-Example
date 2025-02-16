﻿using SnapObjects.Data;
using SnapObjects.Data.Oracle;

namespace Appeon.ModelStoreDemo.Oracle
{
    public class OrderContext : OracleDataContext
    {
        public OrderContext(string connectionString)
            : this(new OracleDataContextOptions<OrderContext>(connectionString))
        {
        }

        public OrderContext(IDataContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public OrderContext(IDataContextOptions options)
            : base(options)
        {
        }
    }
}
