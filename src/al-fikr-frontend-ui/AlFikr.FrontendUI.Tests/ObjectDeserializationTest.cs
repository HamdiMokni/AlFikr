using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Web.Extensions;
using Xunit;

namespace AlFikr.FrontendUI.Tests;

public class ObjectDeserializationTest
{
    [Fact]
    public void ebook_deserialization()
    {
        var str = "    {\"id\": 1  }";

        var item = str.ToEntity<DocumentEntity>();
    }
}
