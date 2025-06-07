using PrismaCatalogo.Front.Cliente.Models;
using System.Diagnostics.CodeAnalysis;

namespace PrismaCatalogo.Front.Cliente
{
    public class TamanhoComparer : IEqualityComparer<TamanhoViewModel>
    {
        public bool Equals(TamanhoViewModel? x, TamanhoViewModel? y)
        {
            if(Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] TamanhoViewModel obj)
        {
            if (obj == null)
                return obj.GetHashCode();

            return obj.Id.ToString().GetHashCode();
        }
    }
}
