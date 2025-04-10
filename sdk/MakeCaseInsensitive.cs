//
// This autonomous intelligent system software is the property of Cartheur Research B.V. Copyright 2021 - 2025, all rights reserved.
//
namespace Cartheur.Animals.Robot
{
    /// <summary>
    /// Normalizes the input text into upper case.
    /// </summary>
    public class MakeCaseInsensitive
    {
        /// <summary>
        /// An ease-of-use static method that re-produces the instance transformation methods.
        /// </summary>
        /// <param name="input">The string to transform</param>
        /// <returns>The resulting string</returns>
        public static string TransformInput(string input)
        {
            return input.ToUpper();
        }
    }
}
