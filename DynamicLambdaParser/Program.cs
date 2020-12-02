using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FilterParser;

namespace DynamicLambdaParser
{
    class Program
    {
        static void Main(string[] args)
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

            // ParameterExpression parameter = Expression.Parameter(typeof(Student),"Student");
            // var property = Expression.Property(parameter, Type.GetType("DynamicLambdaParser.Student").GetProperty("Name"));
            // ConstantExpression c = Expression.Constant("Tomas", typeof(string));
            // BinaryExpression e = Expression.Equal(property, c);
            //
            // var property1 = Expression.Property(parameter, Type.GetType("DynamicLambdaParser.Student").GetProperty("Age"));
            // ConstantExpression c1 = Expression.Constant(25, typeof(int));
            // BinaryExpression e1 = Expression.Equal(property1, c1);
            //
            //
            // var e2 = Expression.Or(e, e1);
            //
            // var lambda3 = Expression.Lambda<Func<Student, bool>>(e2, new[]
            // {
            //     parameter
            // }).Compile();


            FilterGenerator<Student> studentFilterGenerator = new FilterGenerator<Student>()
            {
                root = root
            };
            
            var generatedExpression = studentFilterGenerator.GenerateFilter();
            
            var filteredStudents = students.Where(generatedExpression).ToList();

            // var sts = students.Where(lambda3).ToList();

            IStrategy<Student> andNodeStrategy = new AndNodeStrategy<Student>();
            IStrategy<Student> orNodeStrategy = new OrNodeStrategy<Student>();
            IStrategy<Student> filterNodeStrategy = new FilterNodeStrategy<Student>();
            IStrategy<Student> singularNodeStrategy = new SingularNodeStrategy<Student>();


            ILogicalNode<Student> rootNode = orNodeStrategy.CreateNode(
                filterNodeStrategy.CreateNode("Name", "James", "EQ"),
                filterNodeStrategy.CreateNode("Age", 25, "GT"));

            var e = rootNode.Eval(null);

            // e = filterNodeStrategy.CreateNode("Name", "James", "EQ").Eval();

            var compliedExpression = Expression.Lambda<Func<Student, bool>>(e,
                new[]
                {
                    rootNode.BaseParameter
                }).Compile();

            
            var sts = students.Where(compliedExpression).ToList();

            Console.WriteLine("Hello World!");
        }
    }
}