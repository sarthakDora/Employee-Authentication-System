using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using EmployeeAuthenticationWebAPI.Model;

namespace EmployeeAuthenticationWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		private readonly IDbUtil _iDBUtil;
		public DepartmentController(IDbUtil iDbUtil)
		{
			_iDBUtil = iDbUtil;
		}
		
		[HttpGet]
		public List<Department> GetDepartment()
		{
			List<Department> dpts = new List<Department>();
			var query = "select * from EMP.department";
			_iDBUtil.ExecuteQuery(query, 
				rdr =>
				{
					while(rdr.Read())
					{
						var departmnt = new Department()
						{
							DepartmentId = rdr.GetInt32(0),
							DepartmentName = rdr.GetString(1)
						};
						dpts.Add(departmnt);
					}
				}
				, null);
			//return new JsonResult(dpts);
			return dpts;
		}

		[HttpPost]
		public List<Department> AddDepartment(Department dep)
		{
			List<Department> dpts = new List<Department>();
			var query = $"INSERT  INTO EMP.Department(DepartmentName) values('{dep.DepartmentName}')";
			_iDBUtil.ExecuteQuery(query,
				rdr =>
				{
					while (rdr.Read())
					{
						var departmnt = new Department()
						{
							DepartmentId = rdr.GetInt32(0),
							DepartmentName = rdr.GetString(1)
						};
						dpts.Add(departmnt);
					}
				}
				
				, null);
			//return new JsonResult(dpts);
			return GetDepartment();
		}
		[HttpPut]
		public List<Department> UpdateDepartment(Department dep)
		{
			List<Department> dpts = new List<Department>();
			var query = $"Update d SET DepartmentName = '{dep.DepartmentName}' from EMP.Department d where DepartmentId = {dep.DepartmentId}";
			_iDBUtil.ExecuteQuery(query,
				rdr =>
				{
					while (rdr.Read())
					{
						var departmnt = new Department()
						{
							DepartmentId = rdr.GetInt32(0),
							DepartmentName = rdr.GetString(1)
						};
						dpts.Add(departmnt);
					}
				}

				, null);
			return GetDepartment();
		}
		[HttpDelete("{deptId}")]
		public List<Department> DeleteDepartment(int deptId)
		{
			List<Department> dpts = new List<Department>();
			var query = $"Delete from EMP.Department  where DepartmentId = {deptId}";
			_iDBUtil.ExecuteQuery(query,
				rdr =>
				{
					while (rdr.Read())
					{
						var departmnt = new Department()
						{
							DepartmentId = rdr.GetInt32(0),
							DepartmentName = rdr.GetString(1)
						};
						dpts.Add(departmnt);
					}
				}

				, null);
			return GetDepartment();
		}
	}
}
