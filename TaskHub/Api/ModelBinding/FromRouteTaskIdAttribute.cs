using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.ModelBinding
{
    public class FromRouteTaskIdAttribute : ModelBinderAttribute
    {
        public FromRouteTaskIdAttribute() : base(typeof(TaskIdModelBinder))
        {
            BindingSource = BindingSource.Path;
        }
    }
}