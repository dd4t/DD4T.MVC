using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DD4T.Mvc.Html;
using DD4T.ViewModels.Attributes;
using System.Collections;
using DD4T.ContentModel;
using DD4T.Core.Contracts.ViewModels;
using System;

namespace DD4T.Mvc.ViewModels.Attributes
{
    /// <summary>
    /// An Attribute for a Property representing the Link Resolved URL for a Linked or Multimedia Component
    /// </summary>
    /// <remarks>Uses the default DD4T GetResolvedUrl extension method. To override behavior you must implement
    /// your own Field Attribute. Future DD4T versions will hopefully allow for IoC of this implementation.</remarks>
    public class ResolvedUrlFieldAttribute : FieldAttributeBase
    {
        //public ResolvedUrlFieldAttribute(string fieldName) : base(fieldName) { }
        public override IEnumerable GetFieldValues(IField field, IModelProperty property, ITemplate template, IViewModelFactory factory)
        {
            return field.LinkedComponentValues
                .Select(x => x.GetResolvedUrl());
        }

        public override Type ExpectedReturnType
        {
            get { return typeof(string); }
        }
    }

    /// <summary>
    /// A Rich Text field. Uses the default ResolveRichText extension method.
    /// </summary>
    /// <remarks>This Attribute is dependent on a specific implementation for resolving Rich Text. 
    /// In future versions of DD4T, the rich text resolver will hopefully be abstracted to allow for IoC, 
    /// but for now, to change the behavior you must implement your own Attribute.</remarks>
    public class RichTextFieldAttribute : FieldAttributeBase
    {
        //public RichTextFieldAttribute(string fieldName) : base(fieldName) { }
        public override IEnumerable GetFieldValues(IField field, IModelProperty property, ITemplate template, IViewModelFactory factory)
        {
            return field.Values
                .Select(v => v.ResolveRichText()); //Hidden dependency on DD4T Resolve Rich Text implementation
        }

        public override Type ExpectedReturnType
        {
            get { return  typeof(MvcHtmlString); }
        }
    }


}