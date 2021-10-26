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
		public JsonResult /*List<Employee>*/ Get()
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
			return new JsonResult(emps);//emps;
		}

		[HttpPost]
		public JsonResult Add(Employee emp)
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
			return new JsonResult("Added Successfully");
		}
		[HttpPut]
		public JsonResult Update(Employee emp)
		{
			List<Employee> emps = new List<Employee>();
			var query = $"Update d SET FirstName = '{emp.FirstName}', LastName = '{emp.LastName}', DateOfBirth = '{emp.DateOfBirth}' from EMP.Employee d where ID = {emp.EmployeeID}";
			_iDBUtil.ExecuteQuery(query,null, null);
			return new JsonResult("Updated");
		}
		[HttpDelete("{id}")]
		public void Delete(int id)
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
		public JsonResult /*List<string>*/ GetAllDepts()
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
			return new JsonResult(dpts);
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
