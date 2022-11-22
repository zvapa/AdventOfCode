namespace Tests._2015
{
    public class Day5Tests
    {
        //const string SkipOrNot = null; // Run all tests
        const string SkipOrNot = "Remove the Skip argument to run this test."; // Skip all tests

        [Theory(Skip = SkipOrNot)]
        [InlineData("qjhvhtzxzqqjkmpb", true)]
        [InlineData("xxyxx", true)]
        [InlineData("uurcxstgmygtbstg", false)]
        [InlineData("ieodomkazucvgmuy", false)]
        [InlineData("sleaoasjspnjctqt", false)]
        public void Part2(string input, bool expected)
        {
            bool actual = Day5.IsNiceWord(input);
            string message = $"IsNiceWord({input}) was expected to be {expected} but was {actual}.";
            Assert.True(actual == expected, message);
        }
    }
}
