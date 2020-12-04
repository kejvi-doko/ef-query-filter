using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace FilterParser.UnitTest
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class FilterRowUnitTest
    {
        [Fact]
        public void TestFilterGeneration()
        {
            FilterRow root = new FilterRow()
            {
                ColumnName = "Name",
                Operator = "EQ",
                CompareValue = "Tomas",
                BooleanOperator = "OR",
                NextFilter = new FilterRow()
                {
                    Operator = "GT",
                    ColumnName = "Age",
                    CompareValue = 25
                }
            };

            List<Student> students = new List<Student>()
            {
                new Student()
                {
                    Age = 25,
                    Name = "James"
                },
                new Student()
                {
                    Age = 35,
                    Name = "Kejvi"
                },
                new Student()
                {
                    Age = 25,
                    Name = "Tomas"
                },
            };


            var filterGenerator = new FilterParser.FilterGenerator<Student>(root);

            var compliedExpression = filterGenerator.GenerateFilter();
            var sts = students.Where(compliedExpression).ToList();
            Assert.Equal(2, sts.Count);
        }

        [Fact]
        public void TestJsonParsing()
        {
            var requestFilter = @"{
                    ""CompareValue"": ""Tomas"",
                    ""ColumnName"": ""Name"",
                    ""NextFilter"": {
                        ""CompareValue"": 25,
                        ""ColumnName"": ""Age"",
                        ""NextFilter"": null,
                        ""BooleanOperator"": null,
                        ""Operator"": ""GT""
                    },
                    ""BooleanOperator"": ""OR"",
                    ""Operator"": ""EQ""
                }";

            
            FilterRow root = JsonSerializer.Deserialize<FilterRow>(requestFilter);

            List<Student> students = new List<Student>()
            {
                new Student()
                {
                    Age = 25,
                    Name = "James"
                },
                new Student()
                {
                    Age = 35,
                    Name = "Kejvi"
                },
                new Student()
                {
                    Age = 25,
                    Name = "Tomas"
                },
            };

            var filterGenerator = new FilterParser.FilterGenerator<Student>(root);

            var compliedExpression = filterGenerator.GenerateFilter();
            var sts = students.Where(compliedExpression).ToList();
            Assert.Equal(2, sts.Count);

        }
    }
}
