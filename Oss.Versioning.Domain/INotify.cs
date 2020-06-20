using System;
using System.Collections.Generic;
using System.Text;

namespace Oss.Versioning.Domain
{
    public interface INotify
    {
        public void WriteHeader(string header);
        public void Write(string notification);
        public void WriteError(string error);
    }
}
