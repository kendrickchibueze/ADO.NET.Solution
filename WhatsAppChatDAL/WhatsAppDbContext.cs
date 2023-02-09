using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppChatDAL
{
    public  class WhatsAppDbContext : IDisposable
    {

        private readonly string _connString;


        private bool _disposed;

        private SqlConnection _dbConnection = null;


        public WhatsAppDbContext():this(@"Data Source=DESKTOP-HTUFPR1\SQLEXPRESS;Initial Catalog=WhatsAppChatDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {

        }


        public WhatsAppDbContext(string connString)
        {
            _connString = connString;   

        }


        public  async Task<SqlConnection> OpenConnection()
        {

            _dbConnection = new SqlConnection(_connString);

            await _dbConnection.OpenAsync();
            return _dbConnection;

        }


        public async Task CloseConnection()
        {
            if(_dbConnection?.State != System.Data.ConnectionState.Closed)
            {
                _dbConnection?.CloseAsync();
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _dbConnection.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
