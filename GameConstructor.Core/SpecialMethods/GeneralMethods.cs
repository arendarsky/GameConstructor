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



        public static bool AreThereSameElementsInTheStringCollection (IEnumerable<string> collection)
        {
            foreach (var str in collection)
            {
                int number = collection.Count(item => item == str);

                if (number != 1)
                {
                    return true;
                }
            }

            return false;
        }



        public static Dictionary<string, string> FormingTheAbbreviationDictionary(IEnumerable<string> stringCollection)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            List<string> collectionListRepresentation = stringCollection
                .OrderBy(str => str)
                .ToList();

            while (collectionListRepresentation.Count != 0)
            {
                string element = collectionListRepresentation[collectionListRepresentation.Count - 1];

                int numberOfSimillarElements = collectionListRepresentation.Count(el => el[0] == element[0]);

                if (numberOfSimillarElements == 1)
                {
                    dictionary.Add(element, element[0].ToString());

                    collectionListRepresentation.Remove(element);
                }

                else
                {
                    List<string> simillarElements = collectionListRepresentation
                        .Where(el => el[0] == element[0])
                        .ToList();

                    collectionListRepresentation = collectionListRepresentation.Except(simillarElements).ToList();

                    if (simillarElements.Contains(element[0].ToString()))
                    {
                        dictionary.Add(element[0].ToString(), element[0].ToString());

                        simillarElements.Remove(element[0].ToString());
                    }

                    if (simillarElements.Count == 1)
                    {
                        dictionary.Add(simillarElements[0], simillarElements[0].Substring(0, 2));
                    }

                    else
                    {
                        while (simillarElements.Count != 0)
                        {
                            string simillarElement = simillarElements[simillarElements.Count - 1];

                            int numberOfVerySimillarElements = simillarElements.Count(el => el.Substring(0, 2) == simillarElement.Substring(0, 2));

                            if (numberOfVerySimillarElements == 1)
                            {
                                dictionary.Add(simillarElement, simillarElement.Substring(0, 2));

                                simillarElements.Remove(simillarElement);
                            }

                            else
                            {
                                if (simillarElements.Contains(simillarElement.Substring(0, 2)))
                                {
                                    dictionary.Add(simillarElement.Substring(0, 2), simillarElement.Substring(0, 2));

                                    simillarElements.Remove(simillarElement.Substring(0, 2));

                                    numberOfVerySimillarElements--;
                                }

                                int number = numberOfVerySimillarElements;

                                for (int i = simillarElements.Count - 1; i >= 0; i--)
                                {
                                    if (simillarElements[i].Substring(0, 2) == simillarElement.Substring(0, 2))
                                    {
                                        dictionary.Add(simillarElements[i], simillarElement.Substring(0, 2) + number.ToString());

                                        simillarElements.RemoveAt(i);

                                        number -= 1;

                                        if (number == 0) { break; }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return dictionary;
        }
    }
}
