using System.ComponentModel;
using System.Reflection;

public class SortableBindingList<T> : BindingList<T>
{
    private bool _isSorted;
    private ListSortDirection _sortDirection;
    private PropertyDescriptor? _sortProperty;

    public SortableBindingList() : base() { }
    public SortableBindingList(IList<T> list) : base(list) { }

    protected override bool SupportsSortingCore => true;
    protected override bool IsSortedCore => _isSorted;
    protected override PropertyDescriptor? SortPropertyCore => _sortProperty;
    protected override ListSortDirection SortDirectionCore => _sortDirection;

    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        if (Items is not List<T> list)
            return;

        PropertyInfo? propInfo = typeof(T).GetProperty(prop.Name);
        if (propInfo == null)
            return;

        list.Sort((a, b) =>
        {
            object? valA = propInfo.GetValue(a);
            object? valB = propInfo.GetValue(b);

            // null handling
            if (valA == null && valB == null) return 0;
            if (valA == null) return -1;
            if (valB == null) return 1;

            if (valA is IComparable compA)
            {
                int result = compA.CompareTo(valB);
                return direction == ListSortDirection.Ascending ? result : -result;
            }

            // fallback: compare string representations
            int fallback = string.Compare(valA.ToString(), valB.ToString(), StringComparison.OrdinalIgnoreCase);
            return direction == ListSortDirection.Ascending ? fallback : -fallback;
        });

        _sortProperty = prop;
        _sortDirection = direction;
        _isSorted = true;

        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    protected override void RemoveSortCore()
    {
        _isSorted = false;
        _sortProperty = null;
    }
}
