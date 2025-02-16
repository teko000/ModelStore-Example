﻿using SnapObjects.Data;
using SnapObjects.Data.Odbc;

namespace Appeon.ModelStoreDemo.SQLAnywhere
{
    public class OrderContext : OdbcDataContext
    {
        public OrderContext(string connectionString)
            : this(new OdbcDataContextOptions<OrderContext>(connectionString))
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
