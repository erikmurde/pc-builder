using System.Collections;
using DAL.EF.App.Seeding;
using DAL.EF.App.Seeding.Names;

namespace Tests.Helpers;

public class TestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] {0, CategoryNames.PrebuiltPc},
        new object[] {1, CategoryNames.PrebuiltPc},
        new object[] {10, CategoryNames.PrebuiltPc},
        new object[] {0, CategoryNames.TemplatePc},
        new object[] {1, CategoryNames.TemplatePc},
        new object[] {10, CategoryNames.TemplatePc},
        new object[] {0, CategoryNames.CustomPc},
        new object[] {1, CategoryNames.CustomPc},
        new object[] {10, CategoryNames.CustomPc}
    };
    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}