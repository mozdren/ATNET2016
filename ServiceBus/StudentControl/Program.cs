using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentControl.ServiceZETRef;

namespace StudentControl
{
    class Program
    {
        static void Main(string[] args)
        {
            IZET006Service client = new ZET006ServiceClient();
            var student = client.GetStudent("zet006");
            Console.WriteLine(student.Result.Message);

            var students = client.GetAllStudents();
            Console.WriteLine(students.Result.Message);

            var sortedStudents = client.GetAllStudentsSortedBySurname();
            Console.WriteLine(sortedStudents.Result.Message);

            var selectedStudents = client.GetStudentsByCity("Ostrava");
            Console.WriteLine(selectedStudents.Result.Message);
        }
    }
}
