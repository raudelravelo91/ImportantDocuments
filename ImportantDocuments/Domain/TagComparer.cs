using System.Diagnostics.CodeAnalysis;

namespace ImportantDocuments.Domain
{
    public class TagComparer : IComparer<Tag>
    {
        public int Compare(Tag x, Tag y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    public class TagComparerLowercase : IComparer<Tag>
    {
        public int Compare(Tag x, Tag y)
        {
            return x.Name.ToLower().CompareTo(y.Name.ToLower());
        }
    }

    public class TagEqualityComparerLowercase : IEqualityComparer<Tag>
    {
        public bool Equals(Tag x, Tag y)
        {
            return x.Name.ToLower().Equals(y.Name.ToLower());
        }

        public int GetHashCode([DisallowNull] Tag obj)
        {
            return obj.Name.ToLower().GetHashCode();
        }
    }
}
