using System;

namespace PlainElastic.Net.Mappings
{
    public abstract class ObjectBase<T, TMapping> : MappingBase<TMapping> where TMapping : ObjectBase<T, TMapping>
    {

        /// <summary>
        /// The name of mapped object.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Allows to Map object properties
        /// see http://www.elasticsearch.org/guide/reference/mapping/object-type.html
        /// </summary>
        public TMapping Properties(Func<Properties<T>, Properties<T>> properties)
        {
            RegisterMapAsJson(properties);

            return (TMapping)this;
        }

        /// <summary>
        /// Allows to disable dynamic JSOM object mapping.
        /// </summary>
        public TMapping Dynamic(bool enableDynamic)
        {
            return Custom(" 'dynamic': {0}", enableDynamic.AsString());
        }

        /// <summary>
        /// Allows to disable parsing and adding a named object completely.
        /// </summary>
        public TMapping Enabled(bool enable)
        {
            return Custom(" 'enabled': {0}", enable.AsString());
        }

        /// <summary>
        /// Allows to specify custom path within index to mapped document. 
        /// </summary>
        public TMapping Path(string path)
        {
            return Custom(" 'path': {0}", path.Quotate());
        }

        /// <summary>
        /// Allows to control document and inner documents inclusion to _all field.
        /// </summary>
        public TMapping IncludeInAll(bool includeInAll)
        {
            return Custom(" 'include_in_all': {0}", includeInAll.AsString());
        }



        protected override string ApplyMappingTemplate(string mappingBody)
        {
            return " {0}: {{ 'type': 'object', {1} }}".SmartQuoteF(Name.Quotate(), mappingBody);
        }
    }
}