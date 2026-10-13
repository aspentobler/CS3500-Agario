using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowardAgarioStepTwo
{
    internal class Student : Person
    {
        public float GPA { get; private set; } = 4;

        public Student(float GPA, int id, string Name) : base(Name)
        {
            this. GPA = GPA;
        }

    }
}
