using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Models;

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
                IEnumerable<T> reverseException = collection2.Except(collection1);

                if ((exception == null || exception.Count() == 0) && (reverseException == null || reverseException.Count() == 0))
                {
                    return true;
                }

                return false;
            }
        }



        public static Dictionary<string, string> FormingTheDictionary(IEnumerable<string> stringCollection)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            List<string> collectionListRepresentation = stringCollection.ToList();

            //while (collectionListRepresentation.Count != 0)
            //{
            //    string element = collectionListRepresentation[collectionListRepresentation.Count - 1];

            //    int numberOfSimillarElements = collectionListRepresentation.Count(el => el[0] == element[0]);

            //    if (numberOfSimillarElements == 1 || element.Length == 1)
            //    {
            //        dictionary.Add(element[0].ToString(), element);

            //        collectionListRepresentation.Remove(element);
            //    }

            //    else
            //    {
            //        List<string> simillarElements = collectionListRepresentation
            //            .Where(el => el[0] == element[0])
            //            .ToList();

            //        while (simillarElements.Count != 0)
            //        {
            //            string simillarElement = simillarElements[simillarElements.Count - 1];

            //            int numberOfVerySimillarElements;
            //        }

            //    }
            //}

            foreach (var str in stringCollection)
            {
                dictionary.Add(str, str);
            }

            return dictionary;
        }



        private void ComparingTwoStringsForEqualityOfBeginings(string str1, string str2, out string result1, out string result2)
        {
            result1 = ""; result2 = null;
        }
    }
}
