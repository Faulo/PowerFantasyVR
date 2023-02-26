using System.Runtime.CompilerServices;
using PFVR;

[assembly: InternalsVisibleTo(AssemblyInfo.NAMESPACE_EDITOR)]
[assembly: InternalsVisibleTo(AssemblyInfo.NAMESPACE_TESTS_RUNTIME)]
[assembly: InternalsVisibleTo(AssemblyInfo.NAMESPACE_TESTS_EDITOR)]
[assembly: InternalsVisibleTo(AssemblyInfo.NAMESPACE_TESTS_INPUT)]

namespace PFVR {
    public static class AssemblyInfo {
        public const string NAMESPACE_RUNTIME = "PFVR";
        public const string NAMESPACE_EDITOR = "PFVR.Editor";
        public const string NAMESPACE_INPUT = "PFVR.Input";

        public const string NAMESPACE_TESTS_RUNTIME = "PFVR.Tests.Runtime";
        public const string NAMESPACE_TESTS_EDITOR = "PFVR.Tests.Editor";
        public const string NAMESPACE_TESTS_INPUT = "PFVR.Tests.Input";
    }
}