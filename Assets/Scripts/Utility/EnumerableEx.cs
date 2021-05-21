using System;
using System.Collections.Generic;
using System.Linq;

namespace Garaj.Utility{
    public static class EnumerableEx{
        public static T FirstOrDefaultWithIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate, out int index) {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var retVal = 0;
            foreach (var item in items) {
                if (predicate(item)) {
                    index = retVal;
                    return item;
                }
                retVal++;
            }
            index = 0;
            
            return default;
        }

        public static T GetRandomItemBaseOnWeight<T>(this IEnumerable<T> items, out int indexOfItem) where T : IWeightItem{
            var item = default(T);
            
            var weightItems = items.ToList();
            var weightRange = weightItems.Sum(i => i.Weight);
            var rndWeight = (0, weightRange).RandomRange();
            var passedWeight = 0;
            indexOfItem = -1;

            foreach (var weightItem in weightItems) {
                indexOfItem++;
                if (passedWeight + weightItem.Weight < rndWeight) {
                    passedWeight += weightItem.Weight;
                    continue;
                }

                item = weightItem;
                break;
            }

            return item;
        }
        
        public static T GetRandomItemBaseOnWeight<T>(this IEnumerable<T> items) where T : IWeightItem {
            return GetRandomItemBaseOnWeight(items, out var order);
        }
    }
    
    public interface IWeightItem{
        int Weight { get; }
    }
}