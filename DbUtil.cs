using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAuthenticationWebAPI
{
	public class DbUtil : IDbUtil
	{
		private readonly IConfiguration _config;
		public DbUtil(IConfiguration config)
		{
			_config = config;
		}
		public void ExecuteQuery(string query, Action<IDataReader> selector = null, Action<IDbCommand> paramSelector = null)
		{
			execute(query, CommandType.Text, selector, paramSelector);
		}
		private void execute(string query, CommandType cmdType, Action<IDataReader> selector, Action<IDbCommand> paramSelector)
		{
			query = query.Trim();
			using (IDbConnection cn = GetConnection())
			{
				using (IDbCommand cmd = cn.CreateCommand())
				{
					cmd.CommandText = query;
					cmd.CommandType = cmdType;
					try
					{
						cn.Open();
						if(selector == null)
						{
							cmd.ExecuteNonQuery();
						}
						else
						{
							using (var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
							{
								selector?.Invoke(rdr);
							}
						}
					}
					catch(Exception ex)
					{
						throw ex;
					}
					finally
					{
						if (cn.State != ConnectionState.Closed) cn.Close();
					}
				}
			}
		}
		public IDbConnection GetConnection(string cnName = null)
		{
			return new SqlConnection(_config[$"ConnectionStrings:{cnName ?? "DefaultConnection"}"]);
		}
	}
}
