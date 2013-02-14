using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Cirrious.MvvmCross.Binding.Interfaces;
using Cirrious.MvvmCross.Binding.Interfaces.Binders;
using Cirrious.MvvmCross.Binding.Touch.Interfaces.Views;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using MonoTouch.Foundation;
using MonoTouch.UIKit;


namespace Cirrious.MvvmCross.Binding
{
	public abstract class MvxBaseBindablePickerViewDataSource : UIPickerViewModel
	{
		private static readonly MvxBindingDescription[] DefaultBindingDescription = new MvxBindingDescription[]
		{
			new MvxBindingDescription()
			{
				TargetName = "TitleText",
				SourcePropertyPath = string.Empty
			}, 
		};
		
		private readonly IEnumerable<MvxBindingDescription> _bindingDescriptions;
		private readonly UIPickerView _pickerView;
		
		protected MvxBaseBindablePickerViewDataSource(UIPickerView pickerView)
			: this(pickerView, DefaultBindingDescription)
		{
		}
		
		protected MvxBaseBindablePickerViewDataSource(UIPickerView pickerView, IEnumerable<MvxBindingDescription> descriptions)
		{
			_pickerView = pickerView;
			_bindingDescriptions = descriptions;
		}
		
		protected IEnumerable<MvxBindingDescription> BindingDescriptions { get { return _bindingDescriptions; } }
		protected UIPickerView TableView { get { return _pickerView; } }
		
		private static IEnumerable<MvxBindingDescription> ParseBindingText(string bindingText)
		{
			if (string.IsNullOrEmpty(bindingText))
				return DefaultBindingDescription;
			
			return MvxServiceProviderExtensions.GetService<IMvxBindingDescriptionParser>().Parse(bindingText);
		}
		
		public event EventHandler<MvxSimpleSelectionChangedEventArgs> SelectionChanged;
		
		public virtual void ReloadTableData()
		{
			_pickerView.ReloadAllComponents();
		}
		
		protected virtual void CollectionChangedOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			ReloadTableData();
		}


//		protected abstract object GetItemAt(NSIndexPath indexPath);
//		
//		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
//		{
//			var item = GetItemAt(indexPath);
//			var selectionChangedArgs = MvxSimpleSelectionChangedEventArgs.JustAddOneItem(item);
//			
//			var handler = SelectionChanged;
//			if (handler != null)
//				handler(this, selectionChangedArgs);
//		}
	}
}

