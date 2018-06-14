using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConstructor.Core.SpecialMethods
{
    public class MathLogicMethods
    {
        public const char conjunction = '&';
        public const char disjunction = '|';



        public static List<string> SplittingConjuctionAndDisjunctionsFromAString(string condition)
        {
            var splittedCondition = new List<string>();

            int i = 0;

            while (i < condition.Length)
            {
                if (condition[i] == conjunction || condition[i] == disjunction)
                {
                    splittedCondition.Add(condition.Substring(0, i - 1));
                    splittedCondition.Add(condition[i].ToString());

                    condition = condition.Substring(i + 2);

                    i = 0;
                }

                else
                {
                    i++;
                }
            }

            splittedCondition.Add(condition);

            return splittedCondition;
        }



    }
}
