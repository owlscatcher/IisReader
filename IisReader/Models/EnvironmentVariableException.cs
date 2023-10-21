using System;

namespace IisReader.Models
{
    class EnvirovmentVariableException : Exception
    {
        public EnvirovmentVariableException(string message) : base(message) { }
    }
}