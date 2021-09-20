using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.SharedKernal.Data
{
    public class OperationResult<TResultData>
    {
        public TResultData ResultData { get; set; }
        public bool IsSuccess { get; set; }
        public Exception Exception { get; set; }
    }
}

