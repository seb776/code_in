// Guids.cs
// MUST match guids.h
using System;

namespace Code_in.VSCode_in
{
    static class GuidList
    {
        public const string guidVSCode_inPkgString = "d7154121-42a0-44b0-96a9-9e93f89228ac";
        public const string guidVSCode_inCmdSetString = "9f93f2fb-cf0b-4fc7-9f21-f174680ea39a";

        public static readonly Guid guidVSCode_inCmdSet = new Guid(guidVSCode_inCmdSetString);
    };
}