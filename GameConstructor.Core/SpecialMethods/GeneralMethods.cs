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
        public static char[] MathOperators = new char[] { '>', '<', '⩾', '⩽', '=', '≠' };
        public static char[] MathOperations = new char[] { '+', '-', '/', '*' };
        public static char[] LogicalOperators = new char[] { '&', '|' };
        

        public static bool AreThereSameElementsInTheStringCollection (IEnumerable<string> collection, out string element)
        {
            element = null;

            foreach (var str in collection)
            {
                int number = collection.Count(item => item == str);

                if (number != 1)
                {
                    element = str;

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



        public static string ReturnOriginalRegisterConfiguration (string str, IEnumerable<string> collection)
        {
            foreach (var item in collection)
            {
                if (str.ToUpperInvariant() == item.ToUpperInvariant()) { return item; }
            }

            return null;
        }



        public static string MathConditionWithValidSpaces(string str)
        {
            str = str.Replace(" ", "")
                .Replace("!=", "≠")
                .Replace(">=", "⩾")
                .Replace("<=", "⩽");

            int i = 0;

            while (i < str.Length)
            {
                if (MathOperators.Contains(str[i]) || LogicalOperators.Contains(str[i]))
                {
                    str = str.Substring(0, i) + " " + str[i] + " " + str.Substring(i + 1);

                    i += 3;
                }

                else
                {
                    i++;
                }
            }

            return str;
        }



        public static bool? MathOperatorsContains(string element)
        {
            if (element.Length > 1)
            {
                return null;
            }

            else if (MathOperators.Contains(element[0]))
            {
                return true;
            }

            else
            {
                return false;
            }            
        }

        public static bool? LogicalOperatorsContains(string element)
        {
            if (element.Length > 1)
            {
                return null;
            }

            else if (LogicalOperators.Contains(element[0]))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
