using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAuthenticationWebAPI
{
	public interface IDbUtil
	{
		void ExecuteQuery(string query, Action<IDataReader> selector = null, Action<IDbCommand> paramSelector = null);
	}
}
