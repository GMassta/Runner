/**
 * Расширение коллекции доп. функциями
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace MapGenerator.WorldObjects
{
    public static class CollectionExtension
    {
        private static Random rnd = new Random();

        // Функция возвращает случайный объект из выбранной коллекции
        public static T Random<T>(this IEnumerable<T> collection) =>
            collection.ElementAt(rnd.Next(collection.Count() - 1));
    }
}