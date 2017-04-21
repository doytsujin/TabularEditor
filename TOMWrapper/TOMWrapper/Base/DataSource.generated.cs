// Code generated by a template
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using TabularEditor.PropertyGridUI;
using TabularEditor.UndoFramework;
using TOM = Microsoft.AnalysisServices.Tabular;

namespace TabularEditor.TOMWrapper
{
  
    /// <summary>
	/// Base class declaration for DataSource
	/// </summary>
	[TypeConverter(typeof(DynamicPropertyConverter))]
	public abstract partial class DataSource: TabularNamedObject, IDescriptionObject, IAnnotationObject
	{
	    protected internal new TOM.DataSource MetadataObject { get { return base.MetadataObject as TOM.DataSource; } internal set { base.MetadataObject = value; } }


		public DataSource(TabularModelHandler handler, TOM.DataSource datasourceMetadataObject, bool autoInit = true ) : base(handler, datasourceMetadataObject, autoInit )
		{
		}
		public string GetAnnotation(string name) {
		    return MetadataObject.Annotations.Find(name)?.Value;
		}
		public void SetAnnotation(string name, string value, bool undoable = true) {
			if(MetadataObject.Annotations.Contains(name)) {
				MetadataObject.Annotations[name].Value = value;
			} else {
				MetadataObject.Annotations.Add(new TOM.Annotation{ Name = name, Value = value });
			}
			if (undoable) Handler.UndoManager.Add(new UndoAnnotationAction(this, name, value));
		}
		        /// <summary>
        /// Gets or sets the Description of the DataSource.
        /// </summary>
		[DisplayName("Description")]
		[Category("Basic"),IntelliSense("The Description of this DataSource.")]
		public string Description {
			get {
			    return MetadataObject.Description;
			}
			set {
				var oldValue = Description;
				if (oldValue == value) return;
				bool undoable = true;
				bool cancel = false;
				OnPropertyChanging("Description", value, ref undoable, ref cancel);
				if (cancel) return;
				MetadataObject.Description = value;
				if(undoable) Handler.UndoManager.Add(new UndoPropertyChangedAction(this, "Description", oldValue, value));
				OnPropertyChanged("Description", oldValue, value);
			}
		}
		private bool ShouldSerializeDescription() { return false; }
        /// <summary>
        /// Collection of localized descriptions for this DataSource.
        /// </summary>
        [Browsable(true),DisplayName("Descriptions"),Category("Translations and Perspectives")]
	    public new TranslationIndexer TranslatedDescriptions { get { return base.TranslatedDescriptions; } }
        /// <summary>
        /// Gets or sets the Type of the DataSource.
        /// </summary>
		[DisplayName("Type")]
		[Category("Other"),IntelliSense("The Type of this DataSource.")]
		public TOM.DataSourceType Type {
			get {
			    return MetadataObject.Type;
			}
			
		}
		private bool ShouldSerializeType() { return false; }
    }

	/// <summary>
	/// Collection class for DataSource. Provides convenient properties for setting a property on multiple objects at once.
	/// </summary>
	public partial class DataSourceCollection: TabularObjectCollection<DataSource, TOM.DataSource, TOM.Model>
	{
		public Model Parent { get; private set; }

		public DataSourceCollection(TabularModelHandler handler, string collectionName, TOM.DataSourceCollection metadataObjectCollection, Model parent) : base(handler, collectionName, metadataObjectCollection)
		{
			Parent = parent;

			// Construct child objects (they are automatically added to the Handler's WrapperLookup dictionary):
			foreach(var obj in MetadataObjectCollection) {
				switch((obj as TOM.DataSource).Type) {
					case TOM.DataSourceType.Provider: new ProviderDataSource(handler, obj as TOM.ProviderDataSource) { Collection = this }; break;
					case TOM.DataSourceType.Structured: new StructuredDataSource(handler, obj as TOM.StructuredDataSource) { Collection = this }; break;
				}
			}
		}

		[Description("Sets the Description property of all objects in the collection at once.")]
		public string Description {
			set {
				if(Handler == null) return;
				Handler.UndoManager.BeginBatch(UndoPropertyChangedAction.GetActionNameFromProperty("Description"));
				this.ToList().ForEach(item => { item.Description = value; });
				Handler.UndoManager.EndBatch();
			}
		}

		public override string ToString() {
			return string.Format("({0} {1})", Count, (Count == 1 ? "DataSource" : "DataSources").ToLower());
		}
	}
}
