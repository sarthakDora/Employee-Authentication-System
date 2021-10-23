using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAuthenticationWebAPI.Model
{
	public class Employee
	{
		private int _employeeID;
		public int EmployeeID
		{
			get { return _employeeID; }
			set { _employeeID = value; }
		}

		private string _firstName;
		public string FirstName
		{
			get { return _firstName; }
			set { _firstName = value; }
		}

		private string _lastName;
		public string LastName
		{
			get { return _lastName; }
			set { _lastName = value; }
		}

		private DateTime _dateOfBirth;
		public DateTime DateOfBirth
		{
			get { return _dateOfBirth; }
			set { _dateOfBirth = value; }
		}

		private string _photoFIleName;
		public string PhotoFIleName
		{
			get { return _photoFIleName; }
			set { _photoFIleName = value; }
		}
	}
}
