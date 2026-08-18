// using Maris.Compiler;
// using Xunit;

// public class CompilationTests
// {
//     [Fact]
//     public void Compilation_InitializesUnitsList()
//     {
//         // Arrange
//         var filePaths = new List<string> { "file1.maris", "file2.maris" };
//         var compilation = new Compilation(filePaths);

//         // Act
//         var units = compilation.Units;

//         // Assert
//         Assert.NotNull(units);
//         Assert.Equal(2, units.Count);
//     }
// }