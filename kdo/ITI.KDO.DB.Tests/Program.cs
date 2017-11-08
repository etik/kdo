﻿using NUnit.Common;
using NUnitLite;
using System;
using System.Reflection;

namespace ITI.KDO.DB.Tests
{
    class Program
    {
        static int Main(string[] args)
        {
            return new AutoRun(typeof(Program).GetTypeInfo().Assembly)
                .Execute(args, new ExtendedTextWrapper(Console.Out), Console.In);
        }
    }
}
