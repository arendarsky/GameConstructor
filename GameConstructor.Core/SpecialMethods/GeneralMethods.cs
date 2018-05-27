using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConstructor.Core.SpecialMethods
{
    public class GeneralMethods
    {
        public static bool CheckingWhetherCollectionsHaveTheSameValues<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
        {
            if (collection1 == null && collection2 == null)
            {
                return true;
            }

            else if (collection1 == null || collection2 == null)
            {
                return false;
            }

            else
            {
                IEnumerable<T> exception = collection1.Except(collection2);

                if (exception == null || exception.Count() == 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
