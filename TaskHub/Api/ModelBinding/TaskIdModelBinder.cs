using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.ModelBinding  
{
    public class TaskIdModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var routeData = bindingContext.ActionContext.RouteData.Values;
            
            if (!routeData.TryGetValue("id", out var idValue) || idValue == null || string.IsNullOrWhiteSpace(idValue.ToString()))
            {
                bindingContext.ModelState.AddModelError("id", "Идентификатор задачи не задан");
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var idString = idValue.ToString();
            
            if (!Guid.TryParse(idString, out var guidValue))
            {
                bindingContext.ModelState.AddModelError("id", "Идентификатор задачи имеет некорректный формат");
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(guidValue);
            return Task.CompletedTask;
        }
    }
}