using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace QuizWebsite.Pages
{
    /// <summary>
    /// <see cref="https://learn.microsoft.com/en-us/aspnet/core/mvc/advanced/custom-model-binding?view=aspnetcore-8.0"/>
    /// </summary>
    public class QuizQuestionModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType != typeof(QuizQuestion))
            {
                return null;
            }

            var subclasses = new[] { typeof(SelectQuestion), typeof(TextQuestion), };

            var binders = new Dictionary<Type, (ModelMetadata, IModelBinder)>();
            foreach (var type in subclasses)
            {
                var modelMetadata = context.MetadataProvider.GetMetadataForType(type);
                binders[type] = (modelMetadata, context.CreateBinder(modelMetadata));
            }

            return new QuizQuestionModelBinder(binders);
        }
    }
}
