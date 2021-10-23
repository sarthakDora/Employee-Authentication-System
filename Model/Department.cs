using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAuthenticationWebAPI.Model
{
	public class Department  /*DbContext*/
	{
		//public Department(DbContextOptions<Department> options) : base(options)
		//{

		//}
		private int _departmentId;
		public int DepartmentId
		{
			get { return _departmentId; }
			set { _departmentId = value; }
		}

		private string _departmentName;
		public string DepartmentName
		{
			get { return _departmentName; }
			set { _departmentName = value; }
		}

	}
}
