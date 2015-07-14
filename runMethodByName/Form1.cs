using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace runMethodByName
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Invoker.CreateAndInvoke("runMethodByName.Tester", null, "TestMethod", null);

            Invoker.CreateAndInvoke("runMethodByName.Tester", new[] { "constructorParam" }, "TestMethodUsingValueFromConstructorAndArgs", new object[] { "moo", false });
        }
    }

    public static class Invoker
    {
        public static object CreateAndInvoke(string typeName, object[] constructorArgs, string methodName, object[] methodArgs)
        {
            Type type = Type.GetType(typeName);
            object instance = Activator.CreateInstance(type, constructorArgs);

            MethodInfo method = type.GetMethod(methodName);
            return method.Invoke(instance, methodArgs);
        }
    }


    public class Tester
    {
        public string _testField;

        public Tester()
        {
        }

        public Tester(string arg)
        {
            _testField = arg;
        }

        public void TestMethod()
        {
            MessageBox.Show("Called TestMethod");
        }

        public void TestMethodWithArg(string arg)
        {
            MessageBox.Show("Called TestMethodWithArg: " + arg);
        }

        public void TestMethodUsingValueFromConstructorAndArgs(string arg, bool arg2)
        {
            MessageBox.Show("Called TestMethodUsingValueFromConstructorAndArg " + arg + " " + arg2 + " " + _testField);
        }

        public string GetContstructorValue()
        {
            return _testField;
        }
    }
}
