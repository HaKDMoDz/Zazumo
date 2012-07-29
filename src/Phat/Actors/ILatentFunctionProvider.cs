using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Processors;

namespace Phat.Actors
{
    public interface ILatentFunctionProvider
    {
        void SetProcessRunner(IProcessRunner processRunner);
    }
}
