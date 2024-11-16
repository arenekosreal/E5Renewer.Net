using E5Renewer.Models;

namespace E5Renewer.Tests.Models;

/// <summary>Test
/// <see cref="UintExtends"/>
/// </summary>
[TestClass]
public class UintExtendsTests
{
    /// <summary>
    /// Test
    /// <see cref="UintExtends.ToUnixFileMode(uint)" />
    /// </summary>
    [TestMethod]
    [DataRow(0b000000000, UnixFileMode.None)]
    [DataRow(0b000000001, UnixFileMode.OtherExecute)]
    [DataRow(0b000000010, UnixFileMode.OtherWrite)]
    [DataRow(0b000000011, UnixFileMode.OtherWrite | UnixFileMode.OtherExecute)]
    [DataRow(0b000000100, UnixFileMode.OtherRead)]
    [DataRow(0b000000101, UnixFileMode.OtherRead | UnixFileMode.OtherExecute)]
    [DataRow(0b000000110, UnixFileMode.OtherRead | UnixFileMode.OtherWrite)]
    [DataRow(0b000000111, UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute)]
    [DataRow(0b000001000, UnixFileMode.GroupExecute)]
    [DataRow(0b000010000, UnixFileMode.GroupWrite)]
    [DataRow(0b000011000, UnixFileMode.GroupWrite | UnixFileMode.GroupExecute)]
    [DataRow(0b000100000, UnixFileMode.GroupRead)]
    [DataRow(0b000101000, UnixFileMode.GroupRead | UnixFileMode.GroupExecute)]
    [DataRow(0b000110000, UnixFileMode.GroupRead | UnixFileMode.GroupWrite)]
    [DataRow(0b000111000, UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute)]
    [DataRow(0b001000000, UnixFileMode.UserExecute)]
    [DataRow(0b010000000, UnixFileMode.UserWrite)]
    [DataRow(0b011000000, UnixFileMode.UserWrite | UnixFileMode.UserExecute)]
    [DataRow(0b100000000, UnixFileMode.UserRead)]
    [DataRow(0b101000000, UnixFileMode.UserRead | UnixFileMode.UserExecute)]
    [DataRow(0b110000000, UnixFileMode.UserRead | UnixFileMode.UserWrite)]
    [DataRow(0b111000000, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute)]
    [DataRow(0b100100100, UnixFileMode.UserRead | UnixFileMode.GroupRead | UnixFileMode.OtherRead)]
    public void TestToUnixFileMode(int permission, UnixFileMode mode)
    {
        Assert.AreEqual(mode, ((uint)permission).ToUnixFileMode());
    }
}
