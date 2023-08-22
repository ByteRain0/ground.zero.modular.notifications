using ApplicationRegistry.Data.Models;
using Shared.Specifications;

namespace ApplicationRegistry.Data.Specifications;

public class ApplicationWithEventsByCodeSpecification : Specification<ApplicationDataModel>
{
    public ApplicationWithEventsByCodeSpecification(string applicationCode)
        : base(app => app.Code.ToLower() == applicationCode.ToLower())
    {
        AddInclude(app => app.Events);
        AddOrderBy(app => app.Code);
    }
}
