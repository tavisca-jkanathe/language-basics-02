using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(new[] {"12:12:12"}, new [] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
            Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }
	// Method to check selfcontradiction of input time
	private static bool IsValidInput(string[] exactPostTime, string[] showPostTime)
        {
            for(int i=0; i<exactPostTime.Length-1; i++)
            {
                for(int j=i+1; j<exactPostTime.Length; j++)
                {
                    if(exactPostTime[i].Equals(exactPostTime[j])==true)
                    {if((showPostTime[i].Equals(showPostTime[j]))==false)
                        {
                            return false;
                        }
                    }
                }
			}
			return true;
        }
	    
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            // Add your code here.
            //check for self-contradiction
             if(IsValidInput(exactPostTime, showPostTime)==false)
            {
                return "impossible";
            }
			
            string resultTime="";

            for(int i = 0; i<exactPostTime.Length; i++) 
            {
                DateTime postedDateTime, currentDateTime;
            	postedDateTime = DateTime.Parse(exactPostTime[i]);
                currentDateTime = postedDateTime;
		string timeToAdjust =showPostTime[i].Substring(0,2);

                // cases for second, minute and hour
                if(showPostTime[i].Contains("seconds"))
                {
                    currentDateTime = postedDateTime;
                }
                else if(showPostTime[i].Contains("minutes"))
                { 
                  currentDateTime = postedDateTime.AddMinutes(Convert.ToDouble(timeToAdjust));

                }
                else if(showPostTime[i].Contains("hours"))
                {
                    currentDateTime = postedDateTime.AddHours(Convert.ToDouble(timeToAdjust));
                }
            //convert time to 24 hour format and check for  lexicographical order
            if(string.Compare(resultTime,currentDateTime.ToString("HH:mm:ss")) < 1)
                 resultTime = currentDateTime.ToString("HH:mm:ss");
            }
            return resultTime;
        }
    }
}
