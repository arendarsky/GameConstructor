using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConstructor.Core
{
    public class GeneralMethods
    {
        public static IEnumerable<T1> GettingAbstractCollectionFromNormalCollection<T1, T2>(IEnumerable<T2> collection) where T2 : T1
        {
            try
            {
                List<T1> abstractCollection = new List<T1>();

                foreach (var item in collection)
                {
                    abstractCollection.Add(item);
                }

                return abstractCollection as IEnumerable<T1>;
            }

            catch { return null; }
        }
    }
}
