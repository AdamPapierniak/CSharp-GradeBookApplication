using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            int numberOfStudents = Students.Count;
            List<double> ListOfGrades = new List<double>();

            if (numberOfStudents < 5)
            {
                throw new InvalidOperationException();
            }

            foreach (Student student in Students)
            {
                foreach (double grade in student.Grades)
                    ListOfGrades.Add(grade);
            }

            ListOfGrades.Sort((x,y)=> y.CompareTo(x));

            if (averageGrade >= ListOfGrades[GetGradeCutoff(20)])
            {
                return 'A';
            }
            else if (averageGrade >= ListOfGrades[GetGradeCutoff(40)])
            {
                return 'B';
            }
            else if (averageGrade >= ListOfGrades[GetGradeCutoff(60)])
            {
                return 'C';
            }
            else if (averageGrade >= ListOfGrades[GetGradeCutoff(80)])
            {
                return 'D';
            }
            else
            {
                return 'F';
            }


        }

        public int GetGradeCutoff(int percentage)
        {
            double numberOfGrades = 0;

            foreach (Student student in Students)
            {
                numberOfGrades += student.Grades.Count;
            }
                     
            return (int)Math.Round(numberOfGrades * percentage / 100.0, MidpointRounding.ToEven) - 1;
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }
            else
            {
                base.CalculateStatistics();
            }
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }
            else
            {
                base.CalculateStudentStatistics(name);
            }
         }
    }
}
