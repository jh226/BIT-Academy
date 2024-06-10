using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _0428_성적
{
    internal class StudentList : ObservableCollection<Student>
    {
        static Random r = new Random(DateTime.Now.Millisecond);

        public StudentList()
        {
            MakeStudent();            
        }

        private void MakeStudent()
        {
            var names = new string[] { "이길동", "김길동", "홍길동", "정길동", "허길동", "박길동", "황길동"};
            var ids = new int[] { 2020, 2021, 2022, 2023};
            var subjects = new string[] { "WINFORM", "JAVA", "C#", "Python", "Network", "비트 고급", "비트 단기" };
            var Grades = new string[] { "A", "A+", "B", "B+", "C", "C+", "D", "D+", "F" };
            
            for (int i = 0; i < 15; i++)
            {             
                string name = pickRandom(names);
                int id = pickRandom(ids);
                string subject = pickRandom(subjects);
                string grade = pickRandom(Grades);


                this.Add(new Student(name, id, subject, grade));
            }
        }

        static T pickRandom<T>(T[] array)
        {
            return array[r.Next(array.Length)];
        }

        public IEnumerable<Student> FindStudents(string searchString)
        {
            return this.Where(p => p.Name.Contains(searchString));
        }
    }
}
