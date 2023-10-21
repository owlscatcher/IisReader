using System;
using IisReader.Models;

namespace IisReader.Helpers
{
    internal class EnvironmentHelper
    {
        public static string GetEnvirovmentVariable(string name)
        {
            var variable = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.User);

            if (string.IsNullOrWhiteSpace(variable))
                throw new EnvirovmentVariableException($"Переменная среды \"{name}\" не задана, пустая или находится не в среде \"User\"");

            return variable;
        }
    }
}
