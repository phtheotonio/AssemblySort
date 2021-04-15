using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblySort
{
    public class AssemblyProduct : IEquatable<AssemblyProduct>, IComparable<AssemblyProduct>
    {
        public string startingTime { get; set; }
        public string productName { get; set; }
        public int assemblyValue { get; set; }

        public int SortByNameAscending(string name1, string name2)
        {

            return name1.CompareTo(name2);
        }

        public int CompareTo(AssemblyProduct compareProduct)
        {
            if (compareProduct == null)
                return 1;

            else
                return compareProduct.assemblyValue.CompareTo(this.assemblyValue);
        }


        public override int GetHashCode()
        {
            return assemblyValue;
        }
        public bool Equals(AssemblyProduct other)
        {
            if (other == null) return false;
            return (this.assemblyValue.Equals(other.assemblyValue));
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            AssemblyProduct objAsPart = obj as AssemblyProduct;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
    }
}
