using System.IO;
using System.Text.RegularExpressions;
using DirectorySync.Console;
using DirectorySync.Console.Types;
using NUnit.Framework;

namespace DirectorySync.Core.Tests.Unit;

[TestFixture]
public class DirectoryPathTests
{
    [Test]
    public void Test1()
    {
        var pathStr = "./\\test///data/";
        pathStr = pathStr.Replace("\\", "/");
        pathStr = Regex.Replace(pathStr, "/{2,}", "/");
        pathStr = Regex.Replace(pathStr, "\\{2,}", "\\");
        pathStr = pathStr.Trim('/');

        // var path = DirectoryPath.AbsoluteFrom(".////test_data/");
        // var exists = path.Exists();
    }
}