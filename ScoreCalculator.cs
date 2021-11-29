using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final
{
    public class ScoreCalculator
    {
        public int AddScores(IEnumerable<int> scores)
        {
            var sum = 0;
            foreach(var score in scores)
            {
                sum += score;

            }
            return sum;
        }
    }
}
