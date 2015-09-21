using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanViewModels
{
    public interface IServiceStatus
    {
        bool IsBusy { get; }
        string LastError { get; }
    }
}
