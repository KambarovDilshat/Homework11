
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homework11

{
    // Задание 1: Структура Employee и интерфейс

    public interface IEmployee
    {
        string ToString();
    }

    public struct Employee : IEmployee
    {
        public string Name;
        public string Position;
        public decimal Salary;
        public DateTime HireDate;
        public Gender Gender;

        public Employee(string name, string position, decimal salary, DateTime hireDate, Gender gender)
        {
            Name = name;
            Position = position;
            Salary = salary;
            HireDate = hireDate;
            Gender = gender;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Position: {Position}, Salary: {Salary:C}, Hire Date: {HireDate.ToShortDateString()}, Gender: {Gender}";
        }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    // Задание 2: Класс Student

    public class Student
    {
        public string FullName;
        public string Group;
        public double AverageScore;
        public decimal FamilyIncome;
        public int FamilyMembers;
        public Gender Gender;
        public EducationForm EducationForm;

        public Student(string fullName, string group, double averageScore, decimal familyIncome, int familyMembers, Gender gender, EducationForm educationForm)
        {
            FullName = fullName;
            Group = group;
            AverageScore = averageScore;
            FamilyIncome = familyIncome;
            FamilyMembers = familyMembers;
            Gender = gender;
            EducationForm = educationForm;
        }

    }

    public enum EducationForm
    {
        FullTime,
        PartTime,
        DistanceLearning
    }


    // Задание 3: Класс Cat и перечисление Food

    public class Cat
    {
        public int SatiationLevel;

        public void Eat(Food food)
        {
            switch (food)
            {
                case Food.Fish:
                    SatiationLevel += 5;
                    break;
                case Food.Mouse:
                    SatiationLevel += 3;
                    break;
                case Food.CatFood:
                    SatiationLevel += 6;
                    break;
                case Food.Milk:
                    SatiationLevel += 4;
                    break;
            }
        }
    }

    public enum Food
    {
        Fish,
        Mouse,
        CatFood,
        Milk,
    }
    class Program
    {
        const decimal MINIMUM_SALARY = 10000;
        static void Main(string[] args)
        {
            // Задание 1: Работа с сотрудниками
            Console.WriteLine("Enter the number of employees:");
            int employeeCount = int.Parse(Console.ReadLine());
            Employee[] employees = new Employee[employeeCount];

            for (int i = 0; i < employeeCount; i++)
            {
                Console.WriteLine($"Enter data for employee {i + 1}:");
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Position: ");
                string position = Console.ReadLine();
                Console.Write("Salary: ");
                decimal salary = decimal.Parse(Console.ReadLine());
                Console.Write("Hire Date (yyyy-MM-dd): ");
                DateTime hireDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Gender (Male/Female/Other): ");
                Gender gender = (Gender)Enum.Parse(typeof(Gender), Console.ReadLine());

                employees[i] = new Employee(name, position, salary, hireDate, gender);
            }

            // Вывод информации о всех сотрудниках
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.ToString());
            }

            // Задание 2: Работа со студентами

            List<Student> students = new List<Student>();

            // Добавляем студентов в список (в реальном приложении данные будут вводиться пользователем)
            students.Add(new Student("Ivan Kalinin", "Школьник", 4.3, 12000, 5, Gender.Male, EducationForm.FullTime));
            students.Add(new Student("Yan Nefalted", "Сварщик", 4.0, 15000, 2, Gender.Male, EducationForm.FullTime));
            students.Add(new Student("Dmitriy Falkovskiy", "Айтишник", 4.5, 18000, 4, Gender.Male, EducationForm.FullTime));
            students.Add(new Student("Roman Abakumov", "Айтишник", 228.3, 28000, 3, Gender.Male, EducationForm.FullTime));

            // Выполнение различных отчетов
            PrintStudentsWithLowFamilyIncomeFirst(students);
            PrintDormitoryQueueWithColorCoding(students);
            PrintStudentsWithIncompleteFamily(students);
            PrintTop10StudentsByHighestScore(students);
            PrintTop10StudentsByLowestScore(students);
            PrintStudentsWithLowestIncomeAndIncompleteFamily(students);

            // Задание 3: Работа с классом Cat
            Cat cat = new Cat();
            cat.Eat(Food.Fish);
            Console.WriteLine($"Cat's satiation level after eating fish: {cat.SatiationLevel}");
            cat.Eat(Food.Mouse);
            Console.WriteLine($"Cat's satiation level after eating mouse: {cat.SatiationLevel}");

        }
        static void PrintStudentsWithLowFamilyIncomeFirst(List<Student> students)
        {
            var sortedStudents = students.OrderBy(s => s.FamilyIncome > 2 * MINIMUM_SALARY)
                                         .ThenByDescending(s => s.AverageScore);

            Console.WriteLine("Students with low family income first:");
            foreach (var student in sortedStudents)
            {
                Console.WriteLine(student.FullName);
            }
        }

        static void PrintDormitoryQueueWithColorCoding(List<Student> students)
        {
            var sortedStudents = students.OrderBy(s => s.FamilyIncome > 2 * MINIMUM_SALARY)
                                         .ThenByDescending(s => s.AverageScore)
                                         .ToList();

            Console.WriteLine("Dormitory queue with color coding:");
            for (int i = 0; i < sortedStudents.Count; i++)
            {
                string color = i < 10 ? "Green" : i < 20 ? "Orange" : "Red";
                Console.WriteLine($"{color}: {sortedStudents[i].FullName}");
            }
        }

        static void PrintStudentsWithIncompleteFamily(List<Student> students)
        {
            var filteredStudents = students.Where(s => s.FamilyMembers < 2);

            Console.WriteLine("Students with incomplete family:");
            foreach (var student in filteredStudents)
            {
                Console.WriteLine(student.FullName);
            }
        }

        static void PrintTop10StudentsByHighestScore(List<Student> students)
        {
            var topStudents = students.OrderByDescending(s => s.AverageScore).Take(10);

            Console.WriteLine("Top 10 students by highest score:");
            foreach (var student in topStudents)
            {
                Console.WriteLine(student.FullName);
            }
        }

        static void PrintTop10StudentsByLowestScore(List<Student> students)
        {
            var bottomStudents = students.OrderBy(s => s.AverageScore).Take(10);

            Console.WriteLine("Top 10 students by lowest score:");
            foreach (var student in bottomStudents)
            {
                Console.WriteLine(student.FullName);
            }
        }

        static void PrintStudentsWithLowestIncomeAndIncompleteFamily(List<Student> students)
        {
            var filteredStudents = students.Where(s => s.FamilyMembers < 2)
                                           .OrderBy(s => s.FamilyIncome)
                                           .Take(3);

            Console.WriteLine("Students with the lowest income and incomplete family:");
            foreach (var student in filteredStudents)
            {
                Console.WriteLine(student.FullName);
            }
        }
    }
}