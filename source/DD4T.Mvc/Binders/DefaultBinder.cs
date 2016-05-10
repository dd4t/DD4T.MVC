using DD4T.ContentModel;
using DD4T.ContentModel.Factories;
using DD4T.Core.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DD4T.Mvc.Binders
{
    public class TypedModelBinder : DefaultModelBinder, IModelBinderProvider
    {
        private IPageFactory _pageFactory;
        private IViewModelFactory _viewModelFactory;
        private IComponentPresentationFactory _componentPresentationFactory;

        public TypedModelBinder(IPageFactory pageFactory, IViewModelFactory viewModelFactory, IComponentPresentationFactory componentPresentationFactory)
        {
            _pageFactory = pageFactory;
            _viewModelFactory = viewModelFactory;
            _componentPresentationFactory = componentPresentationFactory;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object model = null;

            ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value != null)
            {
                var passedModel = value.RawValue;

                if (passedModel != null && bindingContext.ModelType.IsAssignableFrom(passedModel.GetType()))
                {
                    //route already available and of the correct type so return it.
                    return passedModel;
                }

                string modelData = value.AttemptedValue;
                if (modelData != null)
                {
                    IModel result = null;
                    if (modelData.StartsWith("tcm:"))
                    {
                        var id = new TcmUri(modelData);
                        if (id.ItemTypeId == 64)
                        {
                            //Try to get and return the object by id.
                            result = _pageFactory.GetPage(modelData);
                        }
                        else
                        {
                            //Try to get and return the object by id.
                            result = _componentPresentationFactory.GetComponentPresentation(modelData);
                        }
                    }
                    else
                    {
                        if (!modelData.StartsWith("/"))
                            modelData = string.Concat("/", modelData);

                        IPage page;
                        if (_pageFactory.TryFindPage(modelData, out page))
                        {
                            result = page;
                        }
                    }
                    if (result != null)
                        model = _viewModelFactory.BuildViewModel(result);
                }
            }
            return model;
        }

        public IModelBinder GetBinder(Type modelType)
        {
            //Can bind to IViewModel
            if (typeof(IViewModel).IsAssignableFrom(modelType)) return this;
            return null;
        }
    }
}