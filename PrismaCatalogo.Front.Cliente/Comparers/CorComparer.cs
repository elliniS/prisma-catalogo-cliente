using PrismaCatalogo.Front.Cliente.Models;
using System.Diagnostics.CodeAnalysis;

namespace PrismaCatalogo.Front.Cliente.Comparers
{
    public class CorComparer : IEqualityComparer<CorViewModel>
    {
        public bool Equals(CorViewModel? x, CorViewModel? y)
        {
            if (ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] CorViewModel obj)
        {
            if (obj == null)
                return obj.GetHashCode();

            return obj.Id.ToString().GetHashCode();
        }
    }
}
