﻿using SnapObjects.Data;
using SnapObjects.Data.PostgreSql;

namespace Appeon.ModelStoreDemo.PostgreSQL
{
    public class OrderContext : PostgreSqlDataContext
    {
        public OrderContext(string connectionString)
            : this(new PostgreSqlDataContextOptions<OrderContext>(connectionString))
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
