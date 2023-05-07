namespace RefactoringTest;

using Refactoring;

// Example from; adapted to C#:
//
// Martin Fowler. Refactoring: Improving the Design of Existing Code.
// 2nd edition. Addison-Wesley Professional. November 2018. 448 pages.
// ISBN: 978-0134757681.

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var plays = new Dictionary<string, Play>() {
            { "hamlet", new Play { Name = "Hamlet", Type = "tragedy" } },
            { "as-like", new Play { Name = "As You Like It", Type = "comedy" } },
            { "othello", new Play { Name = "Othello", Type = "tragedy" } },
        };
        var invoice = new Invoice() {
            Customer = "BigCo",
            Performances = new List<Performance>() {
                new Performance() { ID = "hamlet", Audience = 55 },
                new Performance() { ID = "as-like", Audience = 35 },
                new Performance() { ID = "othello", Audience = 40 },
            }
        };
        var expected = File.ReadAllText(Path.Combine("TestData", "Expected1.txt"));
        var actual = Report.Statement(plays, invoice);
        Assert.AreEqual(expected, actual);
    }
}