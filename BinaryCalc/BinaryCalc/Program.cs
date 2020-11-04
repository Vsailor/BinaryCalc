using System;
using System.Text;


// Instructions:
// 1. Implement the method, Solution.AddBinary, to return the sum of two
// non-negative, 32-bit integer strings.  The output string should also be
// 32-bits long and padded with zeros.  Do not use built-in methods to
// convert the strings to integers in order to add them, write the logic
// to add them in binary form.
// 2. The solution will be accepted once all the unit tests pass.
// 3. You may modify Solution however you wish, but do not modify its Main method
// or the acceptance criteria of the test suite.


class Solution
{
    public String AddBinary(String lhs, String rhs)
    {
        var maxLength = Math.Max(lhs.Length, rhs.Length);

        lhs = lhs.PadLeft(maxLength + 1, '0');
        rhs = rhs.PadLeft(maxLength + 1, '0');

        char remainder = '0';
        string result = "";

        for (var i = lhs.Length - 1; i >= 0; i--)
        {
            result = $"{(char)(lhs[i] ^ rhs[i] ^ remainder)}{result}";

            if (lhs[i] == '1' && rhs[i] == '1' ||
                lhs[i] == '1' && remainder == '1' ||
                rhs[i] == '1' && remainder == '1')
            {
                remainder = '1';
                continue;
            }

            remainder = '0';
        }

        return result;
    }

    static void Main(string[] args)
    {
        Solution solution = new Solution();
        Random random = new Random(0);
        TestSuite suite = new TestSuite(solution, random);
        suite.RunTests();
    }
}


class Constants
{
    public const int PRECISION = 32;
    public const int NUM_TESTS = 100;
}


class TestSuite
{
    private Solution solution;
    private Random random;
    
    public TestSuite(Solution solution, Random random)
    {
        this.solution = solution;
        this.random = random;
    }
    
    private String GenBinary()
    {
        StringBuilder builder = new StringBuilder('0');
        
        for (int i = 0; i < Constants.PRECISION - 1; i += 1)
        {
            builder.Append(random.Next(2));
        }
        
        return builder.ToString();
    }
    
    
    
    public void RunTests()
    {
        for (int i = 0; i < Constants.NUM_TESTS; i += 1)
        {
            String lhs = GenBinary();
            String rhs = GenBinary();
            UnitTest unitTest = new UnitTest(lhs, rhs);
            String actualValue = solution.AddBinary(lhs, rhs);
            if (!unitTest.Accepts(actualValue))
            {
                String message = String.Format("Expected {0} + {1} = {2} but got {3}",
                                               lhs, rhs, unitTest.ExpectedValue,
                                               actualValue);
                throw new Exception(message);
            }
        }
        Console.WriteLine("PASS");
    }
}


class UnitTest
{
    private String lhs;
    private String rhs;
    
    public UnitTest(String lhs, String rhs)
    {
        this.lhs = lhs;
        this.rhs = rhs;
    }
    
    public String ExpectedValue
    {
        get
        {
            // Task: Write the logic to perform this addition in binary form
            long lhs = Convert.ToInt64(this.lhs, 2);
            long rhs = Convert.ToInt64(this.rhs, 2);
            long sum = lhs + rhs;
            return Convert.ToString(sum, 2).PadLeft(Constants.PRECISION, '0');
        }
    }
    
    public bool Accepts(String actualValue) {
        return ExpectedValue.Equals(actualValue);
    }
}