using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace assignment3
{
	


public class Program
{	
	public int temp=2;
	IList<Employee> employeeList;
	IList<Salary> salaryList;
		
	
	public Program()
	{
		employeeList = new List<Employee>()  
		{ 
			new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
			new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
			new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
			new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
			new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
			new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
			new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}			
		};
		
		salaryList = new List<Salary>()
		{
			new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
		};
	
	}
	
	public static void Main()
	{		
		Program program = new Program();
		
		program.Task1();
		
		program.Task2();
		
		program.Task3();
	}
	
	public void Task1()
    {   
		 var result = (from Employee in employeeList
					   join Salary in salaryList 
					   on Employee.EmployeeID equals Salary.EmployeeID
					   group new { Salary, Employee } by Employee.EmployeeFirstName into SalaryGroup
					   orderby SalaryGroup.Sum(i => i.Salary.Amount)
					   select new { Name = SalaryGroup.Key, sal = SalaryGroup.Sum(i => i.Salary.Amount) }
						   );
				Console.WriteLine("Total salary of all the employees with their names in ascending order \n");
                foreach (var i in result)
			    {
				    Console.WriteLine($"{i.Name}:{i.sal}");

			    }
			
		
        
		
	}
	
	public void Task2()
	{	
		
		 var result= (from Employee in employeeList
						   join sal in salaryList
						   on Employee.EmployeeID equals sal.EmployeeID into egroup
						   orderby Employee.Age descending
						   select new
						   {
							   id = Employee.EmployeeID,
							   firstname = Employee.EmployeeFirstName,
							   lastname = Employee.EmployeeLastName,
							   age = Employee.Age,
							   salary = egroup.Sum(i => i.Amount)
						   }).ToList();
			Console.WriteLine("\nDetails of 2nd oldest employee");
			Console.WriteLine(" ID: " + result[temp - 1].id);
			Console.WriteLine(" Name: " + result[temp - 1].firstname + " " + result[temp - 1].lastname);
			Console.WriteLine("Age: " + result[temp - 1].age);
			Console.WriteLine(" Salary: " + result[temp - 1].salary);
	}
	
	public void Task3()
	{
		  Console.WriteLine("\nMean of Monthly ,performance and bonus Salary Of Employees Whose Age Is Greater Than 30:");
			var result = from Employee in employeeList
						  where Employee.Age > 30
						  join salary in salaryList on Employee.EmployeeID equals salary.EmployeeID into grp
						  select new
						  {
							  mean_value = grp.Average(i => i.Amount)
						  };
						  
			foreach (var i in result)
			{
				Console.WriteLine(i.mean_value);
			}
     }

	}


public enum SalaryType{
	Monthly,
	Performance,
	Bonus
}

public class Employee{
	public int EmployeeID { get; set; }
	public string EmployeeFirstName { get; set; }
	public string EmployeeLastName { get; set; }
	public int Age { get; set; }	
}

public class Salary{
	public int EmployeeID { get; set; }
	public int Amount { get; set; }
	public SalaryType Type { get; set; }
}
}
