using EmployeeAuthenticationWebAPI.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAuthenticationWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IDbUtil _iDBUtil;
		private readonly IConfiguration _config;
		private readonly IWebHostEnvironment _env;
		public EmployeeController(IDbUtil iDbUtil, IWebHostEnvironment env)
		{
			_iDBUtil = iDbUtil;
			_env = env;
		}
		[HttpGet]
		public List<Employee> Get()
		{
			List<Employee> emps = new List<Employee>();
			var query = "select * from emp.Employee";
			_iDBUtil.ExecuteQuery(query,
				rdr =>
				{
					while (rdr.Read())
					{
						var employee = new Employee()
						{
							EmployeeID = rdr.GetInt32(0),
							FirstName = rdr.GetString(1),
							LastName = rdr.GetString(2),
							DateOfBirth = rdr.GetDateTime(3),
							PhotoFIleName = rdr[4] as string
						};
						emps.Add(employee);
					}
				}
				, null);
			return emps;
		}

		[HttpPost]
		public List<Employee> Add(Employee emp)
		{
			List<Employee> emps = new List<Employee>();
			var query = $"INSERT  INTO EMP.Employee(FirstName, LastName, DateOfBirth, PhotoFIleName) " +
				$"values('{emp.FirstName}', '{emp.LastName}', '{emp.DateOfBirth}', '{emp.PhotoFIleName}')";
			_iDBUtil.ExecuteQuery(query,
				rdr =>
				{
					while (rdr.Read())
					{
						var employee = new Employee()
						{
							EmployeeID = rdr.GetInt32(0),
							FirstName = rdr.GetString(1),
							LastName = rdr.GetString(2),
							DateOfBirth = rdr.GetDateTime(3),
							PhotoFIleName = rdr.GetString(4)
						};
						emps.Add(employee);
					}
				}

				, null);
			return Get();
		}
		[HttpPut]
		public List<Employee> Update(Employee emp)
		{
			List<Employee> emps = new List<Employee>();
			var query = $"Update d SET FirstName = '{emp.FirstName}' from EMP.Employee d where ID = {emp.EmployeeID}";
			_iDBUtil.ExecuteQuery(query,
				rdr =>
				{
					while (rdr.Read())
					{
						var employee = new Employee()
						{
							EmployeeID = rdr.GetInt32(0),
							FirstName = rdr.GetString(1),
							LastName = rdr.GetString(2),
							DateOfBirth = rdr.GetDateTime(3),
							PhotoFIleName = rdr.GetString(4)
						};
						emps.Add(employee);
					}
				}

				, null);
			return Get();
		}
		[HttpDelete("{id}")]
		public List<Employee> Delete(int id)
		{
			List<Employee> emps = new List<Employee>();
			var query = $"Delete from EMP.Employee  where ID = {id}";
			_iDBUtil.ExecuteQuery(query,
				rdr =>
				{
					while (rdr.Read())
					{
						var employee = new Employee()
						{
							EmployeeID = rdr.GetInt32(0),
							FirstName = rdr.GetString(1),
							LastName = rdr.GetString(2),
							DateOfBirth = rdr.GetDateTime(3),
							PhotoFIleName = rdr.GetString(4)
						};
						emps.Add(employee);
					}
				}

				, null);
			return Get();
		}
		[Route("SaveFile")]
		[HttpPost]
		public JsonResult SaveFile()
		{
			try
			{
				var httpRequest = Request.Form;
				var postedFile = httpRequest.Files[0];
				var fileName = postedFile.FileName;
				var physicalPath = _env.ContentRootPath+"/Photos/"+fileName;
				using(var stream = new FileStream(physicalPath, FileMode.Create))
				{
					postedFile.CopyTo(stream);
				}

				return new JsonResult(fileName);
			}
			catch
			{
				return new JsonResult("Anonymous.jpg");
			}

		}
		[Route("GetAllDepartmentsName")]
		[HttpGet]
		public List<string> GetAllDepts()
		{
			List<string> dpts = new List<string>();
			var query = "select DepartmentName from EMP.department";
			_iDBUtil.ExecuteQuery(query,
				rdr =>
				{
					while (rdr.Read())
					{
						dpts.Add(rdr.GetString(0));
					}
				}
				, null);
			//return new JsonResult(dpts);
			return dpts;
		}
		//[Route("SaveFile")]
		//[HttpPost]
		//public List<Employee> SaveFile()
		//{
		//	try
		//	{
		//		var httpRequest = Request.Form;
		//		var postedFile = httpRequest.Files[0];
		//	}
		//	catch(Exception ex)
		//	{
		//		throw;
		//	}
		//}

	}
}
