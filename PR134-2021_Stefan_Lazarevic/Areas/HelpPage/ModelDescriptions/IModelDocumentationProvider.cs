using System;
using System.Reflection;

namespace PR134_2021_Stefan_Lazarevic.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}