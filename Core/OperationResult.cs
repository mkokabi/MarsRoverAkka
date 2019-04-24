using System;
using System.Collections.Generic;
using System.Text;

namespace Zip.MarsRover.Core
{
    public class OperationResult
    {
        public bool Successful { get; protected set; } = true;

        public bool Failed { get; protected set; } = false;

        public string Error { get; protected set; }

        public string Result { get; protected set; }
    }

    public sealed class SuccessOperationResult : OperationResult
    {
        public SuccessOperationResult(string result)
        {
            Failed = false;
            Successful = true;
            Result = result;
        }
    }

    public sealed class FailOperationResult : OperationResult
    {
        public FailOperationResult(string error)
        {
            Failed = true;
            Successful = false;
            Error = error;
        }
    }
}
